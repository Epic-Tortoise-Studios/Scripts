using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachPlayer : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    } 

    void OnTriggerEnter(Collider other)
    {
        if(other == Player)
        {
            //Player.transform.parent = transform;
            Player.transform.SetParent(this.transform);
            Debug.Log("Setting Parent");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other == Player)
        {
            Player.transform.parent = null;
        }
    }

    public void Example(Transform newParent)
    {
        Player.transform.SetParent(newParent);
    }

}
