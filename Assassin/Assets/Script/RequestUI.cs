using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestUI : MovingUI
{
    private bool haveQuest = false;


    [SerializeField]
    private Image questTempPrefab;
    [SerializeField]
    private Transform trPanel;

    [SerializeField]
    private List<Image> QuestList = new List<Image>();

    private float ftest = 0;

    [SerializeField]
    private QuestUI questUI;
    [SerializeField]
    private PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        questUI = GameObject.Find("QuestUI").GetComponent<QuestUI>();
        UpdateQuest();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void UpdateQuest()
    {
        ResetQuest();
        for (int i = 0; i < 4; i++) {
            Image imgTemp = Instantiate(questTempPrefab);
            imgTemp.transform.SetParent(trPanel, false);
            imgTemp.transform.GetComponent<QuestTemp>().setText(SettingQuest());
            QuestList.Add(imgTemp);
        }
    }
    private Quest SettingQuest() {
        Quest questTemp = new Quest();

        //랜덤화
        TargetState TSTemp = new TargetState();
        TSTemp.status = eStatus.농부;
        TSTemp.sTargetName = "원용진";
        TSTemp.fHealth = 100f;
        TSTemp.fPow = 20f;
        TSTemp.fDex = 0f;
        TSTemp.fInt = 50f;

        ClientState CSTemp = new ClientState();
        CSTemp.sClientName = "연지";
        CSTemp.Status = eStatus.상인;
        CSTemp.fReputation = 100f;

        questTemp.setQuest(Quest.QuestGrade.F, 100f+ ftest, "부럽다 원용진", TSTemp, CSTemp);
        //asd


        ftest++;
        return questTemp;
    }

    public void ResetQuest() {
        for (int i = 0; i < QuestList.Count; i++) {
            Destroy(QuestList[i].gameObject);
        }
        QuestList.Clear();
    }
    public void ChageHaveQuest() {
        haveQuest = !haveQuest;
    }

    public override void OnClickButtonUI(int _n)
    {
        if (haveQuest)
        {
            UpdateQuest();
            ChageHaveQuest();
            questUI.OnClickButtonRequestUI();
            if (_n == 0)
            {
                playerState.SetGold(-(int)(questUI.quest.fQuestGold * 2f));
            }
        }
        else {
            if (isOpen)
            {
                anim.SetTrigger("Close");
                Background.SetActive(false);
            }
            else
            {
                anim.SetTrigger("Open");
                Background.SetActive(true);
            }
            isOpen = !isOpen;
            if (anotherMovingUI.getIsOpen() && _n != 2)
            {
                anotherMovingUI.OnClickButtonUI(2);
            }
        }
    }
}
