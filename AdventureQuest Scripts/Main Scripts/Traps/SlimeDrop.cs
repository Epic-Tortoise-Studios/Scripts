
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDrop : MonoBehaviour
{
    public float damage;
    public float destroyTime;
    
    void Start()
    {
        StartCoroutine(Destroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats.Instance.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}
