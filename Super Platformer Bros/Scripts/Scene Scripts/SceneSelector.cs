using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public void LoadScene(string sLevel)
    {
        SceneManager.LoadScene(sLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Metrics()
    {
        SceneManager.LoadScene("Metrics");
    }

    public void Continue()
    {
        //SaveLoad.Load();
    }
}
