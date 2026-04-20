using UnityEngine;
using Zenject;

public class GameBindingInstaller : MonoInstaller
{
    [SerializeField] DragInputService inputService;
    [SerializeField] SolarPanelSpawnerHandler solarSpawner;
    [SerializeField] WaterSpawnerHandler waterSpawner;
    [Header("___Pools___")]
    [SerializeField] ResourcePoolService poolSolarEnergy;
    [SerializeField] ResourcePoolService poolWater;
    public override void InstallBindings()
    {
        Container.BindInstance<IInputService>(inputService).AsSingle().NonLazy();
        Container.BindInstance<IInventoryService>(new BackPack()).AsSingle().NonLazy();
        Container.BindInstance<IResourcePoolService>(poolSolarEnergy).WithId("SolarEnergy").AsTransient().NonLazy();
        Container.BindInstance<IResourcePoolService>(poolWater).WithId("Water").AsTransient().NonLazy();
        Container.BindFactory<SolarPanelSpawnerHandler, SolarPanelSpawnerHandler.Factory>().WithId("SolarEnergy").FromComponentInNewPrefab(solarSpawner);
        Container.BindFactory<WaterSpawnerHandler, WaterSpawnerHandler.Factory>().WithId("Water").FromComponentInNewPrefab(waterSpawner);
        Container.BindInstance<IBuildingSiteDataService>(new BuildingSiteDataService()).AsSingle().NonLazy();
    }
}