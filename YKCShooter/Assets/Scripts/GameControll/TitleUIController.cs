using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TitleUIController : MonoBehaviour
{
    [SerializeField] GameObject titleLogo;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject[] titleButton;

    [SerializeField] int logoY;
    [SerializeField] int buttonX;

    [Header("UIState")]
    [SerializeField] List<GameObject> uiStateObject = new List<GameObject>();

    private void Start()
    {
        startText.GetComponent<TextMeshProUGUI>().DOFade(0, 1.2f).SetLoops(-1, LoopType.Yoyo);
    }

    // 맨 처음 터치
    public void TouchTitle()
    {
        titleLogo.transform.DOMoveY(logoY, 1).SetEase(Ease.OutQuad);
        startText.SetActive(false);
        for (int i = 0; i < titleButton.Length; i++)
        {
            titleButton[i].transform.DOMoveX(buttonX, (i+1)/2f).SetEase(Ease.InSine);
        }
    }
    public void UpdateUI(TitleUIState state)
    {

    }
}
[System.Serializable]
public enum TitleUIState : byte
{
    Logo = 0,
    StageSelect = 1,
    Setting = 2,
    HowToPlay = 3
}
