using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeftWallPOS : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        //Calls GameMaster to get last Left Wall POS for respawn
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //transform.position = gm.leftWallPos;
    }

    void Update()
    {
        //Set this to the restart menu button on death later. Currently testing purposes.
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
