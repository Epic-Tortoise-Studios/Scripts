using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathSound : MonoBehaviour
{
    public GameObject currentPlayer;
    private PlayerHealth playerHealth;

    public AudioClip playerDeathSound;
    AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        //Geting sound source
        audioSrc.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerHealth = currentPlayer.GetComponent<PlayerHealth>();

        if (playerHealth.playerDead == true)
        {
            //Playing death sound
            audioSrc.PlayOneShot(playerDeathSound);
        }
    }
}

