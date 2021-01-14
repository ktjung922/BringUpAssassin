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
    private Quest.QuestGrade PlayerGrade = Quest.QuestGrade.F;

    [SerializeField] // 0->health 1->pow 2->dex 3->int
    private Image[] imgs;
    [SerializeField] // 0->health 1->pow 2->dex 3->int
    private Text[] texts;
    [SerializeField]
    private Text textGold;
    [SerializeField]
    private Text textGrade;
    void Start()
    {
        UpdateState(0);
        UpdateGold();
        UpdateGrade();
    }

    // Update is called once per frame
    void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Y)) { SetMaxHP(100); }
        if (Input.GetKeyDown(KeyCode.U)) { SetCurHP(-10); }
        if (Input.GetKeyDown(KeyCode.I)) { SetPow(5); }
        if (Input.GetKeyDown(KeyCode.O)) { SetDex(5); }
        if (Input.GetKeyDown(KeyCode.P)) { SetInt(5); }
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
    public void OnclickRestButton() {
        float nTemp = fMaxHP - fCurHP;
        SetCurHP(nTemp);
        SetGold(-(10 + (int)nTemp));
    }
    public void SetGold(int _n) {
        nHaveGold += _n;
        UpdateGold();
    }
}
