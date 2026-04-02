using System.Collections;
using UnityEngine;
using Zenject;

public class ResourceSpawner : BaseSpawnerSourceHandler
{
    [SerializeField] private Transform _spawnArea;
    [SerializeField] private Vector2 _spawnBounds = new Vector2(5f, 5f);

    private IResourcePoolService _poolService;

    [Inject]
    public void Initialize(IResourcePoolService poolService)
    {
        _poolService = poolService;
    }
    private void OnEnable() => StartSpawning();

    private void OnDisable() => StopSpawning();

    protected override void Spawn()
    {
        ResourceHandler handler = _poolService.Get();
        
        Vector3 randomPos = GetRandomSpawnPosition();
        handler.transform.position = randomPos;
        handler.transform.rotation = Quaternion.identity;
        
        handler.gameObject.SetActive(true);
        handler.AnimateInstanciate();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-_spawnBounds.x, _spawnBounds.x);
        float z = Random.Range(-_spawnBounds.y, _spawnBounds.y);
        return _spawnArea.position + new Vector3(x, 0, z);
    }
}
