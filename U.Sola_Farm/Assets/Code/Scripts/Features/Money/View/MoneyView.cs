using UnityEngine;
using Zenject;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private ResourceCollectionCard _card;
    [SerializeField] private ResourceSheet _moneySheet;

    private IMoneyService _moneyService;

    [Inject]
    public void Initialize(IMoneyService moneyService)
    {
        _moneyService = moneyService;
    }

    private void OnEnable()
    {
        if (_moneyService != null)
        {
            _moneyService.OnBalanceChanged += OnBalanceChanged;
            Redraw();
        }
    }

    private void OnDisable()
    {
        if (_moneyService != null)
            _moneyService.OnBalanceChanged -= OnBalanceChanged;
    }

    private void OnBalanceChanged(int currentBalance, int previousBalance) => Redraw();

    private void Redraw()
    {
        if (_card == null || _moneySheet == null || _moneyService == null) return;
        _card.Configure(_moneySheet, _moneyService.GetBalance());
    }
}
