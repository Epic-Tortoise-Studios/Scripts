using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionDamage : MonoBehaviour
{
    public int damage;
    public GameObject bloodSplatter;
    public AudioClip wepAttk;
    public AudioSource audioSource;


    void Start()
    {
        audioSource.clip = wepAttk;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(damage);
            Instantiate(bloodSplatter, transform.position + (transform.forward * 1), transform.rotation);
            audioSource.Play();
        }
    }
}
