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


    [SerializeField]
    private QuestUI questUI;
    [SerializeField]
    private PlayerState playerState;

    //0->F 1->E 2->D 3->C 4->B 5->A 6->S 7->SS 8->SSS
    [SerializeField]
    private int[] nWeight;
    [SerializeField]
    private int nWeightSum;

    public List<Dictionary<string, object>> NameData = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> QuestRandomData = new List<Dictionary<string, object>>();

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        nWeightSum = 0;
        questUI = GameObject.Find("QuestUI").GetComponent<QuestUI>();
        NameData = CSVReader.Read("Name");
        QuestRandomData = CSVReader.Read("QuestData");
        UpdateQuest();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool GetHaveQuest() {
        return haveQuest;
    }
    public void LoadData(bool _b) {
        ChangeHaveQuest(_b);
        UpdateQuest();
    }
    public void UpdateQuest()
    {
        ResetQuest();
        ChangeWeight(playerState.GetGrade());
        for (int i = 0; i < nWeight.Length; i++)
        {
            nWeightSum += nWeight[i];
        }
        for (int i = 0; i < 4; i++) {
            Image imgTemp = Instantiate(questTempPrefab);
            imgTemp.transform.SetParent(trPanel, false);
            imgTemp.transform.GetComponent<QuestTemp>().setText(SettingQuest());
            QuestList.Add(imgTemp);
        }
    }
    private Quest SettingQuest() {
        Quest questTemp = new Quest();

        int RandomNum = Random.Range(0, nWeightSum);
        for (int i = 0; i < nWeight.Length; i++) {
            if (nWeight[i] > RandomNum)
            {
                questTemp.questGrade = (Quest.QuestGrade)i;
                break;
            }
            else {
                RandomNum -= nWeight[i];
            }
        }

        TargetState TSTemp = CreateTargetState(questTemp.questGrade);
        TSTemp.sTargetName = NameData[RandomNameNum()]["NAME"].ToString();

        ClientState CSTemp = CreateClientState(questTemp.questGrade);
        CSTemp.sClientName = NameData[RandomNameNum()]["NAME"].ToString();



        questTemp.setQuest(questTemp.questGrade, Random.Range((int)QuestRandomData[(int)questTemp.questGrade]["GOLDMIN"], (int)QuestRandomData[(int)questTemp.questGrade]["GOLDMAX"]) , TSTemp.sTargetName + "을 살해 하라", TSTemp, CSTemp);
     
        return questTemp;
    }
    private TargetState CreateTargetState(Quest.QuestGrade _grade)
    {
        TargetState TSTemp = new TargetState();
        TSTemp.status = (eStatus)Random.Range((int)QuestRandomData[(int)_grade]["TSMIN"], (int)QuestRandomData[(int)_grade]["TSMAX"]+1);
        TSTemp.fHealth = Random.Range((int)QuestRandomData[(int)_grade]["THPMIN"], (int)QuestRandomData[(int)_grade]["THPMAX"]);
        TSTemp.fPow = Random.Range((int)QuestRandomData[(int)_grade]["TPWMIN"], (int)QuestRandomData[(int)_grade]["TPWMAX"]);
        TSTemp.fDex = Random.Range((int)QuestRandomData[(int)_grade]["TDXMIN"], (int)QuestRandomData[(int)_grade]["TDXMAX"]);
        TSTemp.fInt = Random.Range((int)QuestRandomData[(int)_grade]["TITMIN"], (int)QuestRandomData[(int)_grade]["TITMAX"]);
        return TSTemp;
    }
    private ClientState CreateClientState(Quest.QuestGrade _grade)
    {
        ClientState CSTemp = new ClientState();
        CSTemp.Status = (eStatus)Random.Range((int)QuestRandomData[(int)_grade]["CSMIN"], (int)QuestRandomData[(int)_grade]["CSMAX"]+1);
        CSTemp.fReputation = Random.Range((int)QuestRandomData[(int)_grade]["CRTMIN"], (int)QuestRandomData[(int)_grade]["CRTMAX"]);
        return CSTemp;
    }

    private void ResetQuest() {
        for (int i = 0; i < QuestList.Count; i++) {
            Destroy(QuestList[i].gameObject);
        }
        QuestList.Clear();
        nWeightSum = 0;
    }

    private void ChangeWeight(Quest.QuestGrade grade) {
        List<Dictionary<string, object>> data = CSVReader.Read("WeightTable");
        for (int i = 0; i < nWeight.Length; i++) {
            nWeight[i] = (int)data[i][grade.ToString()];
        }
    }
    public void ChangeHaveQuest(bool _b) {
        haveQuest = _b;
    }
    private int RandomNameNum() {
        return Random.Range(0, NameData.Count);
    }
    public override void OnClickButtonUI(int _n)
    {
        if (haveQuest)
        {
            if (_n == 0) {
                string s = "의뢰를 포기하겠습니까?\n포기 할 경우 위약금이 있습니다.\n예상 위약금: " + (int)(questUI.quest.fQuestGold * 1.5f);
                Notification.SettingNotification(() => ClearQuest(_n), s);
            }
            else {
                ClearQuest(1);
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
    public void ClearQuest(int _n) {
        UpdateQuest();
        ChangeHaveQuest(false);
        questUI.OnClickButtonRequestUI();
        if (_n == 0)
        {
            playerState.SetGold(-(int)(questUI.quest.fQuestGold * 1.5f));
            Advice.SettingAdvice("암살을 포기했습니다.\n지불한 골드: <color=red> -"+ (int)(questUI.quest.fQuestGold * 1.5f) + "</color>");
        }
    }
}
