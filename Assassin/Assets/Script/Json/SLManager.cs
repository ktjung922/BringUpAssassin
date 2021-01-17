using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

class SaveData {
    public float fPlayerStateMaxHP;
    public float fPlayerStateCurHP;
    public float fPlayerStatePow;
    public float fPlayerStateDex;
    public float fPlayerStateInt;
    public int nPlayerStateHaveGold;
    public float fPlayerStateReputation;
    public bool bRequestHaveQuest;
    public int nQuestGrade;
    public float fQuestGold;
    public string sQuestName;
    public string sTargetName;
    public float fTargetHP;
    public float fTargetPow;
    public float fTargetDex;
    public float fTargetInt;
    public int nTargetStatus;
    public string sClientName;
    public float fClientReputation;
    public int nClientStatus;
    public bool bCollectTable0;
    public bool bCollectTable1;
    public bool bCollectTable2;
    public bool bCollectTable3;
    public bool bCollectTable4;
    public bool bCollectTable5;
    public string sItem0Name;
    public string sItem1Name;
    public string sItem2Name;
    public string sItem3Name;



    public SaveData(float fPlayerStateMaxHP = 0, float fPlayerStateCurHP = 0, float fPlayerStatePow = 0, float fPlayerStateDex = 0,
        float fPlayerStateInt = 0, int nPlayerStateHaveGold = 0, float fPlayerStateReputation = 0,
        bool bRequestHaveQuest = false, int nQuestGrade = 0, float fQuestGold = 0, string sQuestName = "", string sTargetName = "",
        float fTargetHP = 0, float fTargetPow = 0, float fTargetDex = 0, float fTargetInt = 0, int nTargetStatus = 0,
        string sClientName = "", float fClientReputation = 0, int nClientStatus = 0, bool bCollectTable0 = false, bool bCollectTable1 = false,
        bool bCollectTable2 = false, bool bCollectTable3 = false, bool bCollectTable4 = false, bool bCollectTable5 = false,
        string sItem0Name = "", string sItem1Name = "", string sItem2Name = "", string sItem3Name = "")
    {
        this.fPlayerStateMaxHP = fPlayerStateMaxHP;
        this.fPlayerStateCurHP = fPlayerStateCurHP;
        this.fPlayerStatePow = fPlayerStatePow;
        this.fPlayerStateDex = fPlayerStateDex;
        this.fPlayerStateInt = fPlayerStateInt;
        this.nPlayerStateHaveGold = nPlayerStateHaveGold;
        this.fPlayerStateReputation = fPlayerStateReputation;
        this.bRequestHaveQuest = bRequestHaveQuest;
        this.nQuestGrade = nQuestGrade;
        this.fQuestGold = fQuestGold;
        this.sQuestName = sQuestName;
        this.sTargetName = sTargetName;
        this.fTargetHP = fTargetHP;
        this.fTargetPow = fTargetPow;
        this.fTargetDex = fTargetDex;
        this.fTargetInt = fTargetInt;
        this.nTargetStatus = nTargetStatus;
        this.sClientName = sClientName;
        this.fClientReputation = fClientReputation;
        this.nClientStatus = nClientStatus;
        this.bCollectTable0 = bCollectTable0;
        this.bCollectTable1 = bCollectTable1;
        this.bCollectTable2 = bCollectTable2;
        this.bCollectTable3 = bCollectTable3;
        this.bCollectTable4 = bCollectTable4;
        this.bCollectTable5 = bCollectTable5;
        this.sItem0Name = sItem0Name;
        this.sItem1Name = sItem1Name;
        this.sItem2Name = sItem2Name;
        this.sItem3Name = sItem3Name;
    }

