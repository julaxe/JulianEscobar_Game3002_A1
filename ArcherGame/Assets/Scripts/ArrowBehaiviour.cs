using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaiviour : MonoBehaviour
{ 
    //for the enemy
    public Material green;

    //own variables
    public bool inAir;
    private float angle;
    private Vector3 acceleration;
    public Vector3 velocity;

    //Score
    private ArcherController archer;


    void Start()
    {
        archer = GameObject.FindObjectOfType<ArcherController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(inAir)
        {
            //for this mini game we define an slower gravity so we can see the arrows better
            float gravity = 9.8f / 3.0f;

            //only gravity affects our acceleration
            acceleration = new Vector3(0.0f, -gravity, 0.0f);
            //v = v0 + a*t
            velocity += acceleration * Time.fixedDeltaTime;
            //we calculate the position.
            transform.position += velocity * Time.fixedDeltaTime;

            //angle calculation
            Vector2 LateralVelocity = new Vector2(velocity.x, velocity.z);
            Vector2 CompleteVelocity = new Vector2(LateralVelocity.magnitude, velocity.y);
            angle = Mathf.Atan2(CompleteVelocity.normalized.y, CompleteVelocity.normalized.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(-angle, transform.eulerAngles.y, 180.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(inAir) // the arrow is in Use.
        {
            if(collision.gameObject.GetComponent<Enemy>() != null) // it's an enemy!
            {
                collision.gameObject.GetComponent<Renderer>().material = green;
                collision.gameObject.GetComponent<Enemy>().active = false;
                archer.score += 10;
                inAir = false;
            }
            else if (collision.gameObject.tag == "yellow") // it's an enemy!
            {
                Destroy(collision.gameObject);
                archer.score += 50;
                inAir = false;
            }
            else if (collision.gameObject.tag == "purple") // it's an enemy!
            {
                Destroy(collision.gameObject);
                archer.score += 200;
                inAir = false;
            }
            else if (collision.gameObject.tag == "Target") // it's an enemy!
            {
                inAir = false;
            }
        }
        
    }
}
