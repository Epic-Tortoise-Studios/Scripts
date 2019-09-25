using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnlocker : MonoBehaviour
{
    public GameObject levelOneDoor;
    public GameObject levelTwoDoor;
    public GameObject levelThreeDoor;

    private bool hasRed;
    private bool hasBlue;
    private bool hasGreen;

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (LevelManager.Instance.levelOneComplete)
        {
            Debug.Log("Open Level One Door");

            levelOneDoor.GetComponent<Animator>().SetBool("Opened", true);

            if(sceneName == "MansionHub" && !hasRed)
            {
                PlayerController.Instance.exclaimed = false;
                hasRed = true;
            }
        }

        if (LevelManager.Instance.levelTwoComplete)
        {
            Debug.Log("Open Level Two Door");

            levelTwoDoor.GetComponent<Animator>().SetBool("Opened", true);

            if (sceneName == "MansionHub" && !hasBlue)
            {
                PlayerController.Instance.exclaimed = false;
                hasBlue = true;
            }
        }

        if (LevelManager.Instance.levelThreeComplete)
        {
            Debug.Log("Open Level Three Door");

            levelThreeDoor.GetComponent<Animator>().SetBool("Opened", true);

            if (sceneName == "MansionHub" && !hasGreen)
            {
                PlayerController.Instance.exclaimed = false;
                hasGreen = true;
            }
        }

    }
}
