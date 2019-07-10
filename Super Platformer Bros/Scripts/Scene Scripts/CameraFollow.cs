using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public GameObject player;
    //private Vector3 offset;

    public GameObject followTarget;
    private Vector3 targetPosition;
    private float moveSpeed = 5;

    void Start()
    {
        followTarget = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);

        //transform.position = player.transform.position;
        //offset = transform.position - player.transform.position;
    }

    void Update()
    {
        //transform.position = player.transform.position + offset;
        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}

