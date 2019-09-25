using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<LevelManager>();
            return instance;
        }
    }
    #endregion

    public bool levelOneComplete;
    public bool levelTwoComplete;
    public bool levelThreeComplete;

    public DialogueBase redDialogue;
    public DialogueBase blueDialogue;
    public DialogueBase greenDialogue;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LevelManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
