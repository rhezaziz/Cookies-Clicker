using TMPro;
using UnityEngine;

public class QuestList : MonoBehaviour
{
    [Header("UI Component")]
    public TMP_Text questName;
    public TMP_Text rewardText;
    public TMP_Text progressText;

    private Quest curQuest;
    public Quest thisQuest
    {
        get
        {
            return curQuest;
        }
    }
    /// <summary>
    /// inisiasi UI Quest
    /// </summary>
    /// <param name="quest"></param>
    public void init(Quest quest)
    {
        curQuest = quest;
        quest.init();
        questName.text = quest.questNama;
        rewardText.text = $"reward : {quest.reward}";
        progressText.text = $"{quest.curretnQuest}/{quest.targetQuest}";
    }

    /// <summary>
    /// update indikator progress quest
    /// </summary>
    public void reportProgress()
    {
        progressText.text = $"{curQuest.curretnQuest}/{curQuest.targetQuest}";
    }
}

