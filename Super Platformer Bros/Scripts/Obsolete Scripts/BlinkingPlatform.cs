using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{
    public float maxTime = 0;
    public float minTime = 0;
    private float time;
    private float spawnTime;

    void Start()
    {
        RandomSpawn();
        time = minTime;
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= spawnTime)
        {
            BlinkInCheck();
            BlinkOutCheck();
            RandomSpawn();
        }
    }

    public void BlinkInCheck()
    {
        if (this.gameObject.GetComponent<Renderer>().enabled == false)
        {
            BlinkIn();
        }
    }

    public void BlinkOutCheck()
    {
        if (this.gameObject.GetComponent<Renderer>().enabled == true)
        {
            BlinkOut();
        }
    }
    

    public void BlinkOut()
    {
        time = 0;
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        BlinkIn();
    }

    public void BlinkIn()
    {
        time = 0;
        this.gameObject.GetComponent<Renderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        BlinkOut();
    }

    public void RandomSpawn()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }
}
