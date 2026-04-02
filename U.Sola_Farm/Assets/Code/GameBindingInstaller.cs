using UnityEngine;
using Zenject;

public class GameBindingInstaller : MonoInstaller
{
    [SerializeField] DragInputService inputService;
    public override void InstallBindings()
    {
        Container.BindInstance<IInputService>(inputService).AsSingle().NonLazy();
        Container.BindInstance<IResourceInventoryService>(new ResourceInventoryService()).AsSingle().NonLazy();
        Container.BindInstance<IResourcePoolService>(ResourcePoolService.Instance).AsSingle().NonLazy();
    }
}
