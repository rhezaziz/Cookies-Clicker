using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region get set
    public static GameManager instance;

    
    private QuestManager QuestManager;
    public QuestManager questManager
    {
        get
        {
            if(QuestManager == null)
                QuestManager = GetComponent<QuestManager>();

            return QuestManager;
        }
    }

    #endregion

    [Header("Value")]
    private int cookie;
    private int point;

    public int GetCookie() => cookie;
    public int GetPoint() => point;

    [Header("UI Component")]
    public TMP_Text Cookies_Text;
    public TMP_Text Point_Text;

    public Cookies cookies = new Cookies();
    public Action<int> OnPointChanged;
    public Action<int> OnClickerChanged;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void tambahCookie()
    {
        cookie += cookies.clicker;
        questManager.ReportQuest(QuestType.JumlahCookie, cookies.clicker);
        updateUI();
    }

    public void jualCookie()
    {
        if (cookie <= 0) return;

        point += cookies.point;
        questManager.ReportQuest(QuestType.JumlahPoint, cookies.point);
        questManager.ReportQuest(QuestType.Selling, 1);

        cookie --;

        updateUI();
    }

    public void ApplyItemEffect(item item) 
    {

        point -= item.currCost();
        item.level += 1;
        switch (item.type)
        {
            case ItemType.UpgradeClick:
                cookies.clicker = levelValue(item.level); 
                break;
            case ItemType.UpgradeSell:
                cookies.point += levelValue(item.level); 
                break;
            case ItemType.UpgradeDuration:
                cookies.durationAuto += levelValue(item.level);
                break;
            case ItemType.AutoClick:

                StartCoroutine(AutoClick(item));
                break;
        }
        questManager.ReportQuest(QuestType.Buying, 1); 
        updateUI();
    }

    private int levelValue(int level)
    {
        return level <= 10 ? level : 10 + Mathf.FloorToInt((level - 10) * 0.5f);
    }

    IEnumerator AutoClick(item item)
    {
        float duration = cookies.durationAuto;
        var clickingObject = FindObjectOfType<Clicker>();
        item.active = true;
        while(duration >= 0f)
        {
            tambahCookie();
            clickingObject.animated();
            yield return new WaitForSeconds(1f);
            duration -= 1f;
        }

        item.active = false;
        OnClickerChanged?.Invoke(point);
    }

    public void Reward(int value)
    {
        point += value;
        updateUI();
    }

    void updateUI()
    {
        Cookies_Text.text = $"{cookie}";
        Point_Text.text = $"{point}";


        OnClickerChanged?.Invoke(point);
    }
}

[System.Serializable]
public class Cookies
{
    public int clicker;
    public int point;
    public float durationAuto;
}
