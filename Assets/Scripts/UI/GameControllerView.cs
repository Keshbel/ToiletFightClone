using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerView : MonoBehaviour
{
    private GameSingleton Game => GameSingleton.Instance;
    
    [Header("Canvas Group")] 
    [SerializeField] private CanvasGroup canvasGroup;
    
    [Header("Panels")]
    [SerializeField] private GameObject creditPanel;
    
    [Header("Texts")]
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private TMP_Text creditText;
    [SerializeField] private TMP_Text nextGameText;
    
    [Header("Buttons")]
    [SerializeField] private Button menuButton;
    [SerializeField] private Button continueButton;

    [Header("Options")] [SerializeField] private float duration = 0.25f;

    private void Awake()
    {
        SetFade(false,0);
        
        menuButton.onClick.AddListener(Game.gameController.ReturnInMenu);
    }

    public void ShowEndPanel(bool isVictory)
    {
        if (!creditPanel) return;
        
        creditPanel.SetActive(isVictory);
        resultText.text = isVictory ? "Victory" : "Defeat";
        nextGameText.text = isVictory ? "Next Game" : "Reset";
        creditText.text = "+" + ProjectController.Instance.GetAwardValue();
        
        continueButton.onClick.AddListener(isVictory ? Game.gameController.NextGame : Game.gameController.ResetGame);
        
        SetFade(true, duration);
    }

    private void SetFade(bool isOn, float durationFade)
    {
        canvasGroup.DOFade(Convert.ToInt32(isOn), durationFade).OnComplete(() =>
        {
            canvasGroup.interactable = isOn;
            canvasGroup.blocksRaycasts = isOn;
        });
    }
}
