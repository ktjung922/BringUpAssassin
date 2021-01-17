using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoldTemp : MonoBehaviour
{
    [SerializeField]
    private Text txt;
    private void Awake()
    {
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        animFunction();
    }
    public void SetGold(int _n) {
        if (_n < 0)
        {
            txt.text = "-" + _n;
            txt.color = Color.red;
        }
        else {
            txt.text = "+" + _n;
            txt.color = Color.green;
        }
    }
    public void animFunction() {
        transform.Translate(Vector3.up * Time.deltaTime * 2f);
        Color color = txt.color;
        color.a -= Time.deltaTime * 0.4f;
        txt.color = color;
    }
}
