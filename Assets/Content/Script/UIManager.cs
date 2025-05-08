using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Button")]
    public Button shop;
    public Button quest;
    public Button close;

    [Header("Canvas")]
    public RectTransform panelButton;
    public RectTransform panelShop;
    public RectTransform panelQuest;


    [Header("Text")]
    public TMP_Text cookies;
    public TMP_Text point;

    private void Start()
    {
        shop.onClick.AddListener(() => showPanelShop());
        close.onClick.AddListener(() => closePanel());
    }

    /// <summary>
    /// Menampilkan panel Shop untuk upgrade atau membeli item
    /// </summary>
    void showPanelShop()
    {
        panelButton.DOPivotX(1f, 1f);               //Animasi 
        panelShop.gameObject.SetActive(true);
        panelShop.DOPivotX(0f, 1f).SetDelay(.5f);
    }


    /// <summary>
    /// Menutup panel shop atau quest
    /// </summary>
    void closePanel()
    {
        var temp = panelShop.gameObject.activeSelf ? panelShop : panelQuest;

        temp.DOPivotX(1f, 1f);
        panelButton.DOPivotX(0f, 1f).SetDelay(.75f).OnComplete(() =>
        {
            temp.gameObject.SetActive(false);
        });
    }
}
