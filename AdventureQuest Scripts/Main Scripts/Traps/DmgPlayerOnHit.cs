using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgPlayerOnHit : MonoBehaviour
{
    public float damage;
    private bool isActive;

    void Start()
    {
        isActive = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(!isActive)
            {
                isActive = true;
                StartCoroutine(Damage());
            }
        }
    }

    IEnumerator Damage()
    {
        PlayerStats.Instance.TakeDamage(damage);
        yield return new WaitForSeconds(1);
        isActive = false;
    }
}
