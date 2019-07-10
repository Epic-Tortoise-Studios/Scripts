using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToWin : MonoBehaviour
{
    public float timer = 5;
   

    public void StartTime()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
