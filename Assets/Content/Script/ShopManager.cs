using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public List<item> items = new List<item>();
    public GameObject prefabItem;
    private List<ItemShop> itemShops = new List<ItemShop>();
    public Transform parent;

    private void Start()
    {
        initListShop();
    }

    /// <summary>
    /// inisiasi spawn Container item
    /// </summary>
    void initListShop()
    {
        foreach(var item in items)
        {
            var itemShop = Instantiate(prefabItem).GetComponent<ItemShop>();
            
            itemShop.gameObject.SetActive(true);

            itemShop.init(item);
            itemShop.transform.SetParent(parent);
            itemShops.Add(itemShop);
        }
    }

}
/// <summary>
/// Mengelompokkan jenis item
/// </summary>
public enum ItemType
{
    UpgradeClick,
    UpgradeSell,
    UpgradeDuration,
    AutoClick
}

[System.Serializable]
public class item
{
    public ItemType type;
    public int level;
    public string namaItem;
    public int costItem;
    public bool active; // Jika item berjenis Auto, maka ini sebagai kondisi


    /// <summary>
    /// Mengubah biaya atau harga item
    /// </summary>
    /// <returns></returns>
    public int currCost()
    {
        if (type == ItemType.AutoClick)
            return costItem;

        return Mathf.FloorToInt(costItem * Mathf.Pow(level, 2f));
    }
}
