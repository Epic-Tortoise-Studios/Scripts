using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TT_EndLevel : MonoBehaviour
{
    public bool onTrigger;

    void OnTriggerStay(Collider other)
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
    }
    void OnGUI()
    {
        if (onTrigger)
        {
            GUI.Box(new Rect(215, 525, 320, 35), "Level Complete! Touch the orb to continue.");
            GUI.Box(new Rect(220, 530, 310, 25), "");
        }
    }
}