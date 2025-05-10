using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    private System.Random _random = new System.Random();


    public List<Quest> quests = new List<Quest>();
    public List<QuestList> activeQuests = new List<QuestList>();
    public GameObject Prefabs;
    public Transform parent;


    public float refreshTime = 10;

    void Awake()
    {
        initListQuest();
        StartCoroutine(AutoRefresh());
    }

    public void ReportQuest(QuestType type, int value)
    {
        foreach(var temp in activeQuests)
        {
            var quest = temp.thisQuest;

            if(quest.type == type)
            {
                quest.progress(value);
                temp.reportProgress();

                if (quest.isComplete)
                {
                    GameManager.instance.Reward(quest.reward);
                    quests.Remove(quest);
                    quests.Add(quest);
                    temp.init(quests[Random.Range(3, quests.Count - 1)]);
                }
            }
        }
    }

    public void initListQuest()
    {
        Shuffle(quests);

        for(int i = 0; i < 3; i++)
        {
            var quest = quests[i];

            SpawnQuest(quest);
        }
    }



    void SpawnQuest(Quest quest)
    {
        var questList = Instantiate(Prefabs).GetComponent<QuestList>();
        questList.transform.SetParent(parent);
        questList.gameObject.SetActive(true);
        questList.init(quest);
        activeQuests.Add(questList);
    }

    void RefreshQuests()
    {
        Shuffle(quests);
        for(int i = 0; i < activeQuests.Count; i++)
        {
            activeQuests[i].init(quests[i]);
        }
    }

    IEnumerator AutoRefresh()
    {
        while (true)
        {
            yield return new WaitForSeconds(refreshTime);
            RefreshQuests();
        }
    }

    void Shuffle(List<Quest> array)
    {
        int p = array.Count;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(0, n);
            Quest t = array[r];
            array[r] = array[n];
            array[n] = t;
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
    public int reward;
    public bool isComplete;


    private int count;
    public void init()
    {
        curretnQuest = 0;
        isComplete = false;
    }

    public void progress(int value)
    {
        if (isComplete) return;

        curretnQuest += value;
        
        if(curretnQuest >= targetQuest){
            isComplete = true;
            count++;
            updateValue();
        }
    }

    void updateValue()
    {
        reward = count <= 10 ? count : 10 + Mathf.FloorToInt((count - 10) * 0.5f);
        targetQuest = Mathf.RoundToInt(50 * Mathf.Pow(count, 2f));
    }
}

public enum QuestType
{
    Clicking,
    Buying,
    JumlahPoint,
    JumlahCookie,
    Selling
}
