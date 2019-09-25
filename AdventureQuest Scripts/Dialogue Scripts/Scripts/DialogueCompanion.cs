using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCompanion : MonoBehaviour
{

   /* private MeleeStates meleeStates;
    public GameObject meleeStateScript;
    public GameObject melee;

    public DialogueBase followDialogue;
    public DialogueBase waitDialogue;
    public DialogueBase aggressiveDialogue;
    public DialogueBase passiveDialogue;
    public DialogueBase defenderDialogue;

    private DialogueManager dialogueManager;
    private UIManager UIManager;
    private MeleeMovement meleeMovement;
    private GameObject currentCompanion;


    private GameObject companionUI;
    private GameObject debugUI;


    public float dialogueTime;
    private float waitTime;

    public bool companionDialogue;

    [SerializeField] private bool alreadyFollow;
    [SerializeField] private bool alreadyWait;
    [SerializeField] private bool alreadyAggressive;
    [SerializeField] private bool alreadyPassive;
    [SerializeField] private bool alreadyDefender;

    private void Start()
    {
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        UIManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        companionUI = GameObject.FindGameObjectWithTag("CompanionUI");

        currentCompanion = this.gameObject;

        //meleeStateScript = GameObject.FindGameObjectWithTag("MeleeStateMachine");
        //meleeStates = melee.GetComponent<MeleeStates>();

        melee = GameObject.FindGameObjectWithTag("MeleeCompanion");
        meleeMovement = melee.GetComponent<MeleeMovement>();
        meleeStates = melee.GetComponent<MeleeStates>();

        waitTime = dialogueTime;
    }


    private void Update()
    {
        StateDialogueTrigger();
    }


    void StateDialogueTrigger()
    {
        if(meleeStates.isActive == true && meleeMovement.isActive == true)
        {
            if (meleeMovement.wait == false)
            {
                if (alreadyFollow == false && alreadyAggressive == false && alreadyDefender == false && alreadyPassive == false)
                {
                    DialogueManager.instance.EnqueueDialogue(followDialogue);
                    waitTime -= Time.deltaTime;

                    if (dialogueManager.isCurrentlyTyping == false && waitTime <= 0 || Input.GetKeyDown(KeyCode.Space))
                    {
                        DialogueManager.instance.DequeueDialogue();
                        waitTime = dialogueTime;
                        alreadyFollow = true;
                        alreadyWait = false;


                    }
                }
            }
            else if (meleeMovement.wait)
            {
                if (alreadyWait == false)
                {
                    DialogueManager.instance.EnqueueDialogue(waitDialogue);
                    waitTime -= Time.deltaTime;

                    if (dialogueManager.isCurrentlyTyping == false && waitTime <= 0 || Input.GetKeyDown(KeyCode.Space))
                    {
                        DialogueManager.instance.DequeueDialogue();
                        waitTime = dialogueTime;
                        alreadyFollow = false;
                        alreadyWait = true;
                        alreadyAggressive = false;
                        alreadyPassive = false;
                        alreadyDefender = false;


                    }
                }

            }


            if (meleeMovement.aggressive && meleeMovement.follow)
            {
                if (alreadyAggressive == false && alreadyFollow)
                {
                    DialogueManager.instance.EnqueueDialogue(aggressiveDialogue);
                    waitTime -= Time.deltaTime;



                    if (dialogueManager.isCurrentlyTyping == false && waitTime <= 0 || Input.GetKeyDown(KeyCode.Space))
                    {
                        DialogueManager.instance.DequeueDialogue();
                        waitTime = dialogueTime;
                        alreadyFollow = true;
                        alreadyWait = false;
                        alreadyAggressive = true;
                        alreadyPassive = false;
                        alreadyDefender = false;


                    }
                }
            }
            else if (meleeMovement.defender && meleeMovement.follow)
            {
                if (alreadyDefender == false && alreadyFollow)
                {
                    DialogueManager.instance.EnqueueDialogue(defenderDialogue);
                    waitTime -= Time.deltaTime;



                    if (dialogueManager.isCurrentlyTyping == false && waitTime <= 0 || Input.GetKeyDown(KeyCode.Space))
                    {
                        DialogueManager.instance.DequeueDialogue();
                        waitTime = dialogueTime;
                        alreadyFollow = true;
                        alreadyWait = false;
                        alreadyAggressive = false;
                        alreadyPassive = false;
                        alreadyDefender = true;


                    }
                }
            }
            else if (meleeMovement.passive && meleeMovement.follow)
            {
                if (alreadyPassive == false && alreadyFollow)
                {
                    DialogueManager.instance.EnqueueDialogue(passiveDialogue);
                    waitTime -= Time.deltaTime;


                    if (dialogueManager.isCurrentlyTyping == false && waitTime <= 0 || Input.GetKeyDown(KeyCode.Space))
                    {
                        DialogueManager.instance.DequeueDialogue();
                        waitTime = dialogueTime;
                        alreadyFollow = true;
                        alreadyWait = false;
                        alreadyAggressive = false;
                        alreadyPassive = true;
                        alreadyDefender = false;


                    }
                }

            }
        }
    }*/


}
