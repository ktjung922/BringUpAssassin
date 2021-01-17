using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MerchandiseTemp : MonoBehaviour
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private Text textName;
    [SerializeField]
    private Text textEffect;
    [SerializeField]
    private Text textBuyGold;

    [SerializeField]
    private Image[] imgs;
    [SerializeField]
    private GameObject goText;
    [SerializeField]
    private Button btn;
    [SerializeField]
    private MarketUI marketUI;

    void Awake() {
        btn = GetComponent<Button>();
        marketUI = GameObject.Find("MarketUI").GetComponent<MarketUI>();
    }
    public void ChangeImgsAlpha(int _n) {
        for (int i = 0; i < imgs.Length; i++) {
            Color color = imgs[i].color;
            color.a = 0.5f + (0.5f *_n);
            imgs[i].color = color;
        }
        if (_n == 0)
        {
            goText.SetActive(true);
            btn.interactable = false;
        }
        else {
            goText.SetActive(false);
            btn.interactable = true;
        }
    }

    public void SetMerchandise(Item _item) {
        string sTemp = "";
        switch (_item.EffectState) {
            case Item.eItemEffectState.Health:
                sTemp = "최대 체력:";
                break;
            case Item.eItemEffectState.Pow:
                sTemp = "전투력:";
                break;
            case Item.eItemEffectState.Dex:
                sTemp = "은밀함:";
                break;
            case Item.eItemEffectState.Int:
                sTemp = "수집력:";
                break;
        }
        item = _item;
        textName.text = _item.sItemName;
        textEffect.text = sTemp + " + " + _item.fEffectCount;
        textBuyGold.text = _item.fBuyGold.ToString();
    }

    public void OnClickTemp()
    {
        Notification.SettingNotification(BuyMerchandise, item.sItemName + "을 사시겠습니까?\n비용: " + item.fBuyGold);
    }
    public void BuyMerchandise() {
        if (marketUI.PutOnItemCheck((int)item.ItemType, this))
        {
            ChangeImgsAlpha(0);
        }
    }
    public Item GetItem() {
        return item;
    }
}
