using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{
    public GameObject Bullet;
    private float destroyTimer = 0;

    //public AudioClip playerShootSound;
    //AudioSource audioSrc;

    void Start()
    {
        //audioSrc.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(Bullet, transform.position + (transform.forward * 2), transform.rotation);

            //audioSrc.PlayOneShot(playerShootSound);
        }

        /*destroyTimer += Time.deltaTime;
        if (destroyTimer >= 2.0f)
        {
            Destroy(Bullet);
        }*/
    }
}
