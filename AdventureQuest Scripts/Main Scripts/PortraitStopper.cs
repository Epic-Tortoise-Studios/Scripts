using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitStopper : MonoBehaviour
{
    public GameObject stopTrigger;

    private void OnColliderEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Stopper"))
        {
            Debug.Log("it worked");
        }
    }
}
