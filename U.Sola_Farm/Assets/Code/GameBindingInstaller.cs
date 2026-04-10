using UnityEngine;
using Zenject;

public class GameBindingInstaller : MonoInstaller
{
    [SerializeField] DragInputService inputService;
    [SerializeField] ResourceSpawner resourceSpawner;
    public override void InstallBindings()
    {
        Container.BindInstance<IInputService>(inputService).AsSingle().NonLazy();
        Container.BindInstance<IInventoryService>(new ResourceInventoryService()).AsSingle().NonLazy();
        Container.BindInstance<IResourcePoolService>(ResourcePoolService.Instance).AsSingle().NonLazy();
        Container.BindFactory<ResourceSpawner, ResourceSpawner.Factory>().FromComponentInNewPrefab(resourceSpawner);
    }
}
