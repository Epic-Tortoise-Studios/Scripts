using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestructable : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.AddForce(transform.forward * 5f);
            rb.mass = .01f;
            rb.constraints = RigidbodyConstraints.None;
            StartCoroutine(Destroy());
            Debug.Log("Hit as Player");
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
