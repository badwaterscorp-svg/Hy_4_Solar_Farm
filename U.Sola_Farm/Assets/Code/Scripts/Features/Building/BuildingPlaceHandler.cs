using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Linq;

public class BuildingPlaceHandler : MonoBehaviour
{
    [SerializeField] private string typeBuilding = "Solar Energy";
    [SerializeField] private TriggerDetector _triggerDetector;
    [SerializeField] private GameObject _buildingPrefab;
    [SerializeField] private float _checkInterval = 0.1f;
    [SerializeField] private List<ResourceModel> _buildingRequirements = new List<ResourceModel>();
    [Header("--UI--")]
    [SerializeField] BuildingPlaceUI _ui;
    [Header("--Data--")]
    [SerializeField] string IDSite = "Here is a ID ToSave";
    [HideInInspector] public List<ResourceModel> storageRequirements = new List<ResourceModel>();

    //private BackPack _backPack;
    bool isBuilt = false;
    private float _timer = 0f;
    // factories

    [Inject(Id = "SolarEnergy")] SolarPanelSpawnerHandler.Factory _spawnerFactorySolar;
    [Inject(Id = "Water")] WaterSpawnerHandler.Factory _spawnerFactoryWater;
    [Inject] private IBuildingSiteDataService _dataService;

    private void Awake()
    {
        storageRequirements = new List<ResourceModel>();
        foreach (ResourceModel req in _buildingRequirements)
        {
            storageRequirements.Add(new ResourceModel(req.Name, req.Amount));
        }

        if (_dataService.TryLoad(IDSite, out ResourceWrapper wrapper))
        { 
            storageRequirements = wrapper.resources;
            TryBuild();

        }
    }

    private void OnEnable()
    {
        _ui.Configure(this);
        _triggerDetector.OnTriggerStayed += OnTriggerStayed;
    }

    private void OnDisable()
    {
        if (_triggerDetector != null)
            _triggerDetector.OnTriggerStayed -= OnTriggerStayed;
    }

    private void OnTriggerStayed(Transform other)
    {
        if (isBuilt)
            return;

        _timer -= Time.deltaTime;
        
        if (_timer <= 0)
        {
            _timer = _checkInterval;
            ConsumeResource();
            TryBuild();
        }
    }

    private void TryBuild() 
    {
        var pass = storageRequirements.All(r => r.Amount == 0);
        if (pass)
        {
            Build();
        }
    }
    
    private void ConsumeResource()
    {
        foreach (var item in storageRequirements)
        {
            BackPackHandler backPack = PlayerHandler.Instance.AccessBackPackHandler();
            if (backPack.GetCountResource(item.Name) > 0 && item.Amount>0)
            {
                PlayerHandler.Instance.ThrowResource(item, transform, _checkInterval);
                backPack.RemoveResource(item);
                item.RemoveAmount(1);
                _ui.UpdateUI();
                SaveStorageRequirements();
            }
            else
            {
                print($"[BuildingPlaceHandler] Player does not have required resource: {item.Name}");
            }
        }
    }

    private void SaveStorageRequirements()
    {
        ResourceWrapper wrapper = new ResourceWrapper
        {
            resources = storageRequirements
        };
        _dataService.Save(IDSite, wrapper);
    }


    private void Build()
    {
        if (_buildingPrefab != null)
        {
            isBuilt = true;

            BaseSpawnerResource clone = null;

            if (typeBuilding.Equals("Solar Energy"))
                clone = _spawnerFactorySolar.Create();
            else if (typeBuilding.Equals("Water"))
                clone = _spawnerFactoryWater.Create();

            clone.transform.position = transform.position + Vector3.up;
            clone.transform.rotation = transform.rotation;
            gameObject.SetActive(false);
            Debug.Log("[BuildingPlaceHandler] Building constructed!");
        }
        else
        {
            Debug.LogWarning("[BuildingPlaceHandler] No building prefab assigned!");
        }
    }

    public List<ResourceModel> GetRequirements() => _buildingRequirements;
}
