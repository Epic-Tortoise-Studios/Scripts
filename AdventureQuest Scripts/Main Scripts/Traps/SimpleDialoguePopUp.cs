using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialoguePopUp : MonoBehaviour
{
    public string typeHere = "";
    public bool onTrigger;
    public bool destroy;

    public void Start()
    {
        onTrigger = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            onTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if((other.tag == "Interactible") || destroy)
        {
            Destroy(this.gameObject);
        }
        onTrigger = false;
    }

    void OnGUI()
    {
        if (onTrigger)
        {
            GUI.Box(new Rect(215, 525, 320, 35), typeHere);
        }
    }
}
