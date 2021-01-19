using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSetting : MonoBehaviour
{
    public static VideoSetting Instance;

    FullScreenMode screenMode;
    [SerializeField]
    private Toggle toggleFullScreen;
    [SerializeField]
    private Dropdown ddResolutionDropdown;
    List<Resolution> listResolutions = new List<Resolution>();

    private int nResolition;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InitUI();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void InitUI() {
        for (int i = 0; i < Screen.resolutions.Length; i++) {
            if (Screen.resolutions[i].refreshRate == 60 && Screen.resolutions[i].height == (Screen.resolutions[i].width / 16) * 9) {
                listResolutions.Add(Screen.resolutions[i]);
            }
        }
        ddResolutionDropdown.options.Clear();
        int nOptionNum = 0;
        foreach (Resolution item in listResolutions) {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz";
            ddResolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                ddResolutionDropdown.value = nOptionNum;
            nOptionNum++;
        }
        ddResolutionDropdown.RefreshShownValue();

        toggleFullScreen.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow)?true:false;
    }
    public void DropboxOptionChange(int _n) {
        nResolition = _n;
    }
    public void FullScreen(bool _b) {
        if (_b)
        {
            screenMode = FullScreenMode.FullScreenWindow;
        }
        else {
            screenMode = FullScreenMode.Windowed;
        }
    }

    public void OnClickOK() {
        Screen.SetResolution(listResolutions[nResolition].width,
            listResolutions[nResolition].height,
            screenMode);
    }
}
