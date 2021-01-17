using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MovingUI
{
    [SerializeField]
    private PlayerState playerState;
    [SerializeField]
    private Image merchandisePrefab;
    [SerializeField]
    private int nBeforeNum = 0;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private MarketTag[] marketTags;
    [SerializeField]
    private GameObject[] tables;
    [SerializeField]
    private Item[] items;
    
    // 0 -> 갑옷 1-> 무기 2-> 망토 3-> 망원경 4-> 알약
    [SerializeField]
    private Transform[] trPanel;
    // 0 -> 갑옷 1-> 무기 2-> 망토 3-> 망원경
    [SerializeField]
    private MerchandiseTemp[] putonMerchandiseTemps;
    [SerializeField]
    private List<MerchandiseTemp> merchandiseTemps = new List<MerchandiseTemp>();
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SettingMerchandiseTemp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public MerchandiseTemp GetMerchandiseTemp(int _n) {
        return putonMerchandiseTemps[_n];
    }
    public void LoadData(int _n, string _Str) {
        if (_Str == "")
        {
            if (putonMerchandiseTemps[_n] != null)
            {
                putonMerchandiseTemps[_n].ChangeImgsAlpha(1);
            }
            putonMerchandiseTemps[_n] = null;
        }
        else {
            for (int i = 0; i < merchandiseTemps.Count; i++)
            {
                if (merchandiseTemps[i].GetItem().sItemName == _Str)
                {
                    if (putonMerchandiseTemps[_n] != null)
                    {
                        putonMerchandiseTemps[_n].ChangeImgsAlpha(1);
                    }
                    putonMerchandiseTemps[_n] = merchandiseTemps[i];
                    putonMerchandiseTemps[_n].ChangeImgsAlpha(0);
                }
            }
        }
    }
    public void SettingMerchandiseTemp() {
        for (int i = 0; i < items.Length; i++) {
            Image imgTemp = Instantiate(merchandisePrefab);
            imgTemp.transform.GetComponent<MerchandiseTemp>().SetMerchandise(items[i]);
            merchandiseTemps.Add(imgTemp.transform.GetComponent<MerchandiseTemp>());
            switch (items[i].ItemType){
                case Item.eItemType.Armor:
                    imgTemp.transform.SetParent(trPanel[0], false);
                    break;
                case Item.eItemType.Weapon:
                    imgTemp.transform.SetParent(trPanel[1], false);
                    break;
                case Item.eItemType.Coat:
                    imgTemp.transform.SetParent(trPanel[2], false);
                    break;
                case Item.eItemType.Scope:
                    imgTemp.transform.SetParent(trPanel[3], false);
                    break;
                case Item.eItemType.Pill:
                    imgTemp.transform.SetParent(trPanel[4], false);
                    break;
            }
        }
    }
    public bool PutOnItemCheck(int _ItemTypeNumber, MerchandiseTemp MT) {
        if (playerState.GetGold() < MT.GetItem().fBuyGold) {
            Debug.Log("돈이 부족하여 살 수 없습니다.");
            return false;
        }
        else {
            switch (_ItemTypeNumber)
            {

                case 0:
                    PutOnItem(putonMerchandiseTemps[0], MT);
                    putonMerchandiseTemps[0] = MT;
                    break;
                case 1:
                    PutOnItem(putonMerchandiseTemps[1], MT);
                    putonMerchandiseTemps[1] = MT;
                    break;
                case 2:
                    PutOnItem(putonMerchandiseTemps[2], MT);
                    putonMerchandiseTemps[2] = MT;
                    break;
                case 3:
                    PutOnItem(putonMerchandiseTemps[3], MT);
                    putonMerchandiseTemps[3] = MT;
                    break;
                case 4:
                    ChageStat(MT.GetItem(), true);
                    playerState.SetGold((int)-MT.GetItem().fBuyGold);
                    return false;
            }
            playerState.SetGold((int)-MT.GetItem().fBuyGold);
            return true;
        }
        
    }
    private void PutOnItem(MerchandiseTemp oldMT, MerchandiseTemp newMT) {
        if (oldMT != null) {
            oldMT.ChangeImgsAlpha(1);
            ChageStat(oldMT.GetItem(), false);
        }
        ChageStat(newMT.GetItem(), true);
    }
    private void ChageStat(Item _item, bool isPutOn) {
        float a = 0;
        if (isPutOn) { a = 1f; }
        else { a = -1f; }
        switch (_item.EffectState) {
            case Item.eItemEffectState.Health:
                playerState.SetMaxHP(_item.fEffectCount * a);
                playerState.SetCurHP(_item.fEffectCount * a);
                break;
            case Item.eItemEffectState.Pow:
                playerState.SetPow(_item.fEffectCount * a);
                break;
            case Item.eItemEffectState.Dex:
                playerState.SetDex(_item.fEffectCount * a);
                break;
            case Item.eItemEffectState.Int:
                playerState.SetInt(_item.fEffectCount * a);
                break;
        }
    }
    public void OnClickMarketTag(int _n) {
        if (nBeforeNum != _n) {
            marketTags[nBeforeNum].changeSprite(sprites[0]);
            tables[nBeforeNum].SetActive(false);
            marketTags[_n].changeSprite(sprites[1]);
            tables[_n].SetActive(true);
            nBeforeNum = _n;
        }
    }
}
