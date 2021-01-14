using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuestTemp : MonoBehaviour, IPointerClickHandler
{
    public Quest quest;

    [SerializeField]
    private Text[] texts;
    [SerializeField]
    private QuestUI questui;

    // Start is called before the first frame update
    void Start()
    {
        questui = GameObject.Find("QuestUI").GetComponent<QuestUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setText(Quest _quest) {
        texts[0].text = _quest.sQuestName;
        texts[1].text = "의뢰비: " + _quest.fQuestGold;
        texts[2].text = "대상자 직업: " + _quest.scTargetState.status;
        texts[3].text = _quest.questGrade.ToString();
        this.quest = _quest;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        questui.SettingUIAll(quest);
        questui.OnClickButtonQuestTempUI();
    }
}
