using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleWall : MonoBehaviour
{
    public Transform telePartner;
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Teleported");
            player.transform.position = telePartner.position;
            player.transform.rotation = telePartner.rotation;
        }
    }
}
