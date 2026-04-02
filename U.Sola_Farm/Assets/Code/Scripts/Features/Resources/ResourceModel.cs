using System;
using UnityEngine;

[System.Serializable]
public class ResourceModel : ICopy<ResourceModel>
{
    public string Name;
    public int Pricing;
    public int Amount;

    public event Action<int, int> OnAmountChanged;

    public void SetCantidad(int newCantidad)
    {
        if (Amount != newCantidad)
        {
            int previous = Amount;
            Amount = newCantidad;
            OnAmountChanged?.Invoke(Amount, previous);
        }
    }

    public ResourceModel Copy()
    {
        return (ResourceModel)MemberwiseClone();
    }
}
