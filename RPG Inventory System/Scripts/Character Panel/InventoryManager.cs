using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    //[SerializeField] GameObject characterPanelGameObject;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Must be Null!" + gameObject.name);
        }
        else
        {
            instance = this;
        }
        //characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);
    }
}
