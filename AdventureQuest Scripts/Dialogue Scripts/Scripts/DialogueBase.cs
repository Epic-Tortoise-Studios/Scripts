using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Basic Dialogue", order = -50)]
public class DialogueBase : ScriptableObject
{
    [System.Serializable]
    public class Info
    {
        public NPCProfile character;
        //public string npcName;
        //public Sprite npcPortrait;
        [TextArea(4, 8)]
        public string npcText;
    }

    [Header("Insert Dialogue Information Below")]
    public Info[] dialogueInfo;
}
