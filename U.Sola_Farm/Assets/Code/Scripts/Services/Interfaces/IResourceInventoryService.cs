using System;

public interface IResourceInventoryService
{
    void AddResource(ResourceModel resource);
    void SaveInventory();
    void LoadInventory();
}
