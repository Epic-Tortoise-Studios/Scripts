using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShot : MonoBehaviour
{
    public Transform player;
    public float speed;
    //public int addDamage = 0;
    float destroyTimer = 0;

    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                        Quaternion.LookRotation(direction), .0f);

        destroyTimer += Time.deltaTime;
        if (destroyTimer >= 5.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
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
