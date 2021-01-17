using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Menu/item", order =2)]
public class Item : ScriptableObject
{
    public enum eItemType { 
        Armor, Weapon, Coat, Scope, Pill
    }
    public enum eItemEffectState
    {
        Health, Pow, Dex, Int
    }

    public string sItemName;
    public eItemType ItemType;
    public eItemEffectState EffectState;
    public float fEffectCount;
    public float fBuyGold;
}
