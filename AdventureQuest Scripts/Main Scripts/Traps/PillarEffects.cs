using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarEffects : MonoBehaviour
{    
    public GameObject rumble;
    

    private bool isActive;

    void Start()
    {
        isActive = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isActive)
        {
            if (other.tag == "Player")
            {
                isActive = true;
                //StartCoroutine(RumbleEffect());
                rumble.transform.gameObject.SetActive(true);
            }
        }
    }

}