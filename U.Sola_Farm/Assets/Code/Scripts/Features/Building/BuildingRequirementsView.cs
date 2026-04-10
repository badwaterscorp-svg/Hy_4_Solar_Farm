using System.Collections.Generic;
using UnityEngine;

public class BuildingRequirementsView : MonoBehaviour
{
    [SerializeField] private BuildingPlaceHandler _buildingPlaceHandler;

    private List<ResourceModel> _requirements;

    private void Start()
    {
        if (_buildingPlaceHandler != null)
        {
            _requirements = _buildingPlaceHandler.GetRequirements();
            ShowRequirements();
        }
    }

    private void ShowRequirements()
    {
        Debug.Log("[BuildingRequirementsView] Building costs:");
        foreach (ResourceModel req in _requirements)
        {
            Debug.Log($"  - {req.Name}: {req.Amount}");
        }
    }
}