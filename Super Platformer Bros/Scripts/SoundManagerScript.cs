using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerDeathSound, playerJumpSound, playerDoubleJumpSound, playerHitSound, playerShootSound, playerWalkSound, enemyDeathSound, ghostFireSound, ghostSpawnSound, platformCrumblingSound;
    public static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        //playerDeathSound = Resources.Load<AudioClip>("playerDeathSound");
        //playerJumpSound = Resources.Load<AudioClip>("playerJumpSound");
        //playerDoubleJumpSound = Resources.Load<AudioClip>("playerDoubleJumpSound");
        playerHitSound = Resources.Load<AudioClip>("playerHitSound");
        playerShootSound = Resources.Load<AudioClip>("playerShootSound");
        playerWalkSound = Resources.Load<AudioClip>("playerWalkSound");
        enemyDeathSound = Resources.Load<AudioClip>("enemyDeathSound");
        //ghostFireSound = Resources.Load<AudioClip>("ghostFireSound");
        //ghostSpawnSound = Resources.Load<AudioClip>("ghostSpawnSound");
        platformCrumblingSound = Resources.Load<AudioClip>("platformCrumblingSound");

        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "playerDeathSound":
                audioSrc.PlayOneShot(playerDeathSound);
                break;
            case "playerJumpSound":
                audioSrc.PlayOneShot(playerJumpSound);
                break;
            case "playerDoubleJumpSound":
                audioSrc.PlayOneShot(playerDoubleJumpSound);
                break;
            case "playerHitSound":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "playerShootSound":
                audioSrc.PlayOneShot(playerShootSound);
                break;
            case "playerWalkSound":
                audioSrc.PlayOneShot(playerWalkSound);
                break;
            case "enemyDeathSound":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;
            case "ghostFireSound":
                audioSrc.PlayOneShot(ghostFireSound);
                break;
            case "ghostSpawnSound":
                audioSrc.PlayOneShot(ghostSpawnSound);
                break;
            case "platformCrumblingSound":
                audioSrc.PlayOneShot(platformCrumblingSound);
                break;

        }
    }

}
