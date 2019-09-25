using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    #region Singleton
    private static CutsceneManager instance;
    public static CutsceneManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CutsceneManager>();
            return instance;
        }
    }
    #endregion

    public GameObject playerCamera;
    public GameObject cutToCamera;
    public GameObject cutToCamera02;
    public GameObject cutsceneObject01;
    public GameObject cutsceneObject02;
    public GameObject animationHelper;

    private bool skip;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }


    void Update()
    {
        
    }

    public void SkipIntroCutscene()
    {
        playerCamera.SetActive(true);
        DialogueManager.Instance.DequeueDialogue();

        if (cutToCamera02.activeInHierarchy == true)
        {
            cutToCamera02.SetActive(false);
        }

        if (cutToCamera.activeInHierarchy == true)
        {
            cutToCamera.SetActive(false);
        }

        PlayerController.Instance.canMove = true;
        GameManager.Instance.CursorLock();
    }

    public void SkipButton()
    {
        skip = true;
    }
}
