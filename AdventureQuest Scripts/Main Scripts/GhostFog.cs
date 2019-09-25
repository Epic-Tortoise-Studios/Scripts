using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFog : MonoBehaviour
{
    public float slowedWalkSpeed;
    public float slowedSprintSpeed;

    private float savedWalkSpeed;
    private float savedSprintSpeed;


    void Start()
    {
        savedWalkSpeed = PlayerController.Instance.walkSpeed;
        savedSprintSpeed = PlayerController.Instance.sprintSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(TransformationController.Instance.type == TransformationController.TransformationType.HUMAN)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerController.Instance.walkSpeed = slowedWalkSpeed;
                PlayerController.Instance.sprintSpeed = slowedSprintSpeed;
                PlayerController.Instance.canDash = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (TransformationController.Instance.type == TransformationController.TransformationType.HUMAN)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerController.Instance.walkSpeed = savedWalkSpeed;
                PlayerController.Instance.sprintSpeed = savedSprintSpeed;
                PlayerController.Instance.canDash = true;
            }
        }
    }
}
