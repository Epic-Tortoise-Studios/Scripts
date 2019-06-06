using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestruction : MonoBehaviour
{
    //Time to destroy is editable in inspector
    //This script goes on trigger volume, needs to be duplicate object inside/with it so player doesn't go through
    //Dublicate object needs to be child of the Trigger Volume

    public float destroyTimer;
    public float fireballDestroyTimer;

  public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(DestroyTimer());
        }

        else if(other.gameObject.name == "FireBall(Clone)")
        {
            StartCoroutine(FireballDestroyTimer());
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyTimer);
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator FireballDestroyTimer()
    {
        yield return new WaitForSeconds(fireballDestroyTimer);
        this.gameObject.GetComponent<Renderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
