using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject pickupObject;
    public bool holdingObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickupObject"))
        {
            pickupObject = other.gameObject;

            if(pickupObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                pickupObject.GetComponent<Rigidbody>().useGravity = false;
                pickupObject.transform.position = this.transform.position;
                pickupObject.transform.parent = this.transform;
                pickupObject.GetComponent<Rigidbody>().freezeRotation = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PickupObject"))
        {
            pickupObject.GetComponent<Rigidbody>().useGravity = true;
            pickupObject.transform.parent = null;
            pickupObject = null;
            pickupObject.GetComponent<Rigidbody>().freezeRotation = false;
        }
    }

    private void Update()
    {
        if(pickupObject != null)
        {
            holdingObject = true;
        }
        else
        {
            holdingObject = false;
        }
    }
    /*public GameObject handObject;
    public Transform handTransform;

    void OnMouseDown()
    {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = handTransform.position;
        this.transform.parent = handObject.transform;
    }

    private void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }*/
}
