using System;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : IInventoryService
{
    private List<ResourceModel> _resources = new List<ResourceModel>();

    public event Action<ResourceModel> OnModelChanged;

    public int AmountMax { get
        {
            return 10;
        }
        private set { }
    }

    public int AmountResourcesOnPack {
        get
        {
            int amount = 0;
            foreach (var resource in _resources)
            {
                amount += resource.Amount;
            }
            return amount;
        }
    }

    public void RemoveResource(ResourceModel resource)
    {
        ResourceModel existing = _resources.Find(r => r.Name == resource.Name);
        if (existing == null)
        {
            return;
        }
        existing.RemoveAmount(1);

        if (existing.Amount <= 0)
        {
            _resources.Remove(existing);
        }
        SaveInventory();
        OnModelChanged?.Invoke(resource);
    }

    public int GetResourceCount(string resourceName)
    {
        ResourceModel existing = _resources.Find(r => r.Name == resourceName);
        return existing?.Amount ?? 0;
    }


    public bool IsBackPackFull=> AmountResourcesOnPack >= AmountMax;

    public bool AddResource(ResourceModel resource)
    {

        if (IsBackPackFull)
        { 
            return false;
        }
        ResourceModel existing = _resources.Find(r => r.Name == resource.Name);

        if (existing != null)
        {
            existing.AddAmount(1);
        }
        else
        {
            ResourceModel copy = resource.Copy();
            copy.AddAmount(1);
            _resources.Add(copy);
        }

        SaveInventory();
        OnModelChanged?.Invoke(resource);
        return true;
    }

    public void SaveInventory()
    {
        ResourceWrapper wrapper = new ResourceWrapper
        {
            resources = _resources
        };

        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(KeyStorage.BackPackItems, json);
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(KeyStorage.BackPackItems))
        {
            string json = PlayerPrefs.GetString(KeyStorage.BackPackItems);
            ResourceWrapper wrapper = JsonUtility.FromJson<ResourceWrapper>(json);
            _resources = wrapper.resources ?? new List<ResourceModel>();
        }
    }
}
