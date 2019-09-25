using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterDamage : MonoBehaviour
{
    public int damage;
    public float speed;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(damage);
            Destroy(this.gameObject);
        }
    }
}
