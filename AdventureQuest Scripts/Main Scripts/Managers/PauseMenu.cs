using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
  /*  private PlayerController playerController;
    private CameraController cameraController;

    private Canvas pauseCanvas;
    private GameObject playerUI;
    private GameObject dialogueUI;
    private GameObject companionUI;
    public GameObject debugUI;

    public bool GameIsPaused = false;
    public bool stopUpdate;

    public GameObject pauseMenu;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();

        pauseCanvas = this.gameObject.GetComponent<Canvas>();

        playerUI = GameObject.FindGameObjectWithTag("UI");
        dialogueUI = GameObject.FindGameObjectWithTag("DialogueUI");
        companionUI = GameObject.FindGameObjectWithTag("CompanionUI");
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
             if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        NoOtherMenus();
    }

    public void Resume ()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseCanvas.sortingOrder = -10;
    }

    void Pause ()
    {  
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseCanvas.sortingOrder = 50;
    }

    public void NoOtherMenus()
    {
        if (GameIsPaused)
        {
            playerUI.SetActive(false);
            dialogueUI.SetActive(false);
            companionUI.SetActive(false);

            cameraController.lockCursor = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


            playerController.canMove = false;
            stopUpdate = true;
        }
        else if (stopUpdate)
        {
            playerUI.SetActive(true);
            dialogueUI.SetActive(true);
            companionUI.SetActive(true);

            cameraController.lockCursor = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            playerController.canMove = true;
            stopUpdate = false;
        }
    }*/
}
