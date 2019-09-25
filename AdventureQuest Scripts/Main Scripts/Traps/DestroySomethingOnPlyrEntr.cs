using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySomethingOnPlyrEntr : MonoBehaviour
{
    public bool destroy;
    public GameObject[] makeActive;
    public GameObject[] makeInactive;

    public void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") || (other.tag == "Ghost"))
        {
            if(destroy)
            {
                foreach(GameObject floor1 in makeInactive)
                {
                    Destroy(floor1.gameObject);
                }
            }

            foreach (GameObject floor0 in makeActive)
            {
                floor0.transform.gameObject.SetActive(true);
            }

            foreach (GameObject floor in makeInactive)
            {
                floor.transform.gameObject.SetActive(false);
            }
        }
    }
}