    public void SetSaveData(PlayerState playerState, RequestUI requestUI, Quest quest, CollectUI collect, MarketUI market)
    {
        this.fPlayerStateMaxHP = playerState.GetMaxHP();
        this.fPlayerStateCurHP = playerState.GetCurHP();
        this.fPlayerStatePow = playerState.GetPow();
        this.fPlayerStateDex = playerState.GetDex();
        this.fPlayerStateInt = playerState.GetInt();
        this.nPlayerStateHaveGold = playerState.GetGold();
        this.fPlayerStateReputation = playerState.GetReputation();
        this.bRequestHaveQuest = requestUI.GetHaveQuest();
        if (quest != null) {
            this.nQuestGrade = (int)quest.questGrade;
            this.fQuestGold = quest.fQuestGold;
            this.sQuestName = quest.sQuestName;
            this.sTargetName = quest.scTargetState.sTargetName;
            this.fTargetHP = quest.scTargetState.fHealth;
            this.fTargetPow = quest.scTargetState.fPow;
            this.fTargetDex = quest.scTargetState.fDex;
            this.fTargetInt = quest.scTargetState.fInt;
            this.nTargetStatus = (int)quest.scTargetState.status;
            this.sClientName = quest.scClientState.sClientName;
            this.fClientReputation = quest.scClientState.fReputation;
            this.nClientStatus = (int)quest.scClientState.Status;
        }
        this.bCollectTable0 = collect.GetTable(0);
        this.bCollectTable1 = collect.GetTable(1);
        this.bCollectTable2 = collect.GetTable(2);
        this.bCollectTable3 = collect.GetTable(3);
        this.bCollectTable4 = collect.GetTable(4);
        this.bCollectTable5 = collect.GetTable(5);
        if (market.GetMerchandiseTemp(0) != null) {
            this.sItem0Name = market.GetMerchandiseTemp(0).GetItem().sItemName;
        }
        if (market.GetMerchandiseTemp(1) != null)
        {
            this.sItem1Name = market.GetMerchandiseTemp(1).GetItem().sItemName;
        }
        if (market.GetMerchandiseTemp(2) != null)
        {
            this.sItem2Name = market.GetMerchandiseTemp(2).GetItem().sItemName;
        }
        if (market.GetMerchandiseTemp(3) != null)
        {
            this.sItem3Name = market.GetMerchandiseTemp(3).GetItem().sItemName;
        }
    }
}

public class SLManager : MonoBehaviour
{
    public static SLManager Instance;

    [SerializeField]
    PlayerState player;
    [SerializeField]
    RequestUI request;
    [SerializeField]
    QuestUI questUI;
    [SerializeField]
    CollectUI collect;
    [SerializeField]
    MarketUI market;
    [SerializeField]
    Quest quest;
    [SerializeField]
    SaveData Data = new SaveData();
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void getComponents() {
        player = GameObject.Find("CharacterUI").GetComponent<PlayerState>();
        request = GameObject.Find("RequestUI").GetComponent<RequestUI>();
        questUI = GameObject.Find("QuestUI").GetComponent<QuestUI>();
        collect = GameObject.Find("Button_Collect").GetComponent<CollectUI>();
        market = GameObject.Find("MarketUI").GetComponent<MarketUI>();
    }
    public void _save() {
        quest = questUI.quest;
        Data.SetSaveData(player, request, quest, collect, market);
        string jdata = JsonConvert.SerializeObject(Data);
        File.WriteAllText(Application.dataPath + "/Test.json", jdata);
        print(jdata);
    }
    public void _load() {
        string jdata = File.ReadAllText(Application.dataPath + "/Test.json");
        Data = JsonConvert.DeserializeObject<SaveData>(jdata);
        player.LoadData(Data.fPlayerStateMaxHP, Data.fPlayerStateCurHP, Data.fPlayerStatePow,
            Data.fPlayerStateDex, Data.fPlayerStateInt, Data.nPlayerStateHaveGold, Data.fPlayerStateReputation);
        request.LoadData(Data.bRequestHaveQuest);
        questUI.LoadData(Data.bRequestHaveQuest);
        if (Data.bRequestHaveQuest) {
            questUI.LoadDataQuest(RestoreQuest());
            LoadCollect();
        }
        market.LoadData(0, Data.sItem0Name);
        market.LoadData(1, Data.sItem1Name);
        market.LoadData(2, Data.sItem2Name);
        market.LoadData(3, Data.sItem3Name);

    }
    private Quest RestoreQuest() {
        TargetState TSTemp = new TargetState();
        TSTemp.sTargetName = Data.sTargetName;
        TSTemp.fHealth = Data.fTargetHP;
        TSTemp.fDex = Data.fTargetDex;
        TSTemp.fInt = Data.fTargetInt;
        TSTemp.fPow = Data.fTargetPow;
        TSTemp.status = (eStatus)Data.nTargetStatus;
        ClientState CSTemp = new ClientState();
        CSTemp.sClientName = Data.sClientName;
        CSTemp.fReputation = Data.fClientReputation;
        CSTemp.Status = (eStatus)Data.nClientStatus;
        Quest questTemp = new Quest();
        questTemp.setQuest((Quest.QuestGrade)Data.nQuestGrade, Data.fQuestGold, Data.sQuestName, TSTemp, CSTemp);

        return questTemp;
    }
    private void LoadCollect() {
        collect.LoadTable(0, Data.bCollectTable0);
        collect.LoadTable(1, Data.bCollectTable1);
        collect.LoadTable(2, Data.bCollectTable2);
        collect.LoadTable(3, Data.bCollectTable3);
        collect.LoadTable(4, Data.bCollectTable4);
        collect.LoadTable(5, Data.bCollectTable5);
    }
    
}
