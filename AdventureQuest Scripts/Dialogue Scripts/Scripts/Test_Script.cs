using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Script : MonoBehaviour
{
    //This is where/how we trigger the dialogue

    public DialogueBase dialogue;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.EnqueueDialogue(dialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialogue();
        }
    }
}
