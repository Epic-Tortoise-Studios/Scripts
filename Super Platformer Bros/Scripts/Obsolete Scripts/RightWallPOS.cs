using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RightWallPOS : MonoBehaviour
{
    private GameMaster gm;

    void Start()
    {
        //Calls GameMaster to get last POS and spawns it there.
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //transform.position = gm.rightWallPos;
    }

    void Update()
    {
        //Set this to the restart menu button on death later.  Currently for testing
        if (Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
