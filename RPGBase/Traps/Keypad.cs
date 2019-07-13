using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public string curPW = "789";
    public string input;
    public bool onTrigger;
    public bool doorOpened;
    public bool keypadScreen;
    public Transform doorHinge;

    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        keypadScreen = false;
        input = "";
    }

    void Update()
    {
        if(input == curPW)
        {
            doorOpened = true;
        }

        if(doorOpened)
        {
            var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, -70.0f, 0.0f), Time.deltaTime * 250);
            doorHinge.rotation = newRot;
        }
    }

    void OnGUI()
    {
        if(!doorOpened)
        {
            if (onTrigger)
            {
                GUI.Box(new Rect(215, 525, 320, 35), "Press 'E' to enter code");

                if(Input.GetKeyDown(KeyCode.E))
                {
                    keypadScreen = true;
                    onTrigger = false;
                }
            }

            if (keypadScreen)
            {
                GUI.Box(new Rect(0, 0, 320, 465), "");
                GUI.Box(new Rect(5, 5, 310, 25), input);

                if (GUI.Button(new Rect(5, 35, 100, 100), "1"))
                {
                    input = input + "1";
                }
                if (GUI.Button(new Rect(110, 35, 100, 100), "2"))
                {
                    input = input + "2";
                }
                if (GUI.Button(new Rect(215, 35, 100, 100), "3"))
                {
                    input = input + "3";
                }
                if (GUI.Button(new Rect(5, 140, 100, 100), "4"))
                {
                    input = input + "4";
                }
                if (GUI.Button(new Rect(110, 140, 100, 100), "5"))
                {
                    input = input + "5";
                }
                if (GUI.Button(new Rect(215, 140, 100, 100), "6"))
                {
                    input = input + "6";
                }
                if (GUI.Button(new Rect(5, 245, 100, 100), "7"))
                {
                    input = input + "7";
                }
                if (GUI.Button(new Rect(110, 245, 100, 100), "8"))
                {
                    input = input + "8";
                }
                if (GUI.Button(new Rect(215, 245, 100, 100), "9"))
                {
                    input = input + "9";
                }
                if (GUI.Button(new Rect(110, 350, 100, 100), "0"))
                {
                    input = input + "0";
                }
            }
        }
    }
}
