using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantInteraction : MonoBehaviour
{
    [SerializeField] GameObject shopPanelGameObject;
    [SerializeField] GameObject player;
    [SerializeField] ShopInventory shopInventory;
    [SerializeField] CameraController cameraController;
    [SerializeField] GameObject characterPanelGameObject;

    void Awake()
    {
        shopPanelGameObject.GetComponent<StoreUIController>().PopulateInventory(shopInventory);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (player)
        {
            shopPanelGameObject.SetActive(true);
            characterPanelGameObject.SetActive(true);
            cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
            cameraController.lockCursor = false;
            ShowMouseCursor();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        shopPanelGameObject.SetActive(false);
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        cameraController.lockCursor = true;
        HideMouseCursor();
        
    }

    public void ShowMouseCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideMouseCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
