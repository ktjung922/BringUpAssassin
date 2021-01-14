using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonNameChange : MonoBehaviour
{
    [SerializeField]
    private Text text;


    public void ChageText(bool haveQuest) {
        if (haveQuest) {
            text.text = "의뢰 포기";
        }
        else{
            text.text = "의뢰 수주";
        }
    }
}
