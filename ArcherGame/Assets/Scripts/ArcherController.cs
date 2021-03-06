using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{
    //Shooting variables
    private GameObject rightHand;
    private GameObject arrowLocation;
    public GameObject arrow;
    public bool isShooting;
    public bool arrowReleased;
    private float initialVelocity;

    //animation
    private Animator animator;

    //Camera Variables
    public float cameraSensitivity;
    public Vector3 rotation;
    private float lockScreenZRot;

    //List of Arrows
    public List<GameObject> arrowsList;

    //UI
    public int score;
    public bool GameOver;

    // Start is called before the first frame update
    void Start()
    {
        //we get the right hand so late we can add the arrow to this position
        rightHand = GameObject.Find("RightHand");
        arrowLocation = GameObject.Find("ArrowLocation");

        //defines the state of shooting the arrow
        isShooting = false;
        initialVelocity = 0.0f;

        //initialize arrowList
        arrowsList = new List<GameObject>();

        //hide the cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //lock the screen on a defined value so the player has a better aim control.
        lockScreenZRot = 0.9f;

        //animation variables
        animator = GetComponent<Animator>();

   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) // left-click start the draw animation and also creates the arrow.
        {
            if(!isShooting)
            {
                CreateArrow();
            }
            isShooting = true;
            arrowReleased = false;

            //longer is hold more initial velocity will have.
            initialVelocity += 7.0f * Time.deltaTime; // 7 units every 1 seconds.
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            arrowReleased = true;
            ShootArrow();
        }
    }

    private void FixedUpdate()
    {
        //save the rotation so we can go back if we go out of boundaries.
        Quaternion oldRotation = transform.rotation;

        //axis of the mouse
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        //rotate function on both axis
        transform.RotateAround(transform.position, Vector3.up, rotateHorizontal * cameraSensitivity); //use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead if you dont want the camera to rotate around the player
        transform.RotateAround(Vector3.zero, transform.right, -rotateVertical * cameraSensitivity); // again, use transform.Rotate(transform.right * rotateVertical * sensitivity) if you don't want the camera to rotate around the player
        
        //we get the forward vector, we are going to check the rotation of the z axis (which in this case is the value of the y in the forward vector) of the player.
        rotation = transform.forward;
        if (rotation.y > lockScreenZRot || rotation.y < -lockScreenZRot)
        {
            transform.rotation = oldRotation;
            rotation = transform.forward;

            if (rotation.y > lockScreenZRot || rotation.y < -lockScreenZRot) // we do a double check if for some reason the oldRotation is not inside the safe zone, we go back to a deafault rotation 0,0,0.
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void CreateArrow()
    {
        GameObject temp = GameObject.Instantiate(arrow);
        temp.transform.SetParent(rightHand.transform);
        temp.transform.localPosition = arrowLocation.transform.localPosition;
        temp.transform.localRotation = arrowLocation.transform.localRotation;
        arrowsList.Add(temp);
        //temp.transform.position = arrowLocation.transform.position;
    }
    void ShootArrow()
    {
        if (arrowsList.Count > 0) // check if there are arrows created.
        {
            arrowsList[arrowsList.Count - 1].GetComponent<ArrowBehaiviour>().velocity = arrowsList[arrowsList.Count - 1].transform.forward * initialVelocity; // we set the initial velocity of the arrow
            arrowsList[arrowsList.Count - 1].GetComponent<ArrowBehaiviour>().inAir = true; // activate the movement of the arrow
            arrowsList[arrowsList.Count - 1].transform.parent = null; //last arrow in my list, we deatach the arrow so we can move it freely.
            initialVelocity = 0.0f; // reset the initial velocity for the next arrow
        }
    }
}
