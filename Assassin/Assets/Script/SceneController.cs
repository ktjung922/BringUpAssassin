using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController: MonoBehaviour
{
    public static SceneController Instance;

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Btn_NewStart() {
        SceneManager.LoadScene("GameScene");
        Invoke("Doit", 0.8f);
    }
    public void Doit() {
        Debug.Log("Do it");
        SLManager.Instance.getComponents();
    }
    public void LoadData() {
        SLManager.Instance._load();
    }
    public void Btn_LoadStart()
    {
        SceneManager.LoadScene("GameScene");
        Invoke("Doit", 0.8f);
        Invoke("LoadData",0.9f);
    }
    public void Btn_Return()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Btn_Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
