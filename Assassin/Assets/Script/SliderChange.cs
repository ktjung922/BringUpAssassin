using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderChange : MonoBehaviour
{
    public Slider slider;
    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("fBackgroundVolume", 1f);
        AudioManager.Instance.setSlider(slider);
    }

}
