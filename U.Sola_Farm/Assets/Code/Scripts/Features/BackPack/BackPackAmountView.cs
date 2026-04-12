using DG.Tweening;
using TMPro;
using UnityEngine;

public class BackPackAmountView:MonoBehaviour
{
    [SerializeField] private BackPackHandler _backPackHandler;
    [SerializeField] private TMP_Text _resourceItemPrefab;
    [SerializeField] private AnimationUIController _animaController;
    private void Start()
    {
        ShowData();
    }

    private void OnEnable()
    {
        _backPackHandler.AccessBackPack().OnModelChanged += (t) => 
        {
            print("Model changed: " + t.Name);
            ShowData();
        };
    }

    private void OnDisable()
    {
        _backPackHandler.AccessBackPack().OnModelChanged -= (t) =>
        {
            print("Model changed: " + t.Name);
            ShowData();
        };
    }

    private void ShowData()
    {
        _resourceItemPrefab.text = GetFullAmount();
        //transform.DOShakeScale(0.2f, 0.1f, 10, 90, false);
        _animaController.ActiveAnimation(1);
        CancelInvoke(nameof(Hide));
        if(!PlayerHandler.Instance.AccessBackPackHandler().IsBackPackFull())
            Invoke(nameof(Hide),4f);
    }

    private string GetFullAmount()
    {
        var backBack = _backPackHandler.AccessBackPackAsClass();
        return $"{backBack.AmountResourcesOn}/{backBack.AmountMax}";
    }

    private void Hide() => _animaController.ActiveAnimation(0);
}