using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public GameObject playerHand;
    public float speed ;

    void Start()
    {
        playerHand.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerHand.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && !gameObject.GetComponentInChildren<Pickup>().holdingObject)
        {
            playerHand.SetActive(false);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0) && gameObject.GetComponentInChildren<Pickup>().holdingObject)
        {
            gameObject.GetComponentInChildren<Pickup>().pickupObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponentInChildren<Pickup>().pickupObject.transform.parent = null;
            gameObject.GetComponentInChildren<Pickup>().pickupObject = null;
            playerHand.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && gameObject.GetComponentInChildren<Pickup>().pickupObject != null && gameObject.GetComponentInChildren<Pickup>().holdingObject)
        {
            gameObject.GetComponentInChildren<Pickup>().pickupObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponentInChildren<Pickup>().pickupObject.transform.parent = null;
            gameObject.GetComponentInChildren<Pickup>().pickupObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            gameObject.GetComponentInChildren<Pickup>().pickupObject = null;
            playerHand.SetActive(false);

            Debug.Log("Force");
        }
    }
}
