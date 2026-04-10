using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceSpawner : BaseSpawnerSourceHandler
{
    [SerializeField] private Transform _spawnArea;
    [Header("---Positions Settings--")]
    [SerializeField] private Vector2 _spawnBounds = new Vector2(5f, 5f);
    [field:SerializeField] public int MaxCollection { get; private set; } = 12;
    [SerializeField] private int _columnsPerRow = 3;
    public int SpawnedCount => _spawnedResources.Count;
    public event System.Action OnSpawn;
    public event System.Action OnDespawn;
    private IResourcePoolService _poolService;
    private List<ResourceHandler> _spawnedResources = new List<ResourceHandler>();

    [Inject]
    public void Initialize(IResourcePoolService poolService)
    {
        _poolService = poolService;
    }
    private void OnEnable() => StartSpawning();

    private void OnDisable()
    {
        StopSpawning();
        _spawnedResources.ForEach(r => {
            r.OnDeactive -= ResourceCollected;
        });
    }

    private Vector3 GetLineSpawnPosition()
    {
        int column = SpawnedCount % _columnsPerRow;
        int row = SpawnedCount / _columnsPerRow;
        return _spawnArea.position +
            new Vector3(column * _spawnBounds.x, 0, row * _spawnBounds.y);
    }

    protected override void Spawn()
    {
        if (_spawnedResources.Count>=MaxCollection) 
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

    private void ResourceCollected(ResourceHandler handler)
    {
        handler.OnDeactive -= ResourceCollected;
        _spawnedResources.Remove(handler);
        OnDespawn?.Invoke();
    }

    public class Factory : PlaceholderFactory<ResourceSpawner>
    {
    }
}
