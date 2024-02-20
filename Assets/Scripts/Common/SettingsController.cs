using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    private const string Volume = "Volume";
    
    [Header("Components")] 
    [SerializeField] private AudioMixer audioMixer;

    [Header("View")] 
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Image soundColor;

    [Header("Buttons")] 
    [SerializeField] private Button openPanel;
    [SerializeField] private Button closePanel;
    [SerializeField] private Button resetButton;

    [Header("Options")] 
    [SerializeField] private float duration = 0.25f;

    private void Awake()
    {
        SetFade(false, 0);
        
        openPanel.onClick.AddListener(()=>SetFade(true, duration));
        closePanel.onClick.AddListener(()=>SetFade(false, duration));
        
        soundToggle.onValueChanged.AddListener(SetSound);
        
        soundToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetFloat(Volume, 1));
        
        resetButton.onClick.AddListener(ProjectController.Instance.ResetAll);
    }
    
    private void SetSound(bool isOn)
    {
        audioMixer.SetFloat("Volume", isOn ? 0 : -80);
        soundColor.color = isOn ? Color.green : Color.gray;
        
        PlayerPrefs.SetInt(Volume, Convert.ToInt32(isOn));
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
