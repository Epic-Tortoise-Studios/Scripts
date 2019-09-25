using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgression : MonoBehaviour
{
    private string sceneName;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.LoadScene("MansionHub");

            if (sceneName == "Spooky_Mansion_01_Foster")
            {
                SceneManager.LoadScene("Spooky_Mansion_02_Foster");
            }
            else if (sceneName == "Spooky_Mansion_02_Foster")
            {
                SceneManager.LoadScene("Spooky_Mansion_03_Foster");
            }
            else if (sceneName == "Spooky_Mansion_03_Foster")
            {
                SceneManager.LoadScene("WinScene");
            }

        }
    }
}
