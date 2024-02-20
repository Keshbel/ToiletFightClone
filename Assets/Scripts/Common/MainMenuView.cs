using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    private ProjectController Project => ProjectController.Instance;

    [Header("Main")] 
    [SerializeField] private TMP_Text creditText;
    [SerializeField] private TMP_Text levelText;
    
    [Header("Damage")] 
    [SerializeField] private Button damageUpgrade;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text damageLevelText;
    
    [Header("Heart")]
    [SerializeField] private Button healthUpgrade;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text healthLevelText;

    private void Awake()
    {
        damageUpgrade.onClick.AddListener(Project.UpgradeDamage);
        damageUpgrade.onClick.AddListener(UpdateData);
        healthUpgrade.onClick.AddListener(Project.UpgradeHealth);
        healthUpgrade.onClick.AddListener(UpdateData);
        
        UpdateData();
    }

    private void OnEnable()
    {
        Project.OnUpdateProject += UpdateData;
    }

    private void OnDisable()
    {
        Project.OnUpdateProject -= UpdateData;
    }

    public void UpdateData()
    {
        creditText.text = Project.credit.ToString();
        levelText.text = "Level = " + Project.level;
        
        damageText.text = Project.damage.ToString();
        healthText.text = Project.health.ToString();

        damageLevelText.text = Project.damageLevel.ToString();
        healthLevelText.text = Project.healthLevel.ToString();
    }
}
