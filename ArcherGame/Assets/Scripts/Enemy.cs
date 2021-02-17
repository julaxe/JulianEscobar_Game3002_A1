using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool active;
    private ArcherController archer;
    // Start is called before the first frame update
    void Start()
    {
        archer = GameObject.FindObjectOfType<ArcherController>();
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            RunTowardsPlayer();
        }
    }

    void RunTowardsPlayer()
    {
        Vector3 direction = archer.transform.position - transform.position;
        float speed = 2.0f;
        Vector3 velocity = new Vector3(direction.normalized.x * speed, 0 , direction.normalized.z * speed); // we are not moving on Y
        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if collision with archer -> defeat.
        if(collision.gameObject.GetComponent<ArcherController>() != null)
        {
            Time.timeScale = 0; // pause game, its over.
            archer.GameOver = true;
        }
    }
}
