using System;
using B_Extensions;
using UnityEngine;

public class MoneyService : Singleton<MoneyService>, IMoneyService
{
    private int _balance;

    public event Action<int, int> OnBalanceChanged;

    protected override void Awake()
    {
        base.Awake();
        LoadBalance();
    }

    public int GetBalance() => _balance;

    public void Add(int amount)
    {
        if (amount <= 0) return;

        int previous = _balance;
        _balance += amount;
        Save();
        OnBalanceChanged?.Invoke(_balance, previous);
    }

    public bool Remove(int amount)
    {
        if (amount <= 0) return true;
        if (amount > _balance) return false;

        int previous = _balance;
        _balance -= amount;
        Save();
        OnBalanceChanged?.Invoke(_balance, previous);
        return true;
    }

    public void LoadBalance()
    {
        _balance = PlayerPrefs.GetInt(KeyStorage.MoneyBalance_I, 0);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(KeyStorage.MoneyBalance_I, _balance);
        PlayerPrefs.Save();
    }
}
