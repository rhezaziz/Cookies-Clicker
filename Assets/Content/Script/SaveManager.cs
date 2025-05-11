using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NUnit.Framework.Interfaces;

public class SaveManager : MonoBehaviour
{
    private static string savePath => Application.persistentDataPath + "/save.json";

    /// <summary>
    /// Save Data Game
    /// </summary>
    public static void SaveGame()
    {
        var data = new SaveData();
        data.point = GameManager.instance.point;
        data.cookie = GameManager.instance.cookie;
        data.clickPower = GameManager.instance.cookies.clicker;
        data.sellPower = GameManager.instance.cookies.point;
        data.duration = GameManager.instance.cookies.durationAuto;

        foreach (var quest in GameManager.instance.questManager.quests)
        {
            var q = new Quest
            {
                questNama = quest.questNama,
                type = quest.type,
                currentAmount = quest.currentAmount,
                targetAmount = quest.targetAmount,
                reward = quest.reward,
                count = quest.count,
                isComplete = quest.isComplete
            };
            data.quest.Add(q);
        }

        foreach(var item in GameManager.instance.shopManager.items)
        {
            Debug.Log($"{item.namaItem} : {item.currCost()} - {item.costItem}");
            var s = new item
            {
                namaItem = item.namaItem,
                level = item.level,
                type = item.type,
                costItem = item.costItem,

            };
            data.item.Add(s);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved");
    }

    /// <summary>
    /// Load Data Game
    /// </summary>
    public static void LoadGame()
    {
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        var data = JsonUtility.FromJson<SaveData>(json);

        GameManager.instance.point = data.point;
        GameManager.instance.cookie = data.cookie;
        GameManager.instance.cookies.clicker = data.clickPower;
        GameManager.instance.cookies.point = data.sellPower;
        GameManager.instance.cookies.durationAuto = data.duration;

        GameManager.instance.questManager.quests.Clear();
        foreach (var questData in data.quest)
        {
            var quest = new Quest
            {
                questNama = questData.questNama,
                type = questData.type,
                currentAmount = questData.currentAmount,
                targetAmount = questData.targetAmount,
                reward = questData.reward,
                count = questData.count,
                isComplete = questData.isComplete
            };
            GameManager.instance.questManager.quests.Add(quest);
        }

        GameManager.instance.shopManager.items.Clear();
        foreach (var itemData in data.item)
        {
            Debug.Log($"{itemData.namaItem} : {itemData.currCost()} - {itemData.costItem}");
            var item = new item
            {
                namaItem = itemData.namaItem,
                type = itemData.type,
                level = itemData.level,
                costItem = itemData.costItem,
                active = false,
                
            };
            GameManager.instance.shopManager.items.Add(item);
        }
        GameManager.instance.updateLoadUI();
        Debug.Log("Game Loaded");
    }
}

[System.Serializable]
public class SaveData
{
    public int point;
    public int cookie;
    public int clickPower;
    public int sellPower;
    public float duration;

    public List<Quest> quest = new();
    public List<item> item = new();
}
