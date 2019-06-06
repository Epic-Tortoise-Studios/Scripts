using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostShoot : MonoBehaviour
{
    private Transform player;
    private GameObject playerObject;
    public GameObject projectile;
    public float shootTimer;
    private bool shotReady;
    private bool targetLocked;

    //public AudioClip ghostFireSound;
    //AudioSource audioSrc;

    void Start()
    {
        //audioSrc.GetComponent<AudioSource>();
        shotReady = true;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) < 20)
        {
            targetLocked = true;

        }

        if (targetLocked)
        {
            if (shotReady)
            {
                Shoot();
            }
        }
    }
    void Shoot()
    {
        Instantiate(projectile, transform.position + (transform.forward * 1), transform.rotation);
        shotReady = false;
        //audioSrc.PlayOneShot(ghostFireSound);
        StartCoroutine(FireRate());
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(shootTimer);
        shotReady = true;
    }
}
