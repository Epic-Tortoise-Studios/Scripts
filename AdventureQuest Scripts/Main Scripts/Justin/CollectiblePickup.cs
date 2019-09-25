using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    GameObject collectibleSound;
    AudioSource collectingSound;
    void Start()
    {
        collectibleSound = GameObject.Find("------Scene Essentials-------------/Sounds/Collectibles");
        collectingSound = collectibleSound.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectingSound.Play();
        }
    }
}
