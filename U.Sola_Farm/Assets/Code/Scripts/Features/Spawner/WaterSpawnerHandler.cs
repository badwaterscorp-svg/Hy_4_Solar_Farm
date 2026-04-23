using System;
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
    [Header("--- Decos ---")]
    [SerializeField] private TriggerDetector detector;
    [SerializeField] private GameObject decoEnter;

    public override int SpawnedCount => _spawnedResources.Count;
    protected void OnEnable()
    {
        GameStateContext.GameStateMediator.Subscribe(GameEventType.GameStarted, StartSpawning);
        detector.OnTriggerEntered += ShowDeco;
        detector.OnTriggerExited += HideDeco;
    }

    protected void OnDisable()
    {
        detector.OnTriggerEntered -= ShowDeco;
        detector.OnTriggerExited -= HideDeco;
        GameStateContext.GameStateMediator.Unsubscribe(GameEventType.GameStarted, StartSpawning);
        StopSpawning();
        _spawnedResources.ForEach(r => {
            r.OnDeactive -= ResourceCollected;
        });
    }
    private void ShowDeco(Transform transform) => decoEnter.SetActive(true);
    private void HideDeco(Transform transform) => decoEnter.SetActive(false);

    protected Vector3 GetLineSpawnPosition() => _spawnPoints[SpawnedCount].position;

    protected override void Spawn()
    {
        if (_spawnedResources.Count >= GetMaxCollection())
            return;

        Vector3 posSpawn = GetLineSpawnPosition();
        ResourceHandler handler = _poolService.Get();
        _spawnedResources.Add(handler);
        handler.OnDeactive += ResourceCollected;
        handler.AnimateJump(_spawnArea.position + Vector3.up * 2, posSpawn, 0.6f);
        handler.gameObject.SetActive(true);
        handler.IsThrown = false;
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