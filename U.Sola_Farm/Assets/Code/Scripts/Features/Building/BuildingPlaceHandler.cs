using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuildingPlaceHandler : MonoBehaviour
{
    [SerializeField] private TriggerDetector _triggerDetector;
    [SerializeField] private List<ResourceModel> _buildingRequirements = new List<ResourceModel>();
    [SerializeField] private GameObject _buildingPrefab;

    //private BackPack _backPack;
    bool isBuilt = false;
    ResourceSpawner.Factory _spawnerFactory;
    [Inject]
    public void Initialize(ResourceSpawner.Factory spawnerFactory) 
    {
        _spawnerFactory = spawnerFactory;
    }

    private void OnEnable()
    {
        _triggerDetector.OnTriggerEntered += OnTriggerEntered;
    }

    private void OnDisable()
    {
        if (_triggerDetector != null)
            _triggerDetector.OnTriggerEntered -= OnTriggerEntered;
    }


    private void OnTriggerEntered(Transform other)
    {
        if (isBuilt)
            return;

        print($"[BuildingPlaceHandler] Trigger entered by {other.name}");
        if (HasEnoughResources())
        {
            ConsumeResources();
            Build();
        }
        else
        {
            Debug.Log($"[BuildingPlaceHandler] Not enough resources to build.");
            LogRequirements();
        }
    }

    public bool HasEnoughResources()
    {
        foreach (ResourceModel requirement in _buildingRequirements)
        {
            int available = PlayerHandler.Instance.AccessBackPackHandler().GetCountResource(requirement.Name);
            if (available < requirement.Amount)
            {
                Debug.Log($"[BuildingPlaceHandler] Missing {requirement.Name}: need {requirement.Amount}, have {available}");
                return false;
            }
        }
        Debug.Log("[BuildingPlaceHandler] Enough resources to build!");
        return true;
    }

    private void ConsumeResources()
    {
        foreach (ResourceModel requirement in _buildingRequirements)
        {
            for (int i = 0; i < requirement.Amount; i++)
            {
                PlayerHandler.Instance.AccessBackPackHandler().RemoveResource(requirement);
                Debug.Log($"[BuildingPlaceHandler] Consumed 1 {requirement.Name}");
            }
        }
    }

    private void Build()
    {
        if (_buildingPrefab != null)
        {
            isBuilt = true;
            var clone = _spawnerFactory.Create();
            clone.transform.position = transform.position + Vector3.up;
            clone.transform.rotation = transform.rotation;
            //Instantiate(_buildingPrefab, transform.position, transform.rotation);
            Debug.Log("[BuildingPlaceHandler] Building constructed!");
        }
        else
        {
            Debug.LogWarning("[BuildingPlaceHandler] No building prefab assigned!");
        }
    }

    public List<ResourceModel> GetRequirements() => _buildingRequirements;

    private void LogRequirements()
    {
        Debug.Log("[BuildingPlaceHandler] Requirements:");
        foreach (ResourceModel req in _buildingRequirements)
        {
            int available = PlayerHandler.Instance.AccessBackPackHandler().GetCountResource(req.Name);
            Debug.Log($"  - {req.Name}: {available}/{req.Amount}");
        }
    }
}