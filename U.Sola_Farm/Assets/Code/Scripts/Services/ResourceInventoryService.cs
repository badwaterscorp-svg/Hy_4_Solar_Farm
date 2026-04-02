using System.Collections.Generic;
using UnityEngine;
using B_Extensions;

public class ResourceInventoryService : Singleton<ResourceInventoryService>, IResourceInventoryService
{
    private List<ResourceModel> _resources = new List<ResourceModel>();

    public void AddResource(ResourceModel resource)
    {
        ResourceModel existing = _resources.Find(r => r.Name == resource.Name);
        if (existing != null)
        {
            existing.SetCantidad(existing.Amount + resource.Amount);
        }
        else
        {
            ResourceModel copy = resource.Copy();
            _resources.Add(copy);
        }
        SaveInventory();
    }

    public void SaveInventory()
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
}
