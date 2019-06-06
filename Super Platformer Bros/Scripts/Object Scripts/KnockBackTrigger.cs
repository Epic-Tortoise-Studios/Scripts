using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackTrigger : MonoBehaviour
{
    private GameObject player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player.GetComponent<PlayerMovement>().Launch();
        }
    }
}
