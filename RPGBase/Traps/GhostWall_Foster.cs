using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostWall_Foster : MonoBehaviour
{
    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ghost")
        {
            GetComponent<Collider>().enabled = false;
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        GetComponent<Collider>().enabled = true;
    }

    
}
