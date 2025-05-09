using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public List<item> items = new List<item>();
    public GameObject prefabItem;
    private List<ItemShop> itemShops = new List<ItemShop>();


    private void Start()
    {
        initListShop();
    }


    void initListShop()
    {
        foreach(var item in items)
        {
            var itemShop = Instantiate(prefabItem).GetComponent<ItemShop>();
            
            itemShop.gameObject.SetActive(false);

            itemShop.init(item);
            
            itemShops.Add(itemShop);
        }
    }


    public void buyItem()
    {
        foreach(var item in itemShops)
        {
            item.btnBeli.interactable = false;
        }
    }
}

public enum itemType
{
    Useable,
    Upgradeable
}

[System.Serializable]
public class item
{
    public int level;
    public string namaItem;
    public int costItem;

}
