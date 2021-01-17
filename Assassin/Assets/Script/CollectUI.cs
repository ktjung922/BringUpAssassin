using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectUI : MonoBehaviour
{
    
    // 0->체력 1->전투력 2->감지력 3->비밀력 //4->지위 6->평판
    [SerializeField]
    private bool[] isOpenDataTable;

    [SerializeField]
    private QuestUI questUI;
    [SerializeField]
    private PlayerState playerState;

    [SerializeField]
    private float fBasicGold;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LoadTable(int _num, bool _b) {
        isOpenDataTable[_num] = _b;
        if (_b) {
            questUI.SettingUI(_num);
        }
    }
    public bool GetTable(int _n) {
        return isOpenDataTable[_n];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickCollectUI()
    {
        float fTemp = questUI.GetStateData(3) - playerState.GetInt();
        if (fTemp < 0)
        {
            fTemp = 0;
        }
        Notification.SettingNotification(OnClickNotification, "정보를 수집하겠습니까?\n 예상금액: " + (2f*fTemp + fBasicGold) );
    }
    public void OnClickNotification() {
        if (CheckTable())
        {
            Advice.SettingAdvice("획득할 수 있는 정보를 다 얻었습니다.");
        }
        else
        {
            float fTemp = questUI.GetStateData(3) - playerState.GetInt();
            if (fTemp < 0)
            {
                fTemp = 0;
            }
            if (playerState.GetGold() < 2f*fTemp + fBasicGold) {
                Advice.SettingAdvice("골드가 부족합니다");
                return;
            }
            if (fTemp < Random.Range(0, 101))
            {
                int nTemp;
                while (true)
                {
                    nTemp = Random.Range(0, 6);
                    if (isOpenDataTable[nTemp] == false)
                    {
                        break;
                    }
                }

                playerState.SetGold(-((int)(2f*fTemp + fBasicGold)));

                questUI.SettingUI(nTemp);
                isOpenDataTable[nTemp] = true;
                Advice.SettingAdvice("정보를 얻었습니다.\n획득한 정보: <color=green>"+ nTemp + "번</color>\n지불한 골드: <color=red>" + (int)(2f * fTemp + fBasicGold) + "</color>");
            }
            else
            {
                playerState.SetGold(-((int)(2f * fTemp + fBasicGold)));
                Advice.SettingAdvice("정보 획득에 실패했습니다.\n지불한 골드: <color=red>"+ (int)(2f * fTemp + fBasicGold)+"</color>");
            }
        }
    }
    
    public void resetTable() {
        for (int i = 0; i < isOpenDataTable.Length; i++)
        {
            isOpenDataTable[i] = false;
        }
    }
    private bool CheckTable() {
        bool isTemp = true;
        for (int i = 0; i < isOpenDataTable.Length; i++)
        {
            if (isOpenDataTable[i] == false) {
                isTemp = false;
                break;
            }
        }
        return isTemp;
    }
}
