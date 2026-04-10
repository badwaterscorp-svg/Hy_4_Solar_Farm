using System.Collections.Generic;
using UnityEngine;
using B_Extensions;
using System;

public class ResourceInventoryService:IInventoryService
{
    private  List<ResourceModel> _resources = new List<ResourceModel>();

    public event Action<ResourceModel> OnModelChanged;

    public bool AddResource(ResourceModel resource)
    {
        ResourceModel existing = _resources.Find(r => r.Name == resource.Name);
        if (existing != null)
        {
            existing.AddAmount(1);
        }
        else
        {
            ResourceModel copy = resource.Copy();
            _resources.Add(copy);
        }
        SaveInventory();
        OnModelChanged.Invoke(resource);
        return true;
    }

    public  void SaveInventory()
    {
        ResourceWrapper wrapper = new ResourceWrapper
        {
            resources = _resources
        };

        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(KeyStorage.ResourceInventory, json);
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(KeyStorage.ResourceInventory))
        {
            string json = PlayerPrefs.GetString(KeyStorage.ResourceInventory);
            ResourceWrapper wrapper = JsonUtility.FromJson<ResourceWrapper>(json);
            _resources = wrapper.resources ?? new List<ResourceModel>();
        }
    }

    public void RemoveResource(ResourceModel resource)
    {
        OnModelChanged.Invoke(resource);
    }

    public int GetResourceCount(string resourceName)
    {
        throw new NotImplementedException();
    }
}
