using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoundGhost : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovement;
    public GameObject parent;
    public bool ghostTriggered;
    private float originalSpeed;
    private float changedSpeed;

    private Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        rb = GetComponentInParent<Rigidbody>();
    }


    void Update()
    {
        GhostBehavior();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ghostTriggered = true;
        }
        

        
    }




    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ghostTriggered = false;
            
            Debug.Log("Player Exiting Ghost Trigger");
        }
    }



    void GhostBehavior()
    {
        if (ghostTriggered)
        {
            playerMovement.baseMoveSpeed = 0.2f;
            GetComponentInParent<GhostFloat>().enabled = false;
            rb.useGravity = true;
            rb.freezeRotation = true;
            rb.AddForce(0, -600, 0);
            ghostTriggered = false;
            Debug.Log("Ground Pound");
        }
        else if (ghostTriggered == false)
        {
            playerMovement.baseMoveSpeed = 10;
        }
    }
}
