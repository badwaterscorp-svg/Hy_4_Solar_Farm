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
    protected void OnEnable() => StartSpawning();

    protected void OnDisable()
    {
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

    protected Vector3 GetLineSpawnPosition() => _spawnPoints[SpawnedCount].position;

    protected override void Spawn()
    {
        if (_spawnedResources.Count >= GetMaxCollection())
            return;

        Vector3 posSpawn = GetLineSpawnPosition();
        ResourceHandler handler = _poolService.Get();

        _spawnedResources.Add(handler);
        handler.OnDeactive += ResourceCollected;
        handler.transform.position = posSpawn;
        handler.transform.rotation = Quaternion.identity;
        handler.gameObject.SetActive(true);
        handler.AnimateInstanciate();
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

[System.Serializable]
public class ShowPanelDirty 
{
    [SerializeField] GameObject iconDirty;
    [SerializeField] TriggerDetector triggerDirty;
    [SerializeField] ResourceCollectionCard card;
    [SerializeField] ResourceSheet sheetResource;
    [SerializeField] int amountToClean = 2;
    [SerializeField] ResourceSpawnerCountView countView;
    private ResourceModel bufferModel;
    SolarPanelSpawnerHandler handler;

    public void Configure(SolarPanelSpawnerHandler _handler)
    {
        this.handler = _handler;
        bufferModel = sheetResource.Model.Copy();
        bufferModel.Amount = amountToClean;
        triggerDirty.gameObject.SetActive(true);
        card.Configure(sheetResource, amountToClean);
        triggerDirty.OnTriggerStayed += Clean;
    }

    public void Unsubscribe() 
    {
        triggerDirty.OnTriggerStayed -= Clean;
    }

    private void Clean(Transform t) 
    {
        if(debtCoroutine == null)
            debtCoroutine = handler.StartCoroutine(DoDebt());
    }

    Coroutine debtCoroutine;
    private IEnumerator DoDebt() 
    {
        yield return new WaitForSeconds(0.5f);
        BackPackHandler backPack = PlayerHandler.Instance.AccessBackPackHandler();
        if (backPack.GetCountResource(bufferModel.Name) > 0)
        {
            backPack.RemoveResource(bufferModel);
            bufferModel.RemoveAmount(1);
            card.Draw(bufferModel);
        }

        if (bufferModel.Amount <= 0)
        {
            Debug.Log("TODO Cleaned. Mostrar Particulas");
            DoClean();
        }

        debtCoroutine = null;
    }

    public void DoDirty()
    {
        handler.StopSpawning();
        iconDirty.SetActive(true);
        card.gameObject.SetActive(true);
        countView?.gameObject.SetActive(false);
        triggerDirty.gameObject.SetActive(true);
    }

    public void DoClean() 
    {
        handler.StartSpawning();
        iconDirty.SetActive(false);
        card.gameObject.SetActive(false);
        countView?.gameObject.SetActive(true);
        triggerDirty.gameObject.SetActive(false);
    }
}