using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Button buttonSave;
    [SerializeField]
    private Button buttonLoad;
    [SerializeField]
    private SLManager sl;


    private void Start()
    {
        anim = GetComponent<Animator>();
        sl = GameObject.Find("SLManager").GetComponent<SLManager>();
        if (buttonSave != null)
        {
            buttonSave.onClick.AddListener(save);
        }
        if (buttonLoad != null) {
            buttonLoad.onClick.AddListener(load);
        }
    }

    public void Close() {
        StartCoroutine(CloseOption());
    }
    public IEnumerator CloseOption()
    {
        anim.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        anim.ResetTrigger("Close");
        gameObject.SetActive(false);
    }
    public void save() {
        sl._save();
    }
    public void load() {
        sl._load();
    }
}
