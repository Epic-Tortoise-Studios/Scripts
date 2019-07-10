using UnityEngine;
using UnityEngine.UI;

public class Inventory : ItemContainer
{
	[SerializeField] protected Item[] startingItems;
	[SerializeField] protected Transform itemsParent;

    [SerializeField] GameObject CurrencyPanel;
    public int CopperCoins;
    [SerializeField] Text copperCurrencyText;
    [SerializeField] Text silverCurrencyText;
    [SerializeField] Text goldCurrencyText;

    public int[] GetCoinCurrency()
    {
        int[] currency = new int[] { 0, 0, 0 };
        //Coin conversion. 0 = copper coins 1= silver coins 2 = gold coins
        currency[0] = CopperCoins % 100;
        currency[1] = (CopperCoins / 100) % 100;
        currency[2] = (CopperCoins / 100) / 100;
        return currency;
    }

    protected override void OnValidate()
	{
		if (itemsParent != null)
			itemsParent.GetComponentsInChildren(includeInactive: true, result: ItemSlots);

		if (!Application.isPlaying) {
			SetStartingItems();
		}
	}

	protected override void Awake()
	{
		base.Awake();
        SetStartingItems();
        copperCurrencyText = CurrencyPanel.transform.Find("Copper Coin/Copper Amount").GetComponent<Text>();
        silverCurrencyText = CurrencyPanel.transform.Find("Silver Coin/Silver Amount").GetComponent<Text>();
        goldCurrencyText = CurrencyPanel.transform.Find("Gold Coin/Gold Amount").GetComponent<Text>();
        UpdateCurrency();
        
	}

	private void SetStartingItems()
	{
		Clear();
		foreach (Item item in startingItems)
		{
			AddItem(item.GetCopy());
		}
	}

    public void PurchaseItem(Item purchasedItem)
    {
        //deduct the money from the player.
        CopperCoins -= purchasedItem.PurchasePriceInCopper();
        UpdateCurrency();

        //add the item to the players inventory.
        AddItem(purchasedItem);

        //add it to the UI screen.


    }

    public void UpdateCurrency()
    {
        int[] currency = GetCoinCurrency();
        copperCurrencyText.text = currency[0].ToString();
        silverCurrencyText.text = currency[1].ToString();
        goldCurrencyText.text = currency[2].ToString();

    }
}
