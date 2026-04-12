using UnityEngine;
using Zenject;

public class GameBindingInstaller : MonoInstaller
{
    [SerializeField] DragInputService inputService;
    [SerializeField] ResourceSpawner resourceSpawner;
    [Header("___Pools___")]
    [SerializeField] ResourcePoolService poolSolarEnergy;
    public override void InstallBindings()
    {
        Container.BindInstance<IInputService>(inputService).AsSingle().NonLazy();
        Container.BindInstance<IInventoryService>(new BackPack()).AsSingle().NonLazy();
        Container.BindInstance<IResourcePoolService>(poolSolarEnergy).AsSingle().NonLazy();
        Container.BindFactory<ResourceSpawner, ResourceSpawner.Factory>().FromComponentInNewPrefab(resourceSpawner);
    }
}