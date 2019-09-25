using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Options Dialogue", menuName = "Options Dialogue", order = -50)]
public class DialogueOptions : DialogueBase
{

    [TextArea(2, 10)]
    public string questionText;

    [System.Serializable]
    public class Options
    {

        public string buttonText;
        public DialogueBase nextDialogue;
        public UnityEvent myEvent;
    }

    public Options[] optionsInfo;

}
