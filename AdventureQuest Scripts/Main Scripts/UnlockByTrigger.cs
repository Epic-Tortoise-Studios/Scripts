using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockByTrigger : MonoBehaviour
{
    [Header("Unlock Variables")]
    private GameObject playerCamera;
    public GameObject unlockCamera;
    public GameObject unlockObject;
    public AudioClip unlockAudio;

    private bool playerCheck;

    [Header("Optional")]
    public GameObject requirementObject;

    [Header("Light Puzzle")]
    public bool lightPuzzle;

    [Header("Push Box")]
    public GameObject pushBox;

    [Header("Chandelier")]
    private bool chandelierPuzzle;


    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }


    private void Update()
    {
        Debug.Log(playerCheck);
        if (playerCheck && lightPuzzle)
        {
            LightPuzzle();
        }

        if (chandelierPuzzle)
        {
            ChandelierPuzzle();
        }
    }


    #region Trigger Functions
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("PickupObject"))
        {
            StartCoroutine(Unlocked());

            if(pushBox != null)
            {
                pushBox.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
        else if (other.gameObject.CompareTag("Chandelier"))
        {
            chandelierPuzzle = true;
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            playerCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerCheck = false;
        }
    }
    #endregion

    #region Light Puzzle
    void LightPuzzle()
    {
        if (this.gameObject.name == "UnlockFenceTrigger")
        {
            if (requirementObject.GetComponent<LightPuzzle>().litCheck1)
            {
                StartCoroutine(Unlocked());
            }
        }
        else if (this.gameObject.name == "UnlockPathTrigger")
        {
            if (requirementObject.GetComponent<LightPuzzle>().litCheck2)
            {
                StartCoroutine(Unlocked());
            }
        }
        else if (this.gameObject.name == "UnlockSwitcherTrigger")
        {
            if (requirementObject.GetComponent<LightPuzzle>().litCheck3)
            {
                StartCoroutine(Unlocked());
            }
        }
        else if (this.gameObject.name == "UnlockSwitcher2Trigger")
        {
            if (requirementObject.GetComponent<LightPuzzle>().litCheck4)
            {
                StartCoroutine(Unlocked());
            }
        }

    }
    #endregion

    #region Chandelier Puzzle
    public void ChandelierPuzzle()
    {
        StartCoroutine(Unlocked());
    }
    #endregion

    IEnumerator Unlocked()
    {
        playerCamera.SetActive(false);
        PlayerController.Instance.canMove = false;
        unlockCamera.SetActive(true);
        unlockObject.GetComponent<Animator>().SetBool("Unlocked", true);
        yield return new WaitForSeconds(2);
        playerCamera.SetActive(true);
        PlayerController.Instance.canMove = true;
        unlockCamera.SetActive(false);
        this.gameObject.GetComponent<UnlockByTrigger>().enabled = false;

        if (chandelierPuzzle)
        {
            requirementObject.SetActive(false);
        }
    }
}
