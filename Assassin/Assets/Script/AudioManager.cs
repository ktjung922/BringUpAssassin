using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private AudioSource audioBack;

    private float fBackgroundVolume = 1f;


    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        fBackgroundVolume = PlayerPrefs.GetFloat("fBackgroundVolume", 1f);
        slider.value = fBackgroundVolume;
        audioBack.volume = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
        {
            SliderVolume();
        }
    }
    public void SliderVolume()
    {
        audioBack.volume = slider.value;
        fBackgroundVolume = slider.value;
        PlayerPrefs.SetFloat("fBackgroundVolume", fBackgroundVolume);
    }
    public void setSlider(Slider slider) {
        this.slider = slider;
    }
}
