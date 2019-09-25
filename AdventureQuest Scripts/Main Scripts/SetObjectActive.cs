using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectActive : MonoBehaviour
{
    public GameObject toSetActive;
    public GameObject setShellInactive;

    public AudioClip triggeredClip;

    private bool setActive;

    void Start()
    {
        toSetActive.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject.layer == 0)
        {
            if (!setActive)
            {
                AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
                AudioManager.instance.PlayClip(triggeredClip);
            }

            Debug.Log("Hitting Collision");
            setActive = true;
        }
    }


    private void Update()
    {
        if (setActive)
        {
            toSetActive.SetActive(true);
            setShellInactive.SetActive(false);
        }
    }
}
