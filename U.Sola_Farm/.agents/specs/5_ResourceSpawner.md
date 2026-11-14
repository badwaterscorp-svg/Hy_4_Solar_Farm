## 5 Resource Spawner

**Do**
- haz ufeature que sea un Spawner
**Requeriment 1**
- crea un BaseSpawnerSourceHandler:Monobehaviour, debe tener un temporizador de tiempo entre spawns
- crea ResourceSpawner for instanciate our resources, that inherits from the BaseSpawnerSourceHandler
- usa el ResoucerPoolService Para crear u obtener los ResourcesHandler

## Human Acceptance

**Estado: Implementado**

Archivos relacionados:
- `Assets/Code/Scripts/Features/Spawner/BaseSpawnerResource.cs` - `MonoBehaviour` abstracto con `timeBetweenSpawns`, `_timer`, `StartSpawning()`, `StopSpawning()`, evento `OnSpawn`/`OnDespawn` y metodo `Spawn()` abstracto. (Nombre real `BaseSpawnerResource`, no `BaseSpawnerSourceHandler`.)
- `Assets/Code/Scripts/Features/Spawner/ResourceSpawner.cs` - Implementacion base que hereda de `BaseSpawnerResource`, organiza spawneo por filas/columnas y obtiene `ResourceHandler` desde `_poolService.Get()`.
- `Assets/Code/Scripts/Features/Spawner/SolarPanelSpawnerHandler.cs` y `WaterSpawnerHandler.cs` - Spawners concretos que usan `[Inject(Id = "SolarEnergy")]` / `[Inject(Id = "Water")]` para obtener `IResourcePoolService`. Exponen clase `Factory` para Zenject.
- `Assets/Code/Scripts/Services/ObjectPooling/ResourcePoolService.cs` - Implementa `IResourcePoolService` usando `UnityEngine.Pool.ObjectPool<ResourceHandler>`.
- `Assets/Code/Scripts/Services/ObjectPooling/IResourcePoolService.cs` - Contrato con `Get()`, `Release()`, `CountActive`, `CountInactive`.
- `Assets/Code/Scripts/Services/ObjectPooling/ResourcesPoolManager/ResourcesPoolManager.cs` - Registro/lookup central de pools por nombre de recurso.
- `Assets/Code/GameBindingInstaller.cs` - Inyecta `poolSolarEnergy` y `poolWater` y sus `Factory` por Id.
- `Assets/Code/Scripts/Features/Spawner/ResourceSpawnerCountView.cs` - Vista TMP que muestra `SpawnedCount/GetMaxCollection()`.

Assets:
- `Assets/Level/Prefabs/Prefabs_Buildings/Solar_Panel [ResouceSpawner].prefab` y `Water_Well [ResouceSpawner].prefab` - Prefabs de spawners colocados en el level.
- `Assets/Resources/Prototypes/SolarBattery.prefab`, `Water.prefab` - Prototipos cargados por `ResourcePoolService` desde `Resources/Prototypes/...`.
- `Assets/Code/SO/Resources/SolarEnergy.asset`, `Water.asset` - SOs de recursos.

**Verificaciones pendientes del Humano:**
- Renombrar `BaseSpawnerResource` a `BaseSpawnerSourceHandler` o confirmar el nuevo nombre.
- Confirmar en el prefab del spawner que los `Transform[] _spawnPoints` y `_spawnArea` estan asignados.
- Verificar que `ResourcePoolService` este referenciado en el `GameBindingInstaller` con los SOs prototipo correctos.