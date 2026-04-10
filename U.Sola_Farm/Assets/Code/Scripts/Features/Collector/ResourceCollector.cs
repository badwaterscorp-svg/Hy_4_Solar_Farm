using System.Collections.Generic;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    [SerializeField] private List<TriggerDetector> _triggerDetectors = new List<TriggerDetector>();
    
    private IInventoryService _inventoryService;

    private void Awake()
    {
        foreach (var detector in _triggerDetectors)
        {
            if (detector != null)
            {
                detector.OnTriggerEntered += OnResourceTriggerEnter;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var detector in _triggerDetectors)
        {
            if (detector != null)
            {
                detector.OnTriggerEntered -= OnResourceTriggerEnter;
            }
        }
    }

    public void Initialize(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    private void OnResourceTriggerEnter(Transform resourceTransform)
    {
        ResourceModel resource = resourceTransform.GetComponent<ResourceModel>();
        if (resource != null && _inventoryService != null)
        {
            _inventoryService.AddResource(resource);
        }
    }
}