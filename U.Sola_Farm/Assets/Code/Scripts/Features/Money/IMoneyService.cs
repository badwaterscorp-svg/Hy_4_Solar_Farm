using System;

public interface IMoneyService
{
    event Action<int, int> OnBalanceChanged;
    int GetBalance();
    void Add(int amount);
    bool Remove(int amount);
}
