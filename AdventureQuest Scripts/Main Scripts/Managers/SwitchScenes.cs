using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{

    private string sceneName;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(sceneName == "Milestone2")
            {
                SceneManager.LoadScene("Metrics Level");
                GameManager.Instance.CursorUnlock();
            }
            else if(sceneName == "Metrics Level")
            {
                SceneManager.LoadScene("Milestone2");
                GameManager.Instance.CursorUnlock();
            }
        }
    }

}
