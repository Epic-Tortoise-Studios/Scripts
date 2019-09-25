using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxPad : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactible");
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
