using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodySleep : MonoBehaviour
{
    public GameObject MovingPlatform;
    public Rigidbody rb;

    void Start()
    {
        MovingPlatform = GameObject.Find("Falling1");
        rb = GetComponent<Rigidbody>();
        rb.Sleep();
    }

    void Awake()
    {

        GetComponent<Rigidbody>().Sleep();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            rb.WakeUp();
            Debug.Log("Awake!");
        }
    }
}
