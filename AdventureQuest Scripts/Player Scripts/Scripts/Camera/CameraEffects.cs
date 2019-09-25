using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraEffects : MonoBehaviour
{
    public Canvas ghostCanvas;
    public Image fadeImage;
    public GameObject fadeGO;
    // Start is called before the first frame update
    void Start()
    {
        fadeGO.SetActive(true);
        fadeImage.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        fadeImage.CrossFadeAlpha(1, 3, false);
        ghostCanvas.sortingOrder = 100;
    }

    public void FadeOut()
    {
        fadeImage.CrossFadeAlpha(0, 3, false);
        ghostCanvas.sortingOrder = -10;
    }
}
