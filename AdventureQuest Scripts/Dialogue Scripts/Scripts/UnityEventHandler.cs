using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventHandler : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent eventHandler;
    public DialogueBase myDialogue;

    //This is what happens when you click on a button
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        eventHandler.Invoke();
        DialogueManager.Instance.CloseOptions();
        DialogueManager.Instance.inDialogue = false;

        if(myDialogue != null)
        {
            DialogueManager.Instance.EnqueueDialogue(myDialogue);
        }
    }
}
