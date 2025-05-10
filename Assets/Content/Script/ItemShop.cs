using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    
    [Header("Component")]
    public TMP_Text namaItem;
    public TMP_Text hargaItem;
    public Button btnBeli;

    [Header("Value")]
    private int price;
    private item currItem;

    /// <summary>
    /// inisiasi awal item
    /// </summary>
    /// <param name="item"></param>
    public void init(item item)
    {
        currItem = item;
        initUI();
        btnBeli.onClick.AddListener(() => OnBuyClicked());
        GameManager.instance.OnClickerChanged += UpdateInteractable;
    }

    /// <summary>
    /// inisiasi UI container item
    /// </summary>
    void initUI()
    {
        price = currItem.currCost();
        namaItem.text = currItem.namaItem;
        hargaItem.text = price.ToString();
        btnBeli.interactable = canBuy();
    }

    /// <summary>
    /// update button interactable 
    /// </summary>
    /// <param name="currentPoint"></param>
    public void UpdateInteractable(int currentPoint)
    {
        bool cooldown = currItem.type == ItemType.AutoClick && currItem.active;
        btnBeli.interactable = currentPoint >= price && !cooldown;
    }

    /// <summary>
    /// fungsi membeli item
    /// </summary>
    private void OnBuyClicked()
    {
        if (GameManager.instance.GetPoint() < price)
            return;

        
        GameManager.instance.ApplyItemEffect(currItem);
        initUI();
    }

    /// <summary>
    /// check bisa membeli item
    /// </summary>
    /// <returns></returns>
    bool canBuy()
    {
        int point = GameManager.instance.GetPoint();
        return price <= point;
    }



    private void OnDestroy()
    {
        GameManager.instance.OnClickerChanged -= UpdateInteractable;
        btnBeli.onClick.RemoveAllListeners();
    }
}
