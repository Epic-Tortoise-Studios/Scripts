using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Transformation", menuName = "Transformation", order = 50)]
public class TransformationSO : ScriptableObject
{
    public Sprite sprite;

    public AudioClip transformAudio;

    public GameObject instantiateObj;

    public float cooldown;
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpForce;

    public Color lightColor;

    public KeyCode transformKey;

    public TransformationController.TransformationType type;
}
