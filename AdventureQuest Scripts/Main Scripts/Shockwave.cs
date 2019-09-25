using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{

    private float speed;
    private Vector3 direction;
    private float min;
    private float max;
    private float units = 2.0f;

    public float spawnCheck;
    private float spawnCheckTimer;

    public GameObject projectile;
    private GameObject spawnedProjectile;
    public Transform projectileSpawn1;
    public Transform projectileSpawn2;
    public Transform projectileSpawn3;
    public Transform projectileSpawn4;

    public AudioClip slamNoise;
    public ParticleSystem[] collisionHit;

    private bool stopSpawn;
    public bool stopSlam = true;

    void Start()
    {
        max = transform.position.y;
        min = transform.position.y - units;

        direction = Vector3.up;

        spawnCheckTimer = spawnCheck;
    }

    void Update()
    {
        if (!stopSlam)
        {
            Debug.Log(spawnCheck);
            if (direction == Vector3.down)
            {
                speed = 10.0f;
            }
            else if (direction == Vector3.up)
            {
                speed = 1.0f;
            }

            spawnCheckTimer -= Time.deltaTime;
            transform.Translate(direction * speed * Time.deltaTime);

            if (transform.position.y >= max && spawnCheckTimer <= 0)
            {
                direction = Vector3.down;
                stopSpawn = false;
                spawnCheckTimer = spawnCheck;
            }

            if (transform.position.y <= min && !stopSpawn)
            {
                direction = Vector3.up;

                Instantiate(projectile, projectileSpawn1);
                Instantiate(projectile, projectileSpawn2);
                Instantiate(projectile, projectileSpawn3);
                Instantiate(projectile, projectileSpawn4);

                AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
                AudioManager.instance.PlayClip(slamNoise);

                foreach (ParticleSystem c in collisionHit)
                {
                    if (c != null)
                    {
                        c.Play();
                    }
                }
               //collisionHit.Play();

                stopSpawn = true;
            }

            spawnedProjectile = GameObject.FindGameObjectWithTag("WaveProjectile");
        }

    }
}
