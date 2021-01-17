using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonInteractableChange : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnClickButtonInteratableChange(bool _b) {
        button.interactable = _b;
        setChageAlpha(button.interactable);
    }
    private void setChageAlpha(bool isInteractable) {
        if (isInteractable) {
            Color newColor = text.color;
            newColor.a = 1f;
            text.color = newColor;
        }
        else{
            Color newColor = text.color;
            newColor.a = 0.5f;
            text.color = newColor;
        }
    }
}
