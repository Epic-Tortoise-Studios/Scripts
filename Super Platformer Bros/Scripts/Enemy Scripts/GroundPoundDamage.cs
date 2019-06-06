using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoundDamage : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private PlayerMovement playerMovement;
    public GroundPoundGhost groundPoundGhost;
    private float originalSpeed;

    public GameObject parent;
    //private Rigidbody rb;

    //private Collider col;

    public int damage;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerMovement = player.GetComponent<PlayerMovement>();


        

        //rb = this.gameObject.GetComponent<Rigidbody>();
        //col = this.gameObject.GetComponent<BoxCollider>();

        //col.isTrigger = true;
    }

    
    void Update()
    {
      
        Debug.Log(originalSpeed);
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.currentHealth -= damage;
            Destroy(parent);
            Debug.Log("Hit Player");
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.currentHealth -= damage;
            playerMovement.baseMoveSpeed = 10;
            Destroy(parent);
            Debug.Log("Hit Player");
        }
        if (other.gameObject.tag == "Ground")
        {
            Destroy(parent);
        }

    }
}
