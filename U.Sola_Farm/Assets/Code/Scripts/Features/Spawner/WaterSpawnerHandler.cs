using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaterSpawnerHandler : BaseSpawnerResource
{
    [Inject(Id = "Water")] protected new IResourcePoolService _poolService;
    [SerializeField] private Transform _spawnArea;
    [Header("---Positions Settings--")]
    [SerializeField] private Transform[] _spawnPoints;
    private List<ResourceHandler> _spawnedResources = new List<ResourceHandler>();
    public override int SpawnedCount => _spawnedResources.Count;
    protected void OnEnable() => StartSpawning();

    protected void OnDisable()
    {
        StopSpawning();
        _spawnedResources.ForEach(r => {
            r.OnDeactive -= ResourceCollected;
        });
    }

    protected Vector3 GetLineSpawnPosition() => _spawnPoints[SpawnedCount].position;

    protected override void Spawn()
    {
        if (_spawnedResources.Count >= GetMaxCollection())
        {
            return;
        }

        Vector3 posSpawn = GetLineSpawnPosition();
        ResourceHandler handler = _poolService.Get();

        _spawnedResources.Add(handler);
        handler.OnDeactive += ResourceCollected;
        handler.transform.position = posSpawn;
        handler.transform.rotation = Quaternion.identity;
        handler.gameObject.SetActive(true);
        handler.AnimateInstanciate();
        OnSpawn?.Invoke();
    }

    protected void ResourceCollected(ResourceHandler handler)
    {
        handler.OnDeactive -= ResourceCollected;
        _spawnedResources.Remove(handler);
        OnDespawn?.Invoke();
    }

    public override int GetMaxCollection() => _spawnPoints.Length;

    public class Factory : PlaceholderFactory<WaterSpawnerHandler> { }
}