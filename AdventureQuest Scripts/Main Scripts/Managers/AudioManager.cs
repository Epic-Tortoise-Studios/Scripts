using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip exclaim;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        if(audioSource == null)
        {
            audioSource = this.gameObject.GetComponent<AudioSource>();
        }
    }

    public AudioSource audioSource;

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void StopClip()
    {
        audioSource.Stop();
    }
}
