using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    private IHaveHealth _unit;

    [Header("Components")] 
    [SerializeField] private Image healthBar;
    [SerializeField] private TMP_Text healthText;
    
    [Header("Damage")]
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private CanvasGroup damageCanvasGroup;

    public Tweener DamageTweener;
    
    private void Start()
    {
        _unit = gameObject.GetComponent<IHaveHealth>();

        _unit.UnitHealth.OnHealthUpdate += UpdateHealthVisual;
        UpdateHealthVisual();
        
        _unit.UnitHealth.OnDamageApplied += ShowAppliedDamage;
    }

    private void OnDisable()
    {
        _unit.UnitHealth.OnDamageApplied -= ShowAppliedDamage;
    }

    private void UpdateHealthVisual()
    {
        var currentHealth = _unit.UnitHealth.Health;
        healthText.text = currentHealth.ToString();
        healthBar.fillAmount = (float)currentHealth / _unit.UnitHealth.MaxHealth;
    }

    private void ShowAppliedDamage(int value)
    {
        damageText.text = "-" + value;
        
        DamageTweener?.Kill(true);
        DamageTweener = damageCanvasGroup.DOFade(1, .5f).OnComplete(()=>
        {
            damageCanvasGroup.DOFade(1, 2f).OnComplete(() => damageCanvasGroup.DOFade(0, .5f));
        });
    }
}
