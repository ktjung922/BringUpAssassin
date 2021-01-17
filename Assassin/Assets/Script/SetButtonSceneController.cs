using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetButtonSceneController : MonoBehaviour
{
    public Button Btn;
    public string str;

    // Start is called before the first frame update
    void Start()
    {
        Btn = GetComponent<Button>();
        AddFun(str);
    }

    private void AddFun(string _str) {
        if (_str == "NewStart")
        {
            Btn.onClick.AddListener(SceneController.Instance.Btn_NewStart);
        }
        else if (_str == "Return")
        {
            Btn.onClick.AddListener(SceneController.Instance.Btn_Return);
        }
        else if (_str == "Quit")
        {
            Btn.onClick.AddListener(SceneController.Instance.Btn_Quit);
        }
        else if (_str == "LoadStart") {
            Btn.onClick.AddListener(SceneController.Instance.Btn_LoadStart);
        }
    }
}
