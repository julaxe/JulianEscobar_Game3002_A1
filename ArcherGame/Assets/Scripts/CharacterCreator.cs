using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public GameObject balloonYellow;
    public GameObject balloonPurple;
    public GameObject enemy;

    private float delay;
    private double timer;
    private int countTargets;

    //range
    private float xMin;
    private float xMax;
    private float zMin;
    private float zMax;
    // Start is called before the first frame update
    void Start()
    {
        delay = 3;

        xMin = -20.0f;
        xMax = 20.0f;
        zMin = 10.0f;
        zMax = 18.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= delay) //if we pass the delay, instantiate a new target.
        {
            int selector = Random.Range(1, 5); // [1,5)
            if (selector < 2)// 25% chance
            {
                CreatePurpleBalloon();
            }
            else if(selector < 4) //(2,3) 50% chance
            {
                CreateYellowBalloon();
            }
            else // (4) 25% chance
            {
                CreateEnemy();
            }

            countTargets++; // one more target added.
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    void CreatePurpleBalloon()
    {
        GameObject temp = GameObject.Instantiate(balloonPurple);
        temp.transform.position = RandomPosition();
    }

    void CreateYellowBalloon()
    {
        GameObject temp = GameObject.Instantiate(balloonYellow);
        temp.transform.position = RandomPosition();
    }

    void CreateEnemy()
    {
        GameObject temp = GameObject.Instantiate(enemy);
        temp.transform.position = RandomPosition();
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(xMin, xMax), 1, Random.Range(zMin, zMax));
    }
}
