using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour

{
    private Transform player;
    private GameObject playerObject;
    //public GameObject projectile;
    //public float shootTimer;
    public float speed;
    public float trackingDistance;
    //private bool shotReady;
    private bool targetLocked;


    void Start()
    {
        //shotReady = true;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
    }

    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) < trackingDistance)
        {
            transform.LookAt(player);
            Vector3 direction = player.transform.position - this.transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), .0f);

        }

        /*if (targetLocked)
        {
            if (shotReady)
            {
                Shoot();
            }
        }*/
    }
    /*void Shoot()
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
    }*/
}

