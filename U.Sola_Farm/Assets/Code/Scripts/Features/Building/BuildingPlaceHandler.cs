using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Linq;

public class BuildingPlaceHandler : MonoBehaviour
{
    [SerializeField] private TriggerDetector _triggerDetector;
    [SerializeField] private GameObject _buildingPrefab;
    [SerializeField] private float _checkInterval = 0.1f;
    [SerializeField] private List<ResourceModel> _buildingRequirements = new List<ResourceModel>();
    [Header("--UI--")]
    [SerializeField] BuildingPlaceUI _ui;

    [HideInInspector] public List<ResourceModel> storageRequirements = new List<ResourceModel>();

    //private BackPack _backPack;
    bool isBuilt = false;
    private float _timer = 0f;
    ResourceSpawner.Factory _spawnerFactory;
    
    [Inject]
    public void Initialize(ResourceSpawner.Factory spawnerFactory) 
    {
        _spawnerFactory = spawnerFactory;
        storageRequirements = new List<ResourceModel>();
        foreach (ResourceModel req in _buildingRequirements)
        {
            storageRequirements.Add(new ResourceModel(req.Name, req.Amount));
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

        _timer += Time.deltaTime;
        
        if (_timer >= _checkInterval)
        {
            _timer = 0f;
            
            ConsumeResource();

            var pass = storageRequirements.All(r => r.Amount == 0);

            if (pass)
            {
                Build();
            }
        }
    }
    
    private void ConsumeResource()
    {
        var storageReq = storageRequirements.FirstOrDefault(r => r.Amount >0);
        BackPackHandler backPack = PlayerHandler.Instance.AccessBackPackHandler();
        if (storageReq != null)
        {
            if (backPack.GetCountResource(storageReq.Name) > 0)
            {
                backPack.RemoveResource(storageReq);
                storageReq.RemoveAmount(1);
                _ui.UpdateUI();
            }
            else
            {
                print($"[BuildingPlaceHandler] Player does not have required resource: {storageReq.Name}");
            }
        }
        else
        {
            print($"[BuildingPlaceHandler] Player does not have required resource: {storageReq.Name}");
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

[System.Serializable]
public class  BuildingPlaceUI
{
    [SerializeField] private ResourceCollectionCard[] cards;
    BuildingPlaceHandler placeHandler;

    public void Configure(BuildingPlaceHandler placeHandler) 
    {
        this.placeHandler = placeHandler;
        for (int i = 0; i < placeHandler.storageRequirements.Count; i++)
        {
            var req = placeHandler.storageRequirements[i];
            var sheet = ResourcesRepository.Instance.GetSheetByName(req.Name);
            cards[i].Configure(sheet, req.Amount);
        }
    }

    public void UpdateUI()
    {
        foreach (var item in placeHandler.storageRequirements)
        {
            foreach (var card in cards)
            {
                card.Draw(item);
            }
        }
    }
}