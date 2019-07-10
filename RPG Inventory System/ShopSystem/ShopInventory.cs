using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu (menuName = "Shop/Inventory", fileName = "Shop Inventory")]
public class ShopInventory : ScriptableObject
{
    public List<Item> ShopInventoryItems = new List<Item>();

}
