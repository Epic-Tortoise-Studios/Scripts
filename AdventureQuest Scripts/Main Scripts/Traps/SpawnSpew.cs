using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpew : MonoBehaviour
{
    public AudioClip spewer;

    public GameObject sewerSpew;

    public void Spawn()
    {
        Instantiate(sewerSpew, transform.position + transform.up * 0, transform.rotation);

        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(spewer);
    }
}
