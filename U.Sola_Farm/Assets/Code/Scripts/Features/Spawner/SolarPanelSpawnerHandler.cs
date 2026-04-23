using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;
using Zenject;

public class SolarPanelSpawnerHandler:BaseSpawnerResource
{
    [Inject(Id = "SolarEnergy")] protected new IResourcePoolService _poolService;

    [SerializeField] private Transform _spawnArea;
    [Header("---Positions Settings--")]
    [SerializeField] private Transform[] _spawnPoints;
    [Header("---Class Settings--")]
    [SerializeField] ShowPanelDirty dirtyModel;
    public override int SpawnedCount => _spawnedResources.Count;
    private List<ResourceHandler> _spawnedResources = new List<ResourceHandler>();

    [Header("--- Decos ---")]
    [SerializeField] private TriggerDetector detector;
    [SerializeField] private GameObject decoEnter;

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
        dirtyModel.Unsubscribe();
    }
    private IEnumerator Start()
    {
        dirtyModel.Configure(this);
        yield return new WaitForSeconds(30f);
        dirtyModel.DoDirty();
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
        handler.AnimateJump(_spawnArea.position+Vector3.up*2,posSpawn,0.6f);
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

    public class Factory : PlaceholderFactory<SolarPanelSpawnerHandler> { }
}
