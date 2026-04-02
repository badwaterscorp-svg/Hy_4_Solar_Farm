using System;
using UnityEngine;

public class LifeModel
{
    public LifeData data;
    public event Action OnDead;
    public event Action<int,int> OnDamage =null;
    public event Action<int, int> OnHeal = null;

    public LifeModel(LifeData hp)
    {
        data = hp;
    }

    public void Heal(int healAmount) 
    {
        if (healAmount <= 0 || (data.CurrentHP +healAmount)>= data.MaxHP)
            return;
        var before = data.CurrentHP;
        data.CurrentHP += healAmount;
        OnHeal?.Invoke(before, data.CurrentHP);
    }

    public void MakeDamageBase()
    {
        if (data.CurrentHP > 10000)
            return;
        var before = data.CurrentHP;
        data.CurrentHP--;
        OnDamage?.Invoke(before, data.CurrentHP);
        CheckDead();
    }

    public void MakeDamage(int amount)
    {
        var before = data.CurrentHP;
        data.CurrentHP -= amount;
        OnDamage?.Invoke(before, data.CurrentHP);
        CheckDead();
    }

    public bool IsDead => data.CurrentHP <= 0;
    public bool CheckDead() 
    {
        if (IsDead)
        {
            OnDead?.Invoke();
            return true;
        }
        return false;
    }
}
