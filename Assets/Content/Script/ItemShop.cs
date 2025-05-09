using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    
    [Header("Component")]
    public TMP_Text namaItem;
    public TMP_Text hargaItem;
    public Button btnBeli;
    public void init(item item)
    {
        namaItem.text = item.namaItem;
        hargaItem.text = item.costItem.ToString();
        btnBeli.interactable = canBuy(item);
        btnBeli.onClick.AddListener(() => buyItem(item));
    }

    void buyItem(item item)
    {

    }


    bool canBuy(item item)
    {
        int point = GameManager.instance.jmlPoint;
        return item.costItem >= point;
    }
}
