using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBlockDropper : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boxes")
        {
            Destroy(this.gameObject);
        }
    }
}
