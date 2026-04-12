using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BackPackScreenManager : MonoBehaviour
{
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private ResourceCollectionCard _cardPrefab;

    private IInventoryService _inventoryService;
    private List<ResourceCollectionCard> _cards = new List<ResourceCollectionCard>();

    [Inject]
    public void Initialize(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    private IEnumerator Start()
    {
        RefreshCards();
        yield return new  WaitUntil(() => _inventoryService != null);
        _inventoryService.OnModelChanged += OnBackPackChanged;
    }

    private void OnDestroy()
    {
        if (_inventoryService != null)
            _inventoryService.OnModelChanged -= OnBackPackChanged;
    }

    private void OnBackPackChanged(ResourceModel resource)
    {
        RefreshCards();
    }

    public void RefreshCards()
    {
        ClearCards();

        ResourceWrapper wrapper = LoadBackPackData();
        if (wrapper == null || wrapper.resources == null) return;

        foreach (ResourceModel resource in wrapper.resources)
        {
            ResourceSheet sheet = ResourcesRepository.Instance.GetSheetByName(resource.Name);
            if (sheet != null)
            {
                ResourceCollectionCard card = Instantiate(_cardPrefab, _cardContainer);
                card.Draw(sheet, resource.Amount);
                _cards.Add(card);
            }
        }
    }

    private ResourceWrapper LoadBackPackData()
    {
        var wrapper = PlayerHandler.Instance.AccessBackPackHandler().GetBackPackData();
        return wrapper;
    }

    private void ClearCards()
    {
        foreach (var card in _cards)
        {
            if (card != null) Destroy(card.gameObject);
        }
        _cards.Clear();
    }
}