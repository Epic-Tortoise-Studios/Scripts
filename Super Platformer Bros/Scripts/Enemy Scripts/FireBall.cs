using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Transform player;

    public float speed;
    public int damage;

    private float destroyTimer = 0;

    //public AudioClip playerHitSound;
    //AudioSource audioSrc;

    private void Start()
    {
        //audioSrc.GetComponent<AudioSource>();

        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }


    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                        Quaternion.LookRotation(direction), .0f);

        destroyTimer += Time.deltaTime;
        if (destroyTimer >= 10.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Fireball did " + damage + " points of damage to the player");
            playerHealth.currentHealth -= damage;
            Destroy(gameObject);
            //audioSrc.PlayOneShot(playerHitSound);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        PlayerHealth player = other.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.ApplyDamage(addDamage);
            Destroy(gameObject);
        }
    }*/
}
