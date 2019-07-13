using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TT_BreakWall : MonoBehaviour
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
            GUI.Box(new Rect(215, 560, 320, 35), "Press 'H' to change back"); //Back(Lg)
            GUI.Box(new Rect(220, 530, 310, 25), "Press 'J' to go Beast and break through this wall!"); //Front(Sm)
        }
    }
}
