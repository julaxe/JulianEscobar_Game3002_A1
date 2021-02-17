using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goUp();
    }

    void goUp()
    {
        float speedY = 1.0f;
        Vector3 velocity = new Vector3(0.0f, speedY, 0.0f);
        transform.position += velocity * Time.deltaTime;
    }
}
