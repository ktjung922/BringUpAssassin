using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private float fMaxHP = 100;
    [SerializeField]
    private float fCurHP = 100;
    [SerializeField]
    private float fPow = 10;
    [SerializeField]
    private float fDex = 10;
    [SerializeField]
    private float fInt = 10;
    [SerializeField]
    private int nHaveGold = 100;
    [SerializeField]
    private float fReputation = 0;
    [SerializeField]
    private Quest.QuestGrade PlayerGrade = Quest.QuestGrade.F;

    [SerializeField] // 0->health 1->pow 2->dex 3->int
    private Image[] imgs;
    [SerializeField] // 0->health 1->pow 2->dex 3->int
    private Text[] texts;
    [SerializeField]
    private Text textGold;
    [SerializeField]
    private Text textGrade;
    [SerializeField]
    private RequestUI requestUI;
    [SerializeField]
    private float fUpgradeX;
    [SerializeField]
    private Text textGoldPrefab;
    void Start()
    {
        UpdateState(0);
        UpdateGold();
        UpdateReputation();
        UpdateGrade();
        requestUI = GameObject.Find("RequestUI").GetComponent<RequestUI>();
    }

    public void LoadData(float _fMaxHP, float _fCurHP, float _fPow, float _fDex, float _fInt, int _nGold, float _fReputation) {
        fMaxHP = _fMaxHP;
        fCurHP = _fCurHP;
        fPow = _fPow;
        fDex = _fDex;
        fInt = _fInt;
        nHaveGold = _nGold;
        fReputation = _fReputation;
        UpdateState(0);
        UpdateGold();
        UpdateReputation();
        UpdateGrade();
    }
    public void SetMaxHP(float _num) {
        fMaxHP += _num;
        UpdateState(0);
    }
    public void SetCurHP(float _num) {
        fCurHP += _num;
        UpdateState(1);
    }
    public void SetPow(float _num) {
        fPow += _num;
        UpdateState(2);
    }
    public void SetDex(float _num)
    {
        fDex += _num;
        UpdateState(3);
    }
    public void SetInt(float _num)
    {
        fInt += _num;
        UpdateState(4);
    }
    public float GetMaxHP()
    {
        return fMaxHP ;
    }
    public float GetCurHP() {
        return fCurHP;
    }
    public float GetPow()
    {
        return fPow;
    }
    public float GetDex()
    {
        return fDex;
    }
    public float GetInt()
    {
        return fInt;
    }
    public float GetReputation() {
        return fReputation;
    }
    public Quest.QuestGrade GetGrade() {
        return PlayerGrade;
    }


    public void UpdateState(int num) {
        switch (num) {
            case 0:
                imgs[0].fillAmount = fCurHP / fMaxHP;
                imgs[1].fillAmount = fPow / fMaxHP;
                imgs[2].fillAmount = fDex / fMaxHP;
                imgs[3].fillAmount = fInt / fMaxHP;
                texts[0].text = fCurHP + " / " + fMaxHP;
                texts[1].text = fPow.ToString();
                texts[2].text = fDex.ToString();
                texts[3].text = fInt.ToString();
                break;
            case 1:
                imgs[0].fillAmount = fCurHP / fMaxHP;
                texts[0].text = fCurHP + " / " + fMaxHP;
                break;
            case 2:
                imgs[1].fillAmount = fPow / fMaxHP;
                texts[1].text = fPow.ToString();
                break;
            case 3:
                imgs[2].fillAmount = fDex / fMaxHP;
                texts[2].text = fDex.ToString();
                break;
            case 4:
                imgs[3].fillAmount = fInt / fMaxHP;
                texts[3].text = fInt.ToString();
                break;
            default:
                break;
        }
    }
    public void UpdateGold() {
        textGold.text = "$" + nHaveGold;
    }
    public void UpdateGrade()
    {
        textGrade.text = PlayerGrade.ToString();
    }
    
    public void SetGold(int _n) {
        nHaveGold += _n;
        Text textTemp = Instantiate(textGoldPrefab);
        textTemp.transform.SetParent(textGold.transform, false);
        textTemp.transform.GetComponent<GoldTemp>().SetGold(_n);

        UpdateGold();
    }
    public int GetGold()
    {
        return nHaveGold;
    }
    public void SetReputation(float _f) {
        fReputation += _f;
        UpdateReputation();
    }
    public void UpdateReputation() {
        if (fReputation < 80)
        {
            SetGrade(Quest.QuestGrade.F);
        }
        else if (fReputation < 240)
        {
            SetGrade(Quest.QuestGrade.E);
        }
        else if (fReputation < 860)
        {
            SetGrade(Quest.QuestGrade.D);
        }
        else if (fReputation < 1120)
        {
            SetGrade(Quest.QuestGrade.C);
        }
        else if (fReputation < 2000)
        {
            SetGrade(Quest.QuestGrade.B);
        }
        else if (fReputation < 3280)
        {
            SetGrade(Quest.QuestGrade.A);
        }
        else if (fReputation < 5040)
        {
            SetGrade(Quest.QuestGrade.S);
        }
        else if (fReputation < 7360)
        {
            SetGrade(Quest.QuestGrade.SS);
        }
        else {
            SetGrade(Quest.QuestGrade.SSS);
        }
    }
    public void SetGrade(Quest.QuestGrade questGrade) {
        PlayerGrade = questGrade;
        UpdateGrade();
    }
    public void StateUpgrade(int _n, float _f) {
        switch (_n) {
            case 0:
                SetMaxHP((int)(fUpgradeX * 1.5f));
                break;
            case 1:
                SetPow((int)(fUpgradeX));
                break;
            case 2:
                SetDex((int)(fUpgradeX));
                break;
            case 3:
                SetInt((int)(fUpgradeX));
                break;
        }

        SetReputation(_f);
    }
    public void OnclickRestButton()
    {
        float fTemp = fMaxHP - fCurHP;
        Notification.SettingNotification(() => RestAction(fTemp), "휴식을 하겠습니까?\n휴식할 경우 체력 모두 회복되고\n퀘스트가 전부 변경됩니다.\n예상금액: " + (10 + fTemp));
    }
    public void RestAction(float _f) {
        if (nHaveGold < 10 + (int)_f)
        {
            Advice.SettingAdvice("돈이 부족하여 쉴 수 없습니다.");
        }
        else
        {
            requestUI.UpdateQuest();
            SetCurHP(_f);
            SetGold(-(10 + (int)_f));
            Advice.SettingAdvice("어제 밤 여관에서 쉬었습니다..\n돈은 들었지만 체력을 채울 수 있는 시간이였습니다.\n회복한 체력: <color=green>" + _f + "</color>\n지불한 골드: <color=red>" + (_f + 10) + "</color>");
        }
    }
}
