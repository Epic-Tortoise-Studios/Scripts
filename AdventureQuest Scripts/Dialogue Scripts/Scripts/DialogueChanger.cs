using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChanger : MonoBehaviour
{

    public DialogueBase redHint;
    public DialogueBase blueHint;
    public DialogueBase greenHint;

    private bool redKeyHint = true;
    private bool blueKeyHint = true;
    private bool greenKeyHint = true;

    void Start()
    {
        
    }


    void Update()
    {
        if(LevelManager.Instance.levelOneComplete && redKeyHint)
        {
            gameObject.GetComponent<DialogueTrigger>().dialogue = redHint;
            redKeyHint = false;
        }
        else if(LevelManager.Instance.levelTwoComplete && blueKeyHint)
        {
            gameObject.GetComponent<DialogueTrigger>().dialogue = blueHint;
            blueKeyHint = false;
        }
        else if(LevelManager.Instance.levelThreeComplete && greenKeyHint)
        {
            gameObject.GetComponent<DialogueTrigger>().dialogue = greenHint;
            greenKeyHint = false;
        }
    }
}
