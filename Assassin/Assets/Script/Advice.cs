using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Advice : MonoBehaviour
{
    public static GameObject goAdvice;
    public static Text text_Context;

    private void Awake()
    {
        goAdvice = GameObject.Find("Canvas_Menu").transform.GetChild(2).gameObject;
        text_Context = goAdvice.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
    }
    public static void SettingAdvice(string _Str)
    {
        goAdvice.SetActive(true);
        text_Context.text = _Str;
    }
}
