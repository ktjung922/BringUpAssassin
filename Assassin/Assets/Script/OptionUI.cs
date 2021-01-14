using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Close() {
        StartCoroutine(CloseOption());
    }
    public IEnumerator CloseOption() {
        anim.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        anim.ResetTrigger("Close");
        gameObject.SetActive(false);
    }
}
