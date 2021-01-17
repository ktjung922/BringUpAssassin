using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "New Menu/quest")]
public class Quest : ScriptableObject
{
    public enum QuestGrade { 
        F,E,D,C,B,A,S,SS,SSS
    }
    public QuestGrade questGrade;
    public float fQuestGold;
    public string sQuestName;
    public TargetState scTargetState;
    public ClientState scClientState;

    public void setQuest(QuestGrade questgrade, float questGold, string questName, TargetState TS, ClientState CS) {
        questGrade = questgrade;
        fQuestGold = questGold;
        sQuestName = questName;
        scTargetState = TS;
        scClientState = CS;
    } 
}
