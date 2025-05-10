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

    #region update value Cookie & Point
    /// <summary>
    /// Increase jumlah cookie per click
    /// </summary>
    public void tambahCookie()
    {
        cookie += cookies.clicker;
        questManager.ReportQuest(QuestType.JumlahCookie, cookies.clicker);
        updateUI();
    }
    /// <summary>
    /// Jual cookie
    /// </summary>
    public void jualCookie()
    {
        if (cookie <= 0) return;

        point += cookies.point;

        //check Quest
        questManager.ReportQuest(QuestType.JumlahPoint, cookies.point);
        questManager.ReportQuest(QuestType.Selling, 1);

        // decrease jumlah cookie
        cookie --;

        updateUI();
    }

    #endregion
    /// <summary>
    /// Check Jenis Item yang dibeli
    /// </summary>
    /// <param name="item">Jenis item</param>
    public void ApplyItemEffect(item item) 
    {

        point -= item.currCost();
        item.level += 1;
        switch (item.type)
        {

            // Menambah jumlah cookie yang didapatkan saat 1x click
            case ItemType.UpgradeClick: 
                cookies.clicker = levelValue(item.level); //update value cookie per click
                break;

            // menambah jumlah point saat dijual
            case ItemType.UpgradeSell: 
                cookies.point += levelValue(item.level); // update value point per click
                break;

            // menambah jumlah durasi
            case ItemType.UpgradeDuration: 
                cookies.durationAuto += levelValue(item.level); // update value durasi saat auto
                break;

            //Start auto click
            case ItemType.AutoClick: 
                StartCoroutine(AutoClick(item));
                break;
        }
        questManager.ReportQuest(QuestType.Buying, 1); 
        updateUI();
    }

    /// <summary>
    /// Memperbarui value sesuai level
    /// </summary>
    /// <param name="level">level</param>
    /// <returns></returns>
    private int levelValue(int level)
    {
        return level <= 10 ? level : 10 + Mathf.FloorToInt((level - 10) * 0.5f);
    }

    /// <summary>
    /// Jenis Item auto click
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    IEnumerator AutoClick(item item)
    {
        float duration = cookies.durationAuto;
        var clickingObject = FindObjectOfType<Clicker>(); // cari component Clicker 
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

    /// <summary>
    /// Menambahkan point dari reward quest
    /// </summary>
    /// <param name="value"> jumlah reward yang didapatkan </param>
    public void Reward(int value)
    {
        point += value;
        updateUI();
    }

    /// <summary>
    /// Memperbarui text jumlah cookie dan jumlah point
    /// </summary>
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
