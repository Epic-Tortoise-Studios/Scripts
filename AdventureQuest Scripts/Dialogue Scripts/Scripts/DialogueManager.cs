using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{

    #region Singleton
    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DialogueManager>();
            return instance;
        }
    }
    #endregion

    public GameObject dialogueBox;
    //Added this 6/5/19
    public GameObject dialogueOptionBox;
    //Added this 6/7/19
    //public GameObject dialogueCompanionBox;
    //Added this 6/14/19
    public GameObject inputText;
    //Added this 6/22/19
    //public Canvas dialogueCanvas;

    public TMPro.TextMeshProUGUI dialogueName;
    public TMPro.TextMeshProUGUI dialogueText;
    public Image dialoguePortrait;
    public float delay = 0.001f;
    //Added this 6/4/19
    public bool endedDialogue;

    //New
    public bool isCurrentlyTyping;
    private string completeText;

    //First In First Out Collection
    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();

    //Added this 6/5/19 - Option Stuff
    public bool isDialogueOptions;
    public bool inDialogue;
    private int optionsAmount;
    public TMPro.TextMeshProUGUI questionText;

    public GameObject[] optionButtons;

    private bool showCursor;

    //For UI Management
    HideOnDialogue[] hideWhileDialogue;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Must be Null!" + gameObject.name);
        }
        else
        {
            instance = this;
        }

        inputText = GameObject.FindGameObjectWithTag("InputText");
        inputText.SetActive(false);

        hideWhileDialogue = FindObjectsOfType<HideOnDialogue>();

    }

    //Added this 6/22/19
    private void Update()
    {
        /*if (inDialogue)
        {
            dialogueCanvas.sortingOrder = 50;
        }
        else
        {
            dialogueCanvas.sortingOrder = -10;
        }*/
    }

    public void EnqueueDialogue(DialogueBase db)
    {
        //Added 6/5/19
        if (inDialogue) return;
        inDialogue = true;

        //Clears all past dialogue queues
        dialogueInfo.Clear();

        dialogueBox.SetActive(true);

        OptionsParser(db);

        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        //Added For UI Management 7/6/2019
        foreach (HideOnDialogue h in hideWhileDialogue)
        {
            h.gameObject.SetActive(false);
        }

        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        //6/5/19
        if (isCurrentlyTyping)
        {
            CompleteText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;
        }
        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();
        completeText = info.npcText;


        dialogueName.text = info.character.myName;
        dialoguePortrait.sprite = info.character.myPortrait;
        dialogueText.text = info.npcText;


        //6/5/19
        dialogueText.text = "";

        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        isCurrentlyTyping = true;
        foreach(char c in info.npcText.ToCharArray())
        {
            yield return new WaitForSeconds(delay);
            dialogueText.text += c;

            //6/6/19
            //AudioManager.instance.audioSource = gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(info.character.myVoice);

        }
        isCurrentlyTyping = false;
    }

    //Added 6/5/19
    private void CompleteText()
    {
        dialogueText.text = completeText;
    }

    public void EndOfDialogue()
    {
        dialogueBox.SetActive(false);
        //Added For UI Management 7/6/2019
        foreach (HideOnDialogue h in hideWhileDialogue)
        {
            h.gameObject.SetActive(true);
        }
        OptionsLogic();
    }

    private void OptionsParser(DialogueBase db)
    {
        if (db is DialogueOptions)
        {
            isDialogueOptions = true;


            DialogueOptions dialogueOptions = db as DialogueOptions;
            optionsAmount = dialogueOptions.optionsInfo.Length;

            questionText.text = dialogueOptions.questionText;
            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);
            }
            for (int i = 0; i < optionsAmount; i++)
            {
                optionButtons[i].SetActive(true);
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = dialogueOptions.optionsInfo[i].buttonText;
                UnityEventHandler myEventHandler = optionButtons[i].GetComponent<UnityEventHandler>();
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;

                if (dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }
        }
        else
        {
            isDialogueOptions = false;
        }
    }

    private void OptionsLogic()
    {
        if (isDialogueOptions)
        {
            dialogueOptionBox.SetActive(true);

            //Added For UI Management 7/6/2019
            foreach (HideOnDialogue h in hideWhileDialogue)
            {
                h.gameObject.SetActive(false);
            }
        }
        else
        {
            inDialogue = false;

            //Added For UI Management 7/6/2019
            foreach (HideOnDialogue h in hideWhileDialogue)
            {
                h.gameObject.SetActive(true);
            }
        }

        //Added 6/5/19 for rpg project
        /*if (dialogueOptionBox.activeInHierarchy == true)
        {
            playerMovement.canMove = false;
        }*/
    }

    public void CloseOptions()
    {
        dialogueOptionBox.SetActive(false);

        //Added For UI Management 7/6/2019
        foreach (HideOnDialogue h in hideWhileDialogue)
        {
            h.gameObject.SetActive(true);
        }
    }

    //Trigger Dialogue Functions 8/19/2019
    public void TriggerDialogue(DialogueBase dialogue)
    {
        EnqueueDialogue(dialogue);
    }

}
