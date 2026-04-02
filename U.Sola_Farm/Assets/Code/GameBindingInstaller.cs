using UnityEngine;
using Zenject;

public class GameBindingInstaller : MonoInstaller
{
    [SerializeField] DragInputService inputService;
    public override void InstallBindings()
    {
        Container.BindInstance<IInputService>(inputService).AsSingle().NonLazy();
        Container.BindInstance<IResourceInventoryService>(ResourceInventoryService.Instance).AsSingle().NonLazy();
    }
}
