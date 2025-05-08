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

    private QuestManager questManager;
    public QuestManager QuestManager
    {
        get
        {
            if(questManager == null)
                questManager = GetComponent<QuestManager>();

            return questManager;
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
        QuestManager.checkQuest(cookies.clicker, QuestType.Clicking);
        updateUI();
    }

    public void jualCookie()
    {
        if (jmlClicker == 0) return;

        jmlPoint += cookies.point;
        jmlClicker -= 1;
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
