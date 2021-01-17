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
    private string str1;
    private string str2;
    private int nGold;
    public void OnClickKillUI() {
        Notification.SettingNotification(StartKill, "암살을 시작하겠습니까?");
    }
    public void StartKill() {
        bool success = false;
        demageHPAmont = 0f;
        success = OneKillFunction(questUI.quest.scTargetState);
        if (!success)
        {
            success = FightFunction(questUI.quest.scTargetState);
        }

        if (success)
        {
            GetRewordFunction();
            playerState.StateUpgrade(Random.Range(0, 4), questUI.quest.scClientState.fReputation);
            Advice.SettingAdvice("어제 밤 암살을 시도했습니다.\n" + str1 + "\n<color=green>" + str2 + "</color>을 했습니다.\n의뢰인이 암살에 만족한 만큼 추가금을 주었습니다." +
                "\n획득한 골드: <color=green>" + nGold + "</color>");
        }
        else
        {
            Advice.SettingAdvice("사망했습니다.");
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
        }
        if (fPlayerHP < 0f) {
            fPlayerHP = 0f;
        }
        demageHPAmont = fPlayerStartHP - fPlayerHP;
        playerState.SetCurHP(-demageHPAmont);

        if (fPlayerHP > 0)
        {
            return true;
        }
        else {
            return false;
        }
    }
    private bool OneKillFunction(TargetState _target) {
        float fTemp = playerState.GetDex() - _target.fDex;
        if (fTemp > Random.Range(0, 300))
        {
            str1 = "모습을 들키지 않고 한방에 ";
            return true;
        }
        else {
            str1 = "비록 모습은 들켰지만 전투 끝에 ";
            return false;
        }
    }
    private void GetRewordFunction() {
        float DemagePercentage = demageHPAmont / playerState.GetMaxHP();
        if (DemagePercentage == 0)
        {
            str2 = "환상적인 암살";
            nGold = (int)(questUI.quest.fQuestGold * 2f);
        }
        else if (DemagePercentage < 0.2)
        {
            str2 = "괜찮은 암살";
            nGold = (int)(questUI.quest.fQuestGold * (1f + 0.7f * (1f - DemagePercentage)));
        }
        else if (DemagePercentage < 0.5)
        {
            str2 = "평범한 암살";
            nGold = (int)(questUI.quest.fQuestGold * (1f + 0.5f * (1f - DemagePercentage)));
        }
        else 
        {
            str2 = "최악의 암살";
            nGold = (int)(questUI.quest.fQuestGold * (1f + 0.3f * (1f - DemagePercentage)));
        }
        playerState.SetGold(nGold);

    }
}
