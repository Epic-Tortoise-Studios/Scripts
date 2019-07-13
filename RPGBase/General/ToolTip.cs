using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
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
            GUI.Box(new Rect(215, 525, 320, 35), "I need a key. I bet a Skeleton has it!");
            GUI.Box(new Rect(220, 530, 310, 25), "");
        }
    }
}

