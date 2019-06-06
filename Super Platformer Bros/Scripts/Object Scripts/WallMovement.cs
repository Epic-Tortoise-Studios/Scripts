using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    
    public float speed = .5f;

    //Kat: Added two variables.
    public Vector3 startPosition;
    public GameObject wallStopper;
    public bool moving;

    private void Start()
    {
        startPosition = this.transform.position;
        //moving = true;
    }


    void Update()
    {
        if (moving)
        {
            //Kat: This was all that was inside of the update before I made changes.
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "HurdleStopper")
        {
            Debug.Log("Wall should stop");
            moving = false;
        }
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "HurdleStopper")
        {
            Debug.Log("Wall should stop");
            moving = false;
        }
    }*/
}

