using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject blockTrigger0;
    public GameObject blockTrigger1;
    public GameObject blockTrigger2;
    public GameObject blockTrigger3;
    private bool isActive;

    public GameObject objectToMake;
    public GameObject unlockCamera;
    public GameObject playerCamera;
    
    void Start()
    {
        isActive = false;
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
    
    void Update()
    { 
        if(blockTrigger0 == null && blockTrigger1 == null && blockTrigger2 == null && blockTrigger3 == null && !isActive)
        {
            isActive = true;
            Instantiate(objectToMake, transform.position + transform.up * 0, transform.rotation);
            StartCoroutine(Unlock());
        }
    }

    IEnumerator Unlock()
    {
        yield return new WaitForSeconds(2);
        playerCamera.SetActive(false);
        unlockCamera.SetActive(true);
        PlayerController.Instance.canMove = false;
        yield return new WaitForSeconds(2);
        playerCamera.SetActive(true);
        unlockCamera.SetActive(false);
        PlayerController.Instance.canMove = true;
    }
}
