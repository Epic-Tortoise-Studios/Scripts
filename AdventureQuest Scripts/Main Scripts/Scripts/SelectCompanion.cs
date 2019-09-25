using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCompanion : MonoBehaviour
{
    //private CameraController cameraController;
    [Header("MeleeCompanion")]
    public MeleeStates meleeStates;
    private CompMoveTest compMoveTest;
    public GameObject meleeCompanion;

    [Header("CasterCompanion")]
    public CasterStates casterStates;
    private CasterMovement casterMovement;
    public GameObject casterCompanion;
    //public GameObject healerTrigger;

    [Header("NecroCompanion")]
    public NecroStates necroStates;
    private NecroMovement necroMovement;
    public GameObject necroCompanion;


    [Header("CompanionUI")]
    public GameObject companionPanel;

    void Start()
    {
        //cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        meleeCompanion = GameObject.FindGameObjectWithTag("MeleeCompanion");
        compMoveTest = meleeCompanion.GetComponent<CompMoveTest>();
        //meleeStates = meleeCompanion.GetComponent<MeleeStates>();

        casterCompanion = GameObject.FindGameObjectWithTag("CasterCompanion");
        casterMovement = casterCompanion.GetComponent<CasterMovement>();
        //healerStates = healerCompanion.GetComponent<CompanionStates>();

        necroCompanion = GameObject.FindGameObjectWithTag("NecroCompanion");
        necroMovement = necroCompanion.GetComponent<NecroMovement>();

        //companionPanel.SetActive(false);

        //Healer Companion

        //Tank Companion
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            companionPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            companionCanvas.sortingOrder = 20;
            cameraController.lockCursor = false;
        }
    }*/

    public void SelectMelee()
    {
        meleeStates.isActive = true;
        //meleeMovement.isActive = true;
        compMoveTest.wait = false;
        compMoveTest.passive = true;

        casterStates.isActive = false;
        //casterMovement.isActive = false;
        casterMovement.wait = true;
        casterMovement.passive = false;

        companionPanel.SetActive(true);
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        companionCanvas.sortingOrder = 0;

        cameraController.lockCursor = true;*/
    }

    public void SelectCaster()
    {
        casterStates.isActive = true;
        //casterMovement.isActive = true;
        casterMovement.wait = false;
        casterMovement.passive = true;
        //StartCoroutine(HealerFollowing());

        meleeStates.isActive = false;
        //meleeMovement.isActive = false;
        compMoveTest.passive = false;
        compMoveTest.wait = true;

        companionPanel.SetActive(true);
        /*companionPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        companionCanvas.sortingOrder = 0;

        cameraController.lockCursor = true;*/
    }

    public void SelectNecro()
    {
        necroStates.isActive = true;
        necroMovement.wait = false;
        necroMovement.passive = true;

        meleeStates.isActive = false;
        compMoveTest.passive = false;
        compMoveTest.wait = true;

        casterStates.isActive = false;
        casterMovement.wait = true;
        casterMovement.passive = false;

        companionPanel.SetActive(true);
    }

    /*public void Resume()
    {
        companionPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        companionCanvas.sortingOrder = 0;

        cameraController.lockCursor = true;
    }

    public IEnumerator MeleeFollowing()
    {
        yield return new WaitForSeconds(3);
        meleeTrigger.SetActive(false);
        healerTrigger.SetActive(true);
        Debug.Log("Should turn off melee trigger");
    }
    public IEnumerator HealerFollowing()
    {
        yield return new WaitForSeconds(3);
        healerTrigger.SetActive(false);
        meleeTrigger.SetActive(true);
        Debug.Log("Should turn off healer trigger");

    }*/
}
