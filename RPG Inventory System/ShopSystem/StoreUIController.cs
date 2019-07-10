using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class StoreUIController : MonoBehaviour
{
    [SerializeField] GameObject StoreItemContainer;
    [SerializeField] GameObject CurrencyDefinition;
    [SerializeField] GameObject shopPanelGameObject;
    [SerializeField] Transform scrollViewContent;
    [SerializeField] Inventory inventory;
    [SerializeField] ShopInventory ShopInventory;

    

    private void Start()
    {
        //inventory = FindObjectOfType<Inventory>();
    }

    public void PopulateInventory(ShopInventory shopInventoryList)
    {
        ClearInventory();
        ShopInventory = shopInventoryList;

        Transform ScrollViewContent = transform.Find("Scroll View/Viewport/Content");

        foreach (var item in shopInventoryList.ShopInventoryItems)
        {
            GameObject newItem = Instantiate(StoreItemContainer, ScrollViewContent);

            newItem.transform.localScale = Vector3.one;

            newItem.transform.Find("Image").GetComponent<Image>().sprite = item.Icon;
            newItem.transform.Find("Name").GetComponent<Text>().text = item.ItemName;

            foreach (var currency in item.PurchasePrice)
            {
                GameObject newCurrency = Instantiate(CurrencyDefinition, newItem.transform.Find("Currency/List"));

                newCurrency.transform.localScale = Vector3.one;

                newCurrency.transform.Find("Image").GetComponent<Image>().sprite = currency.Currency.Image;
                newCurrency.transform.Find("Amount").GetComponent<Text>().text = currency.Amount.ToString();

            }

            newItem.transform.Find("Currency/BuyButton").GetComponent<Button>().onClick.AddListener(BuyOnClick);
        }
    }
    
    public void CloseWindow()
    {
        shopPanelGameObject.SetActive(false);

    }

    public void ClearInventory()
    {
        ShopInventory = null;

        if(scrollViewContent == null)
        {
            scrollViewContent = transform.Find("Scroll View/Viewport/Content");
        }

        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }
    }

    public void BuyOnClick()
    {
        Item purchasedItem = ShopInventory.ShopInventoryItems.Find(x => x.name.Equals //Line continues below
            (EventSystem.current.currentSelectedGameObject.transform.parent.parent.Find("Name").GetComponent<Text>().text));

        if (purchasedItem == null)
        {
            Debug.Log("Unable to find item in shop database");
            return;
        }
        else if (purchasedItem.PurchasePriceInCopper() >= inventory.CopperCoins)
        {
            Debug.Log("Cannot afford item");
            return;
        }

        inventory.PurchaseItem(purchasedItem);
        ShopInventory.ShopInventoryItems.Remove(purchasedItem);
        PopulateInventory(ShopInventory);
    }

    
}
