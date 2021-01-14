using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillUI : MonoBehaviour
{
    [SerializeField]
    private QuestUI questUI;
    [SerializeField]
    private PlayerState playerState;

    private float demageHPAmont;
    public void OnClickKillUI() {
        bool success = false;
        demageHPAmont = 0f;
        success = OneKillFunction(questUI.quest.scTargetState);
        if (!success) {
            success = FightFunction(questUI.quest.scTargetState);
        }

        if (success)
        {
            Debug.Log("의뢰 성공");
            GetRewordFunction();
        }
        else {
            Debug.Log("사망");
        }
        questUI.request.OnClickButtonUI(1);
    }
    private bool FightFunction(TargetState _target) {
        float fPlayerHP = playerState.GetCurHP();
        float fPlayerStartHP = fPlayerHP;
        float fPlayerPow = playerState.GetPow();
        float fEnermyHP = _target.fHealth;
        float fEnermyPow = _target.fPow;


        while (fPlayerHP > 0 && fEnermyHP > 0) {
            if (fPlayerPow - fEnermyPow + 100 > Random.Range(0, 200))
            {
                fEnermyHP -= fPlayerPow;
            }
            else {
                fPlayerHP -= fEnermyPow;
            }
            Debug.Log("싸움" + fPlayerHP + " " + fEnermyHP);
        }
        if (fPlayerHP < 0f) {
            fPlayerHP = 0f;
        }
        demageHPAmont = fPlayerStartHP - fPlayerHP;
        playerState.SetCurHP(-demageHPAmont);

        if (fPlayerHP > 0)
        {
            Debug.Log("전투 승리");
            return true;
        }
        else {
            Debug.Log("전투 패배");
            return false;
        }
    }
    private bool OneKillFunction(TargetState _target) {
        float fTemp = playerState.GetDex() - _target.fDex;
        if (fTemp > Random.Range(0, 300))
        {
            Debug.Log("암살 성공");
            return true;
        }
        else {
            Debug.Log("전투");
            return false;
        }
    }
    private void GetRewordFunction() {
        float DemagePercentage = demageHPAmont / playerState.GetMaxHP();
        if (DemagePercentage == 0)
        {
            Debug.Log("환상적인 암살 성공");
            playerState.SetGold((int)(questUI.quest.fQuestGold * 1.5f));
        }
        else if (DemagePercentage < 0.2)
        {
            Debug.Log("괜찮은 암살 성공");
            playerState.SetGold((int)(questUI.quest.fQuestGold * (1.4f - DemagePercentage)));
        }
        else if (DemagePercentage < 0.5)
        {
            Debug.Log("평범한 암살 성공");
            playerState.SetGold((int)(questUI.quest.fQuestGold * (1.2f - DemagePercentage)));
        }
        else 
        {
            Debug.Log("최악의 암살 성공");
            playerState.SetGold((int)(questUI.quest.fQuestGold * (1.0f - DemagePercentage)));
        }
    }
}
