using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandFloorBreak : MonoBehaviour
{
    public AudioClip chand;
    public GameObject[] brokenFloor;
    public GameObject[] breakableFloor;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chandelier")
        {
            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(chand);

            foreach (GameObject floor0 in brokenFloor)
            {
                floor0.transform.gameObject.SetActive(true);
            }

            foreach(GameObject floor in breakableFloor)
            {
                floor.transform.gameObject.SetActive(false);
            }
        }
    }
}
