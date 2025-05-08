using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region get set
    public static GameManager instance;

    private UIManager uiManager;
    public UIManager UIManager
    {
        get
        {
            if(uiManager == null)
                uiManager = GetComponent<UIManager>();

            return uiManager;
        }
    }
    #endregion

    [Header("Value")]
    public int jmlClicker;
    public int jmlPoint;

    [Header("UI Component")]
    public TMP_Text Cookies_Text;
    public TMP_Text Point_Text;

    public Cookies cookies = new Cookies();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void clicking()
    {
        jmlClicker += cookies.clicker;
        updateUI();
    }

    public void jualCookie()
    {
        jmlPoint = jmlClicker * cookies.point;
        jmlClicker = 0;
        updateUI();
    }


    void updateUI()
    {
        Cookies_Text.text = $"{jmlClicker}";
        Point_Text.text = $"{jmlPoint}";
    }
}

[System.Serializable]
public class Cookies
{
    public int clicker;
    public int point;
    public int autoClicker;
    public float durationAuto;
}
