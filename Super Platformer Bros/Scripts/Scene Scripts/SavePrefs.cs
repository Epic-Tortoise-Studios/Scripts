using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePrefs : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameMaster gameMaster;
    

    public int loadedHealth;
    public int loadedLevel;

    public GameObject thePlayer;
    public float xPos;
    public float yPos;
    public float zPos;

    public Vector3 loadedPosition;

    public GameObject continueButton;
    public GameObject playButton;
    



    void Start()
    {
        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        loadedHealth = PlayerPrefs.GetInt("Player Health");
        loadedLevel = PlayerPrefs.GetInt("Level Number");

       /* xPos = PlayerPrefs.GetFloat("XPosition");
        yPos = PlayerPrefs.GetFloat("YPosition");
        zPos = PlayerPrefs.GetFloat("ZPosition");*/

        if (PlayerPrefs.GetInt("First Time", 1) == 1)
        {
            Debug.Log("First Time Opening");

            PlayerPrefs.SetInt("First Time", 0);
            playerHealth.currentHealth = playerHealth.maxHealth;
            PlayerPrefs.SetInt("Player Health", playerHealth.currentHealth);

            if (playButton != null && continueButton != null)
            {
                playButton.SetActive(true);
                continueButton.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Not First Time Opening");

            if(playButton != null && continueButton != null)
            {
                playButton.SetActive(false);
                continueButton.SetActive(true);
            }
        }

        //  thePlayer.transform.position = new Vector3(xPos, yPos, zPos);
    }


    void Update()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        /*xPos = thePlayer.transform.position.x;
        yPos = thePlayer.transform.position.y;
        zPos = thePlayer.transform.position.z;

        loadedPosition = new Vector3(xPos, yPos, zPos);*/
    }

    public void SaveButton()
    {
        PlayerPrefs.SetInt("Player Health", playerHealth.currentHealth);
        Debug.Log(PlayerPrefs.GetInt("Player Health"));

        PlayerPrefs.SetInt("Level Number", SceneManager.GetActiveScene().buildIndex);
        Debug.Log(PlayerPrefs.GetInt("Level Number"));

        xPos = gameMaster.playerLastPos.x;
        yPos = gameMaster.playerLastPos.y;
        zPos = gameMaster.playerLastPos.z;

        PlayerPrefs.SetFloat("XPosition", xPos);
        PlayerPrefs.SetFloat("YPosition", yPos);
        PlayerPrefs.SetFloat("ZPosition", zPos);

        loadedPosition = new Vector3(xPos, yPos, zPos);
        Debug.Log("Saved Position: " + loadedPosition);
        
        PlayerPrefs.Save();

    }

    public void LoadButton()
    {
        Time.timeScale = 1f;

        loadedHealth = PlayerPrefs.GetInt("Player Health");
        loadedLevel = PlayerPrefs.GetInt("Level Number");

        playerHealth.currentHealth = loadedHealth;
        SceneManager.LoadScene(loadedLevel);

        PlayerPrefs.GetFloat("XPosition", xPos);
        PlayerPrefs.GetFloat("YPosition", yPos);
        PlayerPrefs.GetFloat("ZPosition", zPos);

        loadedPosition = new Vector3(xPos, yPos, zPos);
        thePlayer.transform.position = loadedPosition;
        Debug.Log("Loaded Position: " + loadedPosition);      

        Debug.Log("Loaded Health At: " + loadedHealth + " " + "And Loaded Scene At Number: " + loadedLevel);
    }

    public void WipeSaveButton()
    {
        PlayerPrefs.DeleteAll();
        if(playButton != null && continueButton != null)
        {
            playButton.SetActive(true);
            continueButton.SetActive(false);
        }

        Debug.Log("Health And Level: " + PlayerPrefs.GetInt("Player Health") + PlayerPrefs.GetInt("Level Number") + " Loaded Position: " + PlayerPrefs.GetFloat("XPosition") + PlayerPrefs.GetFloat("YPosition") + PlayerPrefs.GetFloat("ZPosition"));
        PlayerPrefs.Save();
    }

}
