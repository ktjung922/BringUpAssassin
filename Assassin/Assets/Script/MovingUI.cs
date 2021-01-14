using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUI : MonoBehaviour
{
    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected GameObject Background;

    protected bool isOpen = false;

    [SerializeField]
    protected MovingUI anotherMovingUI;

    // Start is called before the first frame update
    protected void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnClickButtonUI(int _n)
    {
        if (isOpen)
        {
            anim.SetTrigger("Close");
            Background.SetActive(false);
        }
        else
        {
            anim.SetTrigger("Open");
            Background.SetActive(true);
        }
        isOpen = !isOpen;
        if (anotherMovingUI.getIsOpen() && _n != 2) {
            anotherMovingUI.OnClickButtonUI(2);
        }
    }
    public bool getIsOpen() {
        return isOpen;
    }
}
