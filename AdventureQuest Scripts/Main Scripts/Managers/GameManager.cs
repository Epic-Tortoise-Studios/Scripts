using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Singleton
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }
    #endregion

    private GameObject player;

    public Vector3 initialPlayerPos;
    public Vector3 playerLastPos;

    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    public GameObject checkpoint4;
    public GameObject checkpoint5;
    public GameObject checkpoint6;

    public GameObject pauseMenu;
    public GameObject debugMenu;
    public GameObject transformMenu;

    public AudioClip positiveAudio;
    public AudioClip negativeAudio;

    HideOnPause[] hideWhilePaused;
    HideOnDebugging[] hideWhileDebugging;
    HideOnSelect[] hideWhileSelecting;
    [HideInInspector]
    public BossUICheck bossUI;

    private GameObject[] collectables;

    public int currentCollectableCount;
    private int totalSceneCollectables;
    private TMPro.TextMeshProUGUI collectableText;
    private TMPro.TextMeshProUGUI totalCollectableText;

    private bool paused;
    private bool debugging;
    private bool selecting;
    [HideInInspector]
    public bool levelComplete;
    [HideInInspector]
    public bool bossBattle;
    public bool cursorLocked;

    [HideInInspector]
    public float firstTime = 0;

    private void Awake()
    {
        hideWhilePaused = FindObjectsOfType<HideOnPause>();
        hideWhileDebugging = FindObjectsOfType<HideOnDebugging>();
        hideWhileSelecting = FindObjectsOfType<HideOnSelect>();

        bossUI = GameObject.FindObjectOfType<BossUICheck>();

        collectables = GameObject.FindGameObjectsWithTag("Collectible");
        collectableText = FindObjectOfType<GoldCountCheck>().GetComponent<TMPro.TextMeshProUGUI>();
        totalCollectableText = FindObjectOfType<TotalCollectableCheck>().GetComponent<TMPro.TextMeshProUGUI>();

        CursorLock();

        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneUnloaded(Scene current)
    {
        initialPlayerPos = new Vector3(0, 0, 0);
        firstTime += 1;
        totalSceneCollectables = 0;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Time.timeScale = 1;

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        collectables = GameObject.FindGameObjectsWithTag("Collectible");
        foreach (GameObject c in collectables)
        {
            totalSceneCollectables += 1;
        }
        totalCollectableText.text = "/ " + totalSceneCollectables.ToString();

        if (sceneName == "MansionHub")
        {
            totalCollectableText.text = null;
            GameObject introCutscene = GameObject.FindGameObjectWithTag("Cutscene");

            if (firstTime == 0)
            {
                //player.transform.position = new Vector3(0, 1.5f, -268.4f);
                if (introCutscene != null)
                {
                    introCutscene.SetActive(true);
                }
                Debug.Log("First Time");
            }
            else
            {
                //player.transform.position = new Vector3(0, 1.5f, -268.4f);
                if (introCutscene != null)
                {
                    introCutscene.SetActive(false);
                }
                Debug.Log("Second Time");
            }
        }
        else
        {
            currentCollectableCount = 0;
        }

        if (player != null)
        {
            initialPlayerPos = player.transform.position;
        }

        if (sceneName == "WinScene")
        {
            CursorUnlock();
            Debug.Log("At WinScene");
        }

        levelComplete = false;
    }

    private void Update()
    {
        #region Menu Controls
        //Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) && !debugging && !selecting)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                paused = true;
                ShowPaused();

                PlayerController.Instance.canMove = false;

                CursorUnlock();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                paused = false;
                HidePaused();

                PlayerController.Instance.canMove = true;

                CursorLock();
            }
        }

        //Debug Menu
        if (Input.GetKeyDown(KeyCode.BackQuote) && !paused && !selecting)
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                debugging = true;
                ShowDebug();

                PlayerController.Instance.canMove = false;

                CursorUnlock();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                debugging = false;
                HideDebug();

                PlayerController.Instance.canMove = true;

                CursorLock();
            }
        }
        #endregion

        CollectableCheck();
        collectableText.text = "Souls: " + currentCollectableCount;
        //Debug.Log("Total: " + totalSceneCollectables + " Current: " + currentCollectableCount);

        BossCheck();
    }

    #region PauseMenu
    public void PauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HidePaused();
        }
    }

    public void ShowPaused()
    {
        if(pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Missing PauseMenu Reference in GameManager. Fill Unless In Menu's.");
        }

        /*foreach (ShowOnPause g in pauseObjects)
        {
            g.gameObject.SetActive(true);
        }*/

        foreach (HideOnPause h in hideWhilePaused)
        {
            h.gameObject.SetActive(false);
        }


    }

    public void HidePaused()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Missing PauseMenu Reference in GameManager. Fill Unless In Menu's.");
        }

        /*foreach (ShowOnPause g in pauseObjects)
        {
            g.gameObject.SetActive(false);
        }*/

        foreach (HideOnPause h in hideWhilePaused)
        {
            h.gameObject.SetActive(true);
        }
    }
    #endregion

    #region DebugMenu
    public void DebugControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            ShowDebug();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            HideDebug();
        }
    }

    public void ShowDebug()
    {
        if (debugMenu != null)
        {
            debugMenu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Missing DebugMenu Reference in GameManager. Fill Unless In Menu's.");
        }

        /*foreach (ShowOnDebugging g in debugObjects)
        {
            g.gameObject.SetActive(true);
        }*/

        foreach (HideOnDebugging h in hideWhileDebugging)
        {
            h.gameObject.SetActive(false);
        }
    }

    public void HideDebug()
    {
        if (debugMenu != null)
        {
            debugMenu.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Missing DebugMenu Reference in GameManager. Fill Unless In Menu's.");
        }

        /*foreach (ShowOnDebugging g in debugObjects)
        {
            g.gameObject.SetActive(false);
        }*/

        foreach (HideOnDebugging h in hideWhileDebugging)
         {
            h.gameObject.SetActive(true);
        }
    }
    #endregion

    #region BossBattle
    void BossCheck()
    {
        if (bossBattle)
        {
            bossUI.gameObject.SetActive(true);
        }
        else
        {
            bossUI.gameObject.SetActive(false);
        }
    }
    #endregion

    #region CursorControl
    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cursorLocked = true;
    }

    public void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cursorLocked = false;
    }
    #endregion

    #region Buttons
    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1;
    }

    public void Resume()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            debugging = false;
            HideDebug();

            PlayerController.Instance.canMove = true;

            CursorLock();
        }
    }
    #endregion

    #region Pickups

    public void AddCollectableCount(int collectableToAdd)
    {
        currentCollectableCount += collectableToAdd;
        collectableText.text = "Souls: " + currentCollectableCount;
    }

    public void SubtractCollectableCount(int collectableToSubtract)
    {
        currentCollectableCount -= collectableToSubtract;
        collectableText.text = "Souls: " + currentCollectableCount;
    }

    private void CollectableCheck()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName != "MansionHub" && currentCollectableCount == totalSceneCollectables)
        {
            levelComplete = true;
            Debug.Log("Got All Collectables");
        }
    }
    #endregion
}
