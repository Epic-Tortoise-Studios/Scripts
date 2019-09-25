using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryself : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
