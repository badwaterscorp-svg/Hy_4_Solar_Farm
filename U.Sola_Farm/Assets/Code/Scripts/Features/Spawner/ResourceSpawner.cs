using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceSpawner : BaseSpawnerResource
{
    [SerializeField] private Transform _spawnArea;
    [Header("---Positions Settings--")]
    [SerializeField] private Vector2 _spawnBounds = new Vector2(5f, 5f);
    [SerializeField] private int _columnsPerRow = 3;

    private List<ResourceHandler> _spawnedResources = new List<ResourceHandler>();

    protected void OnEnable() => StartSpawning();

    protected void OnDisable()
    {
        StopSpawning();
        _spawnedResources.ForEach(r => {
            r.OnDeactive -= ResourceCollected;
        });
    }

    protected Vector3 GetLineSpawnPosition()
    {
        int indexBatery = SpawnedCount;
        int column = indexBatery % _columnsPerRow;
        int row = indexBatery / _columnsPerRow;

        return _spawnArea.position + ( 
            (indexBatery ==0)?
            new Vector3(_spawnBounds.x*0, 0, _spawnBounds.y*0) :
            new Vector3(column * _spawnBounds.x, 0, row * _spawnBounds.y));
    }

    protected override void Spawn()
    {
        if (_spawnedResources.Count>=GetMaxCollection()) 
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

    public override int GetMaxCollection() => 12;
}
