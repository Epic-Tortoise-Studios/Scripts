using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberGhost : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public float trackingDistance;
    private bool playerSeen;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }


    void Update()
    {
        Chase();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            //GetComponentInParent<GhostFloat>().enabled = false;

            
            Debug.Log("Bomber Chase");
        }


    }

    private void Chase()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < trackingDistance)
        {
            transform.LookAt(player.transform);
            Vector3 direction = player.transform.position - this.transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), .0f);
            GetComponentInParent<GhostFloat>().enabled = false;

        }
        

        
       if(Vector3.Distance(player.transform.position, this.transform.position) > trackingDistance)
       {
            GetComponentInParent<GhostFloat>().enabled = true;
            Debug.Log("Stopped Chasing");
       }

        
    }
}
