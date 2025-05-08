using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new List<Quest>();

    /// <summary>
    /// Check Quest yang dijalankan dan mengubah progress quest
    /// </summary>
    /// <param name="value"> jumlah progress </param>
    /// <param name="type"> Type Quest yang sedang dijalankan </param>
    public void checkQuest(int value, QuestType type)
    {
        foreach (Quest quest in quests)
        {
            if(quest.type == type && !quest.isComplete)
            {
                quest.curretnQuest += value;
            }
        }
    }
}


[System.Serializable]
public class Quest 
{
    public string questNama;
    public QuestType type;

    public int targetQuest;
    public int curretnQuest;

    public bool isComplete => curretnQuest >= targetQuest;
}

public enum QuestType
{
    Clicking,
    Duration,
    Buying,
    Selling
}
