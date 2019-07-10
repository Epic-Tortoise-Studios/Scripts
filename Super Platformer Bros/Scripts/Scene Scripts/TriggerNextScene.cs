using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TriggerNextScene : MonoBehaviour
{
    public float timer = 5;

    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            EndScene();
        }

    }

    public void EndScene()
    {
        SceneManager.LoadScene("WinScreen");
    }
}
