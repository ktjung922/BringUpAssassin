using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Quest quest;

    [SerializeField]
    private Text textTitle;
    [SerializeField]
    private Text textGrade;
    [SerializeField]
    private Text textGold;
    [SerializeField]
    private Text textTargetName;
    [SerializeField]
    private Text textClientName;
    [SerializeField]
    private Text textGuard;
    [SerializeField]
    private Text[] TargetTexts;
    [SerializeField]
    private Text[] ClientTexts;

    [SerializeField]
    private GameObject goHelpUI;

    public RequestUI request;
    [SerializeField]
    private ButtonInteractableChange[] listButtonInteractable;
    [SerializeField]
    private ButtonNameChange buttonNameChage;
    [SerializeField]
    private CollectUI collectUI;
    
    void Start()
    {
        request = GameObject.Find("RequestUI").GetComponent<RequestUI>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void LoadDataQuest(Quest quest) {
        this.quest = quest;
        SettingUIAll(quest);
    }
    public void LoadData(bool _b) {
        if (_b)
        {
            goHelpUI.SetActive(false);
            buttonNameChage.ChageText(true);
            for (int i = 0; i < listButtonInteractable.Length; i++)
            {
                listButtonInteractable[i].OnClickButtonInteratableChange(_b);
            }
        }
        else {
            goHelpUI.SetActive(true);
            buttonNameChage.ChageText(false);
            collectUI.resetTable();
            for (int i = 0; i < listButtonInteractable.Length; i++)
            {
                listButtonInteractable[i].OnClickButtonInteratableChange(_b);
            }
        }
    }

    public void SettingUIAll(Quest _quest) {
        quest = _quest;
        TargetState TSTemp = _quest.scTargetState;
        ClientState CSTemp = _quest.scClientState;
        textTitle.text = _quest.sQuestName;
        textGrade.text = "의뢰 등급: " + _quest.questGrade;
        textGold.text = "의뢰금: $" + _quest.fQuestGold;
        textTargetName.text = "이    름: " + TSTemp.sTargetName;
        //TargetTexts[0].text = "체   력: " + TSTemp.fHealth;
        //TargetTexts[1].text = "전투력: " + TSTemp.fPow;
        //TargetTexts[2].text = "감지력: " + TSTemp.fDex;
        //TargetTexts[3].text = "비밀력: " + TSTemp.fInt;
        TargetTexts[0].text = "체    력: ???";
        TargetTexts[1].text = "전투력: ???";
        TargetTexts[2].text = "감지력: ???";
        TargetTexts[3].text = "비밀력: ???";

        textClientName.text = "의뢰인: " + CSTemp.sClientName;
        //ClientTexts[0].text = "지   위: " + CSTemp.Status;
        //ClientTexts[1].text = "평   판: " + CSTemp.fReputation;
        ClientTexts[0].text = "지    위: ???";
        ClientTexts[1].text = "평    판: ???";
    }
    public void SettingUI(int _num) {
        switch (_num) {
            case 0:
                TargetTexts[0].text = "체    력: " + quest.scTargetState.fHealth;
                break;
            case 1:
                TargetTexts[1].text = "전투력: " + quest.scTargetState.fPow;
                break;
            case 2:
                TargetTexts[2].text = "감지력: " + quest.scTargetState.fDex;
                break;
            case 3:
                TargetTexts[3].text = "비밀력: " + quest.scTargetState.fInt;
                break;
            case 4:
                ClientTexts[0].text = "지    위: " + quest.scClientState.Status;
                break;
            case 5:
                ClientTexts[1].text = "평    판: " + quest.scClientState.fReputation;
                break;
        }
    }
    public float GetStateData(int _num) {
        float fTemp = 0f;

        switch (_num) {
            case 0:
                fTemp = quest.scTargetState.fHealth;
                break;
            case 1:
                fTemp = quest.scTargetState.fPow;
                break;
            case 2:
                fTemp = quest.scTargetState.fDex;
                break;
            case 3:
                fTemp = quest.scTargetState.fInt;
                break;
            case 4:
                fTemp = (float)quest.scClientState.Status;
                break;
            case 5:
                fTemp = quest.scClientState.fReputation;
                break;
        }
        return fTemp;
    }
    public void OnClickButtonQuestTempUI() {
        request.OnClickButtonUI(1);
        request.ChangeHaveQuest(true);
        goHelpUI.SetActive(false);
        buttonNameChage.ChageText(true);
        for (int i = 0; i < listButtonInteractable.Length; i++) {
            listButtonInteractable[i].OnClickButtonInteratableChange(true);
        }
    }
    public void OnClickButtonRequestUI() {
        goHelpUI.SetActive(true);
        buttonNameChage.ChageText(false);
        collectUI.resetTable();
        for (int i = 0; i < listButtonInteractable.Length; i++)
        {
            listButtonInteractable[i].OnClickButtonInteratableChange(false);
        }
    }

}
