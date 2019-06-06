using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerPOS : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        //Calls GameMaster script to get respawn POS
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.playerLastPos;
        //transform.position = gm.initialPlayerPos;
    }

    //This was for testing
    /*void Update()
    {
        //Set this to the restart menu.  For now is testing purpose hit "K" to test respawn.
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }*/

    public void InitialPOS()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GetComponent<PauseMenu>().Resume();
    }
}
