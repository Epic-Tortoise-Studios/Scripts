using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAnimator : MonoBehaviour
{

    public GameObject target;
    public bool canShoot;
    public float resetTime;
    public float chargingTime;
    public GameObject Wisp;


    // Start is called before the first frame update
    void Start()
    {
        Wisp = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxSpawns/Wisp");
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


    public void Shooting()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
            transform.LookAt(targetPosition);
            if (canShoot)
            {
                canShoot = false;
                Instantiate(Wisp, transform.position + transform.forward * 1, transform.rotation);
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

