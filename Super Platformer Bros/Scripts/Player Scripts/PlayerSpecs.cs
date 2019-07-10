using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSpecs : MonoBehaviour
{
    public int level = 4;
    public int health = 50;
    public PlayerHealth playerHealth;
    //public GameMaster checkPoint;

    public void Start()
    {
        playerHealth = this.gameObject.GetComponent<PlayerHealth>();

     //   checkPoint = this.gameObject.GetComponent<GameMaster>();

    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);



        health = playerHealth.currentHealth;

        level = SceneManager.GetActiveScene().buildIndex;

        //PlayerPrefs.SetInt("SaveScene", level);
       // level = checkPoint.playerLastPos;
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;
    }
}
