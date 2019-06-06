using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRespawn : MonoBehaviour
{
    public float respawnTimer;
    public float fireballRespawnTimer;

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Respawn());
        }

        else if (other.gameObject.name == "FireBall(Clone)")
        {
            StartCoroutine(FireballRespawnTimer());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);
        this.gameObject.GetComponent<Renderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator FireballRespawnTimer()
    {
        yield return new WaitForSeconds(fireballRespawnTimer);
        this.gameObject.GetComponent<Renderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
