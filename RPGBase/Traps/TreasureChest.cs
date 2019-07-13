using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public bool onTrigger;
    public bool doorOpened;
    public Transform doorHinge;
    public GameObject obj0;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;


    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
    }

    void Update()
    {
        if (onTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!doorOpened)
                {
                    doorOpened = true;
                    Instantiate(obj0, transform.position + (transform.up * 5), transform.rotation);
                    Instantiate(obj1, transform.position + (transform.up * 5), transform.rotation);
                    Instantiate(obj2, transform.position + (transform.up * 5), transform.rotation);
                    Instantiate(obj3, transform.position + (transform.up * 5), transform.rotation);
                    Instantiate(obj4, transform.position + (transform.up * 5), transform.rotation);
                    Destroy(this.gameObject);
                }
            }
        }

        if (doorOpened)
        {
            var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(65.0f, 0.0f, 0.0f), Time.deltaTime * 250);
            doorHinge.rotation = newRot;
            //this.gameObject.tag = "Opened";
        }
    }

    void OnGUI()
    {
        if (!doorOpened)
        {
            if (onTrigger)
            {
                GUI.Box(new Rect(215, 525, 320, 35), "Press 'E' to open");
            }
        }
    }
}
