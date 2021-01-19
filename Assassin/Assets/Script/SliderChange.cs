using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderChange : MonoBehaviour
{
    public Slider slider;
    public Toggle toggle;
    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("fBackgroundVolume", 1f);
        AudioManager.Instance.setSlider(slider);
        AudioManager.Instance.SetToggle(toggle);
    }

}
