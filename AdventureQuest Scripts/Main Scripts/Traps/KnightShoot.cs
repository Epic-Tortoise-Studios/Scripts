using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightShoot : MonoBehaviour
{
    private bool canShoot;
    public GameObject target;
    public GameObject slimeShot;
    public float resetTime;

    void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
            transform.LookAt(targetPosition);
            if (canShoot)
            {
                canShoot = false;
                Instantiate(slimeShot, transform.position + transform.forward * 1.5f, transform.rotation);
                StartCoroutine(Reset());
            }
        }
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

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        canShoot = true;
    }
}
