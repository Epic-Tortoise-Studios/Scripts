using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject switchCamera;
    private GameObject playerCamera;

    public float waitTime;

    private bool triggeredCheck = false;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!triggeredCheck)
            {
                StartCoroutine(Switch());
                triggeredCheck = true;
            }
        }
    }

    IEnumerator Switch()
    {
        playerCamera.SetActive(false);
        PlayerController.Instance.canMove = false;
        switchCamera.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        playerCamera.SetActive(true);
        PlayerController.Instance.canMove = true;
        switchCamera.SetActive(false);
    }
}
