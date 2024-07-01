using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IHittable
{

    [SerializeField]
    protected int maxHealth;

    [SerializeField]
    protected int currentHealth;

    public UnityEvent<int> OnHealthValueChange;
    public UnityEvent<int> OnInitialMaxHealth;
    private void Start()
    {
        Inititalize(maxHealth);
    }
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            OnHealthValueChange?.Invoke(currentHealth);
        }
    }

    public UnityEvent OnGetHit;
    public UnityEvent OnDie;
    public UnityEvent OnAddHealth;

    public virtual void GetHit(GameObject gameObject, int weaponDamage)
    {
        GetHit(weaponDamage);
    }

    public void GetHit(int weaponDamage)
    {
        CurrentHealth -= weaponDamage;
        if (CurrentHealth <= 0)
        {
            OnDie?.Invoke();
        }
        else
        {
            OnGetHit?.Invoke();
        }
    }

    public virtual void GetHeal(int healAmoutn)
    {
        CurrentHealth += healAmoutn;
        OnAddHealth?.Invoke();
    }

    public virtual void Inititalize(int health)
    {
        maxHealth = health;
        OnInitialMaxHealth?.Invoke(maxHealth);
        CurrentHealth = health;
    }
}
