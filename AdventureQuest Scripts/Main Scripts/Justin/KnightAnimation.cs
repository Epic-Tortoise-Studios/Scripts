using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimation : MonoBehaviour
{
    private bool canShoot;
    public GameObject target;
    public GameObject slimeShot;
    public float resetTime;

    public AudioSource Charging;
    public AudioSource Shooting;
    public GameObject Charginglocation;
    public GameObject Shootinglocation;


    void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        shoot();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    void shoot()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
            transform.LookAt(targetPosition);
            if (canShoot)
            {
                canShoot = false;
                Instantiate(slimeShot, transform.position + transform.forward * 1, transform.rotation);
                StartCoroutine(Reset());
            }
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        canShoot = true;
    }
}