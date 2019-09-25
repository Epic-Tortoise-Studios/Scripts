using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveProjectile : MonoBehaviour
{
    public float speed;
    public float destroyTimer;
    public float damage;

    void Start()
    {

    }


    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        Destroy(this.gameObject, destroyTimer);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.Instance.TakeDamage(damage);
        }
    }
}
