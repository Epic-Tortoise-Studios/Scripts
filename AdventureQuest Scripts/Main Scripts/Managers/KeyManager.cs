using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    #region Singleton
    private static KeyManager instance;
    public static KeyManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<KeyManager>();
            return instance;
        }
    }
    #endregion

    public Image redKeyImage;
    public Image blueKeyImage;
    public Image greenKeyImage;

    public bool hasRed;
    public bool hasBlue;
    public bool hasGreen;

    void Start()
    {
        redKeyImage = FindObjectOfType<RedKeyCheck>().GetComponent<Image>();
        blueKeyImage = FindObjectOfType<BlueKeyCheck>().GetComponent<Image>();
        greenKeyImage = FindObjectOfType<GreenKeyCheck>().GetComponent<Image>();

        redKeyImage.enabled = false;
        blueKeyImage.enabled = false;
        greenKeyImage.enabled = false;
    }

    public void PickedUpRed()
    {
        redKeyImage.enabled = true;
        hasRed = true;
    }

    public void PickedUpBlue()
    {
        blueKeyImage.enabled = true;
        hasBlue = true;
    }

    public void PickedUpGreen()
    {
        greenKeyImage.enabled = true;
        hasGreen = true;
    }

}
