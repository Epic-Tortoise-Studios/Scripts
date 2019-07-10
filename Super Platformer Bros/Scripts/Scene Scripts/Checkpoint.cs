using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private GameMaster gm;
    private PlayerHealth playerHealth;
    public float xPos;
    public float yPos;
    public float zPos;

    public Vector3 loadedPosition;

    void Start()
    {
        //Calls the GameMaster script to store the values from below.
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void OnTriggerEnter(Collider other)
    {
        //This checks for player and sets new POS to trigger volume hit.
        if (other.CompareTag("Player"))
        {
            gm.playerLastPos = transform.position;

            PlayerPrefs.SetInt("Player Health", playerHealth.currentHealth);
            Debug.Log("Saved: " + PlayerPrefs.GetInt("Player Health"));

            PlayerPrefs.SetInt("Level Number", SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Saved: " + PlayerPrefs.GetInt("Level Number"));

            xPos = gm.playerLastPos.x;
            yPos = gm.playerLastPos.y;
            zPos = gm.playerLastPos.z;

            PlayerPrefs.SetFloat("XPosition", xPos);
            PlayerPrefs.SetFloat("YPosition", yPos);
            PlayerPrefs.SetFloat("ZPosition", zPos);

            loadedPosition = new Vector3(xPos, yPos, zPos);
            Debug.Log("Saved: " + loadedPosition);
        }
       /* else
            //These will check the tag when object hits trigger volume and set new POS to 
            //the trigger volume it hits.
            if (other.CompareTag("LWall"))
        {
            gm.leftWallPos = transform.position;
        }
        else
            if(other.CompareTag("RWall"))
        {
            gm.rightWallPos = transform.position;
        }*/
    }

}

