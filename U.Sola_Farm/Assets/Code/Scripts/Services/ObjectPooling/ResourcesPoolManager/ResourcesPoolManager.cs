using System.Collections.Generic;
using B_Extensions;
using UnityEngine;
using Zenject;

public class ResourcesPoolManager : Singleton<ResourcesPoolManager>
{
    private Dictionary<string, IResourcePoolService> _pools = new Dictionary<string, IResourcePoolService>();

    [Inject(Id = "Water")] IResourcePoolService waterPool;
    [Inject(Id = "SolarEnergy")] IResourcePoolService solarEnergy;

    private new void Awake()
    {
        base.Awake();
        RegisterPool("Water", waterPool);
        RegisterPool("SolarEnergy", solarEnergy);
    }
    public void RegisterPool(string resourceType, IResourcePoolService poolService)
    {
        if (_pools.ContainsKey(resourceType))
        {
            Debug.LogWarning($"[ResourcesPoolManager] Pool for '{resourceType}' already registered.");
            return;
        }
        _pools[resourceType] = poolService;
    }

    public ResourceHandler GetResources(ResourceModel model)
    {
        if (_pools.TryGetValue(model.Name, out IResourcePoolService pool))
            return pool.Get();

        Debug.LogError($"[ResourcesPoolManager] No pool registered for '{model.Name}'");
        return null;
    }

    public void ReleaseResource(ResourceHandler resource)
    {
        if (_pools.TryGetValue(resource.Sheet.Pool, out IResourcePoolService pool))
            pool.Release(resource);
        else
            Debug.LogError($"[ResourcesPoolManager] No pool registered for '{resource.Sheet.Path}'");
    }
}