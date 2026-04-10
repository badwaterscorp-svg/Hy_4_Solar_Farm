
using B_Extensions;
using UnityEngine;
using Zenject;

public class GameManager : Singleton<GameManager>
{

    IInventoryService _rService;
    [Inject] private void Factory(IInventoryService rService) 
    {
        _rService = rService;
    }

    private new void Awake()
    {
        base.Awake();
        _rService.LoadInventory();
    }
}
