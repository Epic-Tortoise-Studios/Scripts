using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisOnTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        if (other.tag == "Ghost")
        {
            Destroy(this.gameObject);
        }
        if (other.tag == "Beast")
        {
            Destroy(this.gameObject);
        }
    }
}
