using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MovingUI
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private MarketTag[] marketTags;
    [SerializeField]
    private int nBeforeNum = 0;
    [SerializeField]
    private GameObject[] tables;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
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
