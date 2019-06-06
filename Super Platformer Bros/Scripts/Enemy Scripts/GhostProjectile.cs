using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostProjectile : MonoBehaviour
{


    public PlayerHealth playerHealth;

    public GameObject player;
    public Transform playerPosition;

    public int ghostDamage;
    public float speed;
    private float destroyTimer = 0;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;
        FindPlayer();
    }

    void Update()
    {
        //FindPlayer();
        DestroyAfterTime();
        Fly();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth.currentHealth -= ghostDamage;
            Destroy(this.gameObject);
        }
    }

    void FindPlayer()
    {
        transform.LookAt(playerPosition);
        //Vector3 direction = player.transform.position - this.transform.position;
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);

        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), .0f);
    }

    void Fly()
    {
        Vector3 direction = player.transform.position - this.transform.position;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), .0f);
    }

    void DestroyAfterTime()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer >= 5.0f)
        {
            Destroy(gameObject);
        }
    }
}
