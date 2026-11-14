using B_Extensions;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BackPackImproveScreen : Singleton<BackPackImproveScreen>
{
    [SerializeField] private ButtonImproveBackPack _improveButton;
    [SerializeField] private Transform _costCardContainer;
    [SerializeField] private ResourceCollectionCard _costCardPrefab;
    [SerializeField] private List<ResourceModel> _costs = new List<ResourceModel>();

    private IInventoryService _inventoryService;
    private readonly List<ResourceCollectionCard> _cards = new List<ResourceCollectionCard>();

    [Inject]
    public void Initialize(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    private void OnEnable()
    {
        if (_inventoryService != null)
            _inventoryService.OnModelChanged += OnInventoryChanged;
        RefreshState();
    }

    private void OnDisable()
    {
        if (_inventoryService != null)
            _inventoryService.OnModelChanged -= OnInventoryChanged;
    }

    private void OnInventoryChanged(ResourceModel resource) => RefreshState();

    public void ConsumeCost()
    {
        BackPackHandler backPack = PlayerHandler.Instance.AccessBackPackHandler();
        if (backPack == null) return;

        foreach (ResourceModel cost in _costs)
        {
            if (cost == null || string.IsNullOrEmpty(cost.Name)) continue;

            if (backPack.GetCountResource(cost.Name) > 0 && cost.Amount > 0)
            {
                backPack.RemoveResource(cost);
                cost.RemoveAmount(1);
            }
            else
            {
                print($"[BackPackImproveScreen] Player does not have required resource: {cost.Name}");
            }
        }
    }

    public void RefreshState()
    {
        BuildCostCards();

        if (_inventoryService == null || _improveButton == null)
            return;

        bool canAfford = CanAffordAllCosts();
        if (canAfford) _improveButton.EnableButton();
        else _improveButton.DisableButton();
    }

    private bool CanAffordAllCosts()
    {
        foreach (ResourceModel cost in _costs)
        {
            if (cost == null || string.IsNullOrEmpty(cost.Name)) continue;
            if (_inventoryService.GetResourceCount(cost.Name) < cost.Amount)
                return false;
        }
        return true;
    }

    private void BuildCostCards()
    {
        ClearCards();

        if (_costCardContainer == null || _costCardPrefab == null) return;

        foreach (ResourceModel cost in _costs)
        {
            if (cost == null || string.IsNullOrEmpty(cost.Name)) continue;

            ResourceSheet sheet = ResourcesRepository.Instance.GetSheetByName(cost.Name);
            if (sheet == null) continue;

            ResourceCollectionCard card = Instantiate(_costCardPrefab, _costCardContainer);
            card.Configure(sheet, cost.Amount);
            _cards.Add(card);
        }
    }

    private void ClearCards()
    {
        foreach (ResourceCollectionCard card in _cards)
        {
            if (card != null) Destroy(card.gameObject);
        }
        _cards.Clear();
    }
}
