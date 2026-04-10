using System;

public interface IInventoryService
{
    public event Action<ResourceModel> OnModelChanged;
    bool AddResource(ResourceModel resource);
    void RemoveResource(ResourceModel resource);
    int GetResourceCount(string resourceName);
    void SaveInventory();
    void LoadInventory();
}
