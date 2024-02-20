using System;
using UnityEngine;

[Serializable]
public class UnitHealth
{
    public event Action<int> OnDamageApplied;
    public event Action OnHealthUpdate;
    public event Action OnDeath;

    private bool _isDeath;
    
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value > 0 ? value : 0;
    }
    public int Health
    {
        get => _health;

        set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthUpdate?.Invoke();
            if (_health == 0 && !_isDeath)
            {
                _isDeath = true;
                OnDeath?.Invoke();
            }
        }
    }

    private int _maxHealth;
    private int _health;

    public bool IsAlive => Health > 0;
    
    public void ApplyDamage(int damage)
    {
        if (damage < 0) throw new ArgumentOutOfRangeException(nameof(damage));
        
        Debug.Log("Apply damage = " + damage);
        Health -= damage;
        OnDamageApplied?.Invoke(damage);
    }
}
