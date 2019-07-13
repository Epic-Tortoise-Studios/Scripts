﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSmoke : MonoBehaviour
{
    public GameObject trap;
    GameObject trapClone;
    public bool isActive;

    public void Start()
    {
        isActive = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isActive)
        {
            isActive = false;
            trapClone = Instantiate(trap, transform.position + (transform.forward * 0), transform.rotation);
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(6);
        isActive = true;
    }
}