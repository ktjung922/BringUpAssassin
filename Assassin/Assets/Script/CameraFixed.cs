using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixed : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float fHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float fWeidth = 1f / fHeight;
        if (fHeight < 1)
        {
            rect.height = fHeight;
            rect.y = (1f - fHeight) / 2f;
        }
        else {
            rect.width = fWeidth;
            rect.x = (1f - fWeidth) / 2f;
        }
        camera.rect = rect;
    }
    //void OnPreCull() => GL.Clear(true, true, Color.black);
}
