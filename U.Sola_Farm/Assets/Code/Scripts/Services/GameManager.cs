
using B_Extensions;
using UnityEngine;
using Zenject;

public class GameManager : Singleton<GameManager>
{

    IResourceInventoryService _rService;
    [Inject] private void Factory(IResourceInventoryService rService) 
    {
        _rService = rService;
    }

    private new void Awake()
    {
        base.Awake();
        _rService.LoadInventory();
    }
}
