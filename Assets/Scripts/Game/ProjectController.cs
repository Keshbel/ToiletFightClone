using System;
using UnityEngine;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-999)]
public class ProjectController : MonoBehaviour
{
    public static ProjectController Instance;
    public const string Credit = "Credit";
    public const string Level = "Level";
    public const string Damage = "Damage";
    public const string DamageLevel = "DamageLevel";
    public const string Health = "Health";
    public const string HealthLevel = "HealthLevel";

    public Action OnUpdateProject;
    
    [Header("Credit")]
    public int credit = 300;
    public int creditForLevel = 200;
    public uint level;

    [Header("Upgrades")] 
    public uint damage = 1;
    public uint damageLevel = 1;
    public uint health = 10;
    public uint healthLevel = 1;
    
    [Header("Price")]
    public int damagePrice = 250;
    public int healthPrice = 200;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        //Singleton
        if (Instance) Destroy(gameObject);
        else Instance = this;
        
        credit = PlayerPrefs.GetInt(Credit, 300);
        level = (uint)PlayerPrefs.GetInt(Level, 1);
        
        health = (uint)PlayerPrefs.GetInt(Health, 10);
        damage = (uint)PlayerPrefs.GetInt(Damage, 1);
        healthLevel = (uint)PlayerPrefs.GetInt(HealthLevel, 1);
        damageLevel = (uint)PlayerPrefs.GetInt(DamageLevel, 1);
    }
    
    public void UpgradeHealth()
    {
        if (credit < healthPrice) return;
        
        var random = (uint)Random.Range(1, 3) * 5;
        health += random;
        healthLevel++;
        
        PlayerPrefs.SetInt(Health, (int)health);
        PlayerPrefs.SetInt(HealthLevel, (int)healthLevel);
        ChangeCredit(-healthPrice);
    }

    public void UpgradeDamage()
    {
        if (credit < damagePrice) return;
        
        var random = (uint)Random.Range(1, 3);
        damage += random;
        damageLevel++;
        
        PlayerPrefs.SetInt(Damage, (int)damage);
        PlayerPrefs.SetInt(DamageLevel, (int)damageLevel);
        ChangeCredit(-damagePrice);
    }

    private void ChangeCredit(int value)
    {
        credit += value;
        PlayerPrefs.SetInt(Credit, credit);
        
        OnUpdateProject?.Invoke();
    }

    public void NewLevel()
    {
        level++;
        
        PlayerPrefs.SetInt(Level, (int)level);
        
        OnUpdateProject?.Invoke();
    }

    public int GetAwardValue()
    {
        return creditForLevel + (int)level * 50;
    }
    
    public void GiveAward()
    {
        ChangeCredit(GetAwardValue());
    }

    public void ResetAll()
    {
        damage = 1;
        damageLevel = 1;
        health = 10;
        healthLevel = 1;
        level = 1;
        credit = 300;
        
        PlayerPrefs.SetInt(Damage, (int)damage);
        PlayerPrefs.SetInt(DamageLevel, (int)damageLevel);
        PlayerPrefs.SetInt(Health, (int)health);
        PlayerPrefs.SetInt(HealthLevel, (int)healthLevel);
        PlayerPrefs.SetInt(Level, (int)level);
        PlayerPrefs.SetInt(Credit, (int)credit);
        
        OnUpdateProject?.Invoke();
    }
}
