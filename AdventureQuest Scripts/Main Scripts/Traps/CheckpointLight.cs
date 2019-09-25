using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLight : MonoBehaviour
{
    public GameObject[] makeActive;

    public AudioClip checkpoint;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player") || (other.tag == "Ghost"))
        {
            foreach (GameObject light in makeActive)
            {
                light.transform.gameObject.SetActive(true);
                audio.PlayOneShot (checkpoint);
                StartCoroutine(Destroy());
            }
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
