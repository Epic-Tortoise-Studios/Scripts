using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBehavior : MonoBehaviour
{

    private GameObject player;
    private PlayerMovement playerMovement;
    private Rigidbody rb;
    [SerializeField]
    private int freezeBodyTimer;
    private bool bodySpawned;
    private bool bodyActive;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        bodySpawned = true;
        freezeBodyTimer = 2;
        
    }

    void Update()
    {



    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && playerMovement.groundPounded)
        {
            Debug.Log("Destroy Body When Ground Pounded");
            Destroy(this.gameObject);
        }
    }

    private void PendulumBodies()
    {
        if (bodyActive)
        {

        }
    }


    private void FreezeBody()
    {
        if (bodySpawned)
        {
            freezeBodyTimer--;

            if (freezeBodyTimer == 0)
            {
                freezeBodyTimer = 2;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                Debug.Log("Freezing Body");
                bodySpawned = false;
            }
        }

    }
}
