using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    //Hook 1

    //Hook 2

    //Hook 3

    //Hook 4

    public WallMovement wallMovement;
    
    public bool inHook1 = false;
    public bool inHook2 = false;
    public bool inHook3 = false;
    public bool inHook4 = false;

    void Start()
    {

    }


    void Update()
    {
        CheckHealth();
    }

    void CheckHealth()
    {
        if (inHook1)
        {
            
        }

        if (inHook2)
        {
            
        }


        if (inHook3)
        {
            
        }

        if (inHook1 == false && inHook2 == false && inHook3 == false && inHook4)
        {
            wallMovement.moving = true;
            Debug.Log("Stay Mid");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "H1")
            {
                inHook1 = true;
                inHook2 = false;
                inHook3 = false;
                inHook4 = false;
            }
            else if(other.gameObject.tag == "H2")
            {
                inHook1 = false;
                inHook2 = true;
                inHook3 = false;
                inHook4 = false;
            }
            else if (other.gameObject.tag == "H3")
            {
                inHook1 = false;
                inHook2 = false;
                inHook3 = true;
                inHook4 = false;
            }
            else if (other.gameObject.tag == "H4")
            {
                inHook1 = false;
                inHook2 = false;
                inHook3 = false;
                inHook4 = true;

                wallMovement.transform.position = wallMovement.startPosition;
            }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.name == "Hook1_Trigger")
            {
                inHook1 = false;
                inHook2 = false;
                inHook3 = false;
                inHook4 = false;
            }
            else if (this.gameObject.name == "Hook2_Trigger")
            {
                inHook1 = false;
                inHook2 = false;
                inHook3 = false;
                inHook4 = false;
            }
            else if (this.gameObject.name == "Hook3_Trigger")
            {
                inHook1 = false;
                inHook2 = false;
                inHook3 = false;
                inHook4 = false;
            }
            else if (this.gameObject.name == "Hook4_Trigger")
            {
                inHook1 = false;
                inHook2 = false;
                inHook3 = false;
                inHook4 = false;
            }
        }
    }*/
}
