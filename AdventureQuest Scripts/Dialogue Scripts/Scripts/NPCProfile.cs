using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Profile", menuName = "Character Profile", order = -50)]
public class NPCProfile : ScriptableObject
{
    public string myName;
    public Sprite myPortrait;
    public AudioClip myVoice;
}
