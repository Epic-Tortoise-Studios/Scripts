using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelOrb : MonoBehaviour
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
            if (this.gameObject.name == "RedKey")
            {
                LevelManager.Instance.levelOneComplete = true;
            }
            else if (this.gameObject.name == "BlueKey")
            {
                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.levelTwoComplete = true;

                    if (sceneName != "Spooky_Mansion_ReturnHub")
                    {
                        SceneManager.LoadScene("Spooky_Mansion_ReturnHub");
                    }
                }
                else
                {
                    SceneManager.LoadScene("Spooky_Mansion_02_Foster");
                }
            }
            else if (this.gameObject.name.Contains("GreenKey"))
            {
                if (LevelManager.Instance != null)
                {
                    LevelManager.Instance.levelThreeComplete = true;

                    if (sceneName != "Spooky_Mansion_ReturnHub")
                    {
                        SceneManager.LoadScene("Spooky_Mansion_ReturnHub");
                    }
                }
                else
                {
                    SceneManager.LoadScene("Spooky_Mansion_03_Foster");
                }
            }
        }
    }
}
