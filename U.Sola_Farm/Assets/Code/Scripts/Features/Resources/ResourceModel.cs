using System;
using UnityEngine;

[System.Serializable]
public class ResourceModel : ICopy<ResourceModel>
{
    public string Name;
    public int Amount;
    public event Action<int, int> OnAmountChanged;

    public void AddAmount(int newAmount)
    {
        int previous = Amount;
        Amount += newAmount;
        //Debug.Log($"Resource {Name} amount changed from {previous} to {Amount}");
        OnAmountChanged?.Invoke(Amount, previous);
    }

    public void RemoveAmount(int removeAmount)
    {
        int previous = Amount;
        Amount -= removeAmount;
        //Debug.Log($"Resource {Name} amount changed from {previous} to {Amount}");
        OnAmountChanged?.Invoke(Amount, previous);
    }

    public ResourceModel Copy()
    {
        return (ResourceModel)MemberwiseClone();
    }
}
