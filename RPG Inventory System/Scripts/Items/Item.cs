using System.Text;
using UnityEngine;
using Assets.NewInventory.Scripts.Items;
using System.Linq;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
	[SerializeField] string id;
	public string ID { get { return id; } }
	public string ItemName;
	public Sprite Icon;
	[Range(1,999)]
	public int MaximumStacks = 1;

	protected static readonly StringBuilder sb = new StringBuilder();

    [Header("Trade Properties"), Tooltip("Currency and Price the player can purchase the item for.")]
    public List<Assets.NewInventory.Scripts.Items.Item.CurrencyDefinition> PurchasePrice;

    public int PurchasePriceInCopper()
    {
        int copperCoins = 0;

        copperCoins += PurchasePrice.Where(x => x.Currency.Name.Equals("Copper Coin")).Select(s => s.Amount).DefaultIfEmpty(0).Single();
        copperCoins += PurchasePrice.Where(x => x.Currency.Name.Equals("Silver Coin")).Select(s => s.Amount).DefaultIfEmpty(0).Single() * 100;
        copperCoins += (PurchasePrice.Where(x => x.Currency.Name.Equals("Gold Coin")).Select(s => s.Amount).DefaultIfEmpty(0).Single() * 100) * 100;

        return copperCoins;
    }

#if UNITY_EDITOR
    protected virtual void OnValidate()
	{
		string path = AssetDatabase.GetAssetPath(this);
		id = AssetDatabase.AssetPathToGUID(path);
	}
	#endif

	public virtual Item GetCopy()
	{
		return this;
	}

	public virtual void Destroy()
	{

	}

	public virtual string GetItemType()
	{
		return "";
	}

	public virtual string GetDescription()
	{
		return "";
	}
}
