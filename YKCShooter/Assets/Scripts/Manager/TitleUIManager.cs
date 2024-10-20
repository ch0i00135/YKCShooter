using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TitleUIManager : MonoBehaviour
{
    public GameObject titleLogo;
    public GameObject startText;
    public GameObject[] titleButton;

    public int logoY;
    public int buttonX;

    private void Start()
    {
        startText.GetComponent<TextMeshProUGUI>().DOFade(0,1).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TouchTitle();
        }
    }

    public void TouchTitle()
    {
        titleLogo.transform.DOMoveY(logoY, 1).SetEase(Ease.OutQuad);
        startText.SetActive(false);
        for (int i = 0; i < titleButton.Length; i++)
        {
            titleButton[i].transform.DOMoveX(buttonX, (i+1)/2f).SetEase(Ease.InSine);
        }
    }
}
