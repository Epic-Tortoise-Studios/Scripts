using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueBase dialogue;
    private PlayerController playerController;
    private CameraController cameraController;
    //private UIManager UIManager;

    private GameObject companionUI;
    private GameObject debugUI;
    private GameObject inputText;

    public string tip;

    private bool inTrigger;
    public bool toolTip = false;
    public bool triggeredDialogue;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        companionUI = GameObject.FindGameObjectWithTag("CompanionUI");
        inputText = DialogueManager.Instance.inputText;
    }


    private void Update()
    {        
        PlayerInput();
        //GetNextLine();
    }

    //A funtion to begin the dialogue system
    public void TriggerDialogue()
    {
        DialogueManager.Instance.EnqueueDialogue(dialogue);
        inputText.SetActive(false);

    }

    //A function to continue with the queued dialogue on button press
    public void GetNextLine()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.Instance.DequeueDialogue();
        }
    }

    //Makes sure the conversation can only be had in the NPC's trigger volume
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTrigger = true;
            inputText.SetActive(true);
            PlayerController.Instance.playerExclaim.SetActive(true);
            AudioManager.instance.PlayClip(AudioManager.instance.exclaim);

            if (toolTip)
            {
                inputText.GetComponent<TMPro.TextMeshProUGUI>().text = tip;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTrigger = false;
            inputText.SetActive(false);
            inputText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press E To Interact";
            PlayerController.Instance.playerExclaim.SetActive(false);
        }
    }

    //Dialogue will start on key press once inside the NPC's trigger and stops player movement
    public void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && inTrigger)
        {
            TriggerDialogue();
            triggeredDialogue = true;

            PlayerController.Instance.canMove = false;
            GameManager.Instance.CursorUnlock();

            PlayerController.Instance.anim.SetFloat("speed", 0);
            if (playerController.anim.GetBool("isRunning"))
            {
                playerController.anim.SetBool("isRunning", false);
            }

            PlayerController.Instance.playerExclaim.SetActive(false);
        }
        else if (triggeredDialogue && DialogueManager.Instance.dialogueBox.activeInHierarchy == false && DialogueManager.Instance.dialogueOptionBox.activeInHierarchy == false)
        {
            //Added 6/6/19
            DialogueManager.Instance.isDialogueOptions = false;
            triggeredDialogue = false;

            PlayerController.Instance.canMove = true;
            GameManager.Instance.CursorLock();

        }
    }

}
