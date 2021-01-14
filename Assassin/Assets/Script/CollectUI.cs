using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectUI : MonoBehaviour
{
    private float collectHauge = 0f;

    
    // 0->체력 1->전투력 2->감지력 3->비밀력 //4->지위 6->평판
    [SerializeField]
    private bool[] isOpenDataTable;

    [SerializeField]
    private QuestUI questUI;
    [SerializeField]
    private PlayerState playerState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickCollectUI()
    {
        if (CheckTable())
        {
            Debug.Log("모든정보 다 열람");
        }
        else {
            float fTemp = questUI.GetStateData(3) - playerState.GetInt();
            if (fTemp < 0)
            {
                fTemp = 0;
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

                playerState.SetGold(-((int)fTemp + 30));

                questUI.SettingUI(nTemp);
                isOpenDataTable[nTemp] = true;
            }
            else {
                Debug.Log("정보 획득 실패");
                playerState.SetGold(-30);
            }
        }
    }
    private void resetTable() {
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
