using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    public GameObject attachedItem;

    private Rigidbody rb;

    void Start()
    {
        rb = attachedItem.GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            rb.constraints = RigidbodyConstraints.None;
            Destroy(this.gameObject);


        }
    }
}
