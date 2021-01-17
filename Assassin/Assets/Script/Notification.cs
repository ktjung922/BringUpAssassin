using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Notification : MonoBehaviour
{
    public static GameObject goNotification;
    public static Button Btn_Yes;
    public static Text text_Context;
    // Start is called before the first frame update

    private void Awake()
    {
        goNotification = GameObject.Find("Canvas_Menu").transform.GetChild(1).gameObject;
        Btn_Yes = goNotification.transform.GetChild(1).transform.GetChild(1).GetComponent<Button>();
        text_Context = goNotification.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void  SettingNotification(UnityEngine.Events.UnityAction call, string _Str) {
        NotificationActive(true);
        text_Context.text = _Str;
        Btn_Yes.onClick.RemoveAllListeners();
        Btn_Yes.onClick.AddListener(call);
        Btn_Yes.onClick.AddListener(()=> { NotificationActive(false); });
    }
    public static void NotificationActive(bool _is) {
        goNotification.SetActive(_is);
    }
}
