using UnityEngine;

public class MoneyCaller : MonoBehaviour
{
    [SerializeField] private int _amount;
    [SerializeField] private MoneyOperation _operation = MoneyOperation.Add;

    public enum MoneyOperation
    {
        Add,
        Remove
    }

    public void Call()
    {
        if (_operation == MoneyOperation.Add)
            MoneyService.Instance.Add(_amount);
        else
            MoneyService.Instance.Remove(_amount);
    }
}
