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
    public void init(Quest quest)
    {
        curQuest = quest;
        quest.init();
        questName.text = quest.questNama;
        rewardText.text = $"reward : {quest.reward}";
        progressText.text = $"{quest.curretnQuest}/{quest.targetQuest}";
    }


    public void reportProgress()
    {
        progressText.text = $"{curQuest.curretnQuest}/{curQuest.targetQuest}";
    }
}

