## 12 Resource Polish

**Do**
haz un pulido que te indicare más adelante

**Requirements**
- Ve a GameBindingInstaler y observa el ResourceSpawner como instala las dependencias con una clase factory
- Quiero que el prefab sea un path string y ya no una referencia a un prefab, y que se ontenga de Resources en el subdirectorio "Protoype/ + path"
- quiero que apliques lo Mismo Para ResourceHandler.cs: Factory, Path is e El Sheet existe obten el Path de si mismo e instancia, recuerda que para crear los ResouceHandler utilizamos Un Object Pool ResourcePoolService si no recuerdo mal entonces ese sera el script que sea inyectado

## Human Acceptance

**Estado: Implementado (con detalles a revisar)**

Archivos relacionados:
- `Assets/Code/Scripts/Services/ObjectPooling/ResourcePoolService.cs`:
  - `private ResourceSheet _prototype;` y `_pool = new ObjectPool<ResourceHandler>(...)`.
  - `CreateItem()` hace `var path = System.IO.Path.Combine("Prototypes", _prototype.Path);` y `Resources.Load<ResourceHandler>(path);` - Carga desde `Resources/Prototypes/{path}`.
  - El `GameBindingInstaller.cs` ya no recibe una referencia directa al prefab, sino el `ResourcePoolService` configurado, y los spawners usan `[Inject(Id = ...)] IResourcePoolService`.
- `Assets/Code/Scripts/Features/Resources/ResourceSheet.cs`:
  - `[SerializeField] private string path;` y `[SerializeField] private string pool;` - Strings configurables.
  - `Path` y `Pool` getters publicos.
- `Assets/Code/GameBindingInstaller.cs`:
  - Inyecta `poolSolarEnergy` y `poolWater` con `BindInstance<IResourcePoolService>(...).WithId("SolarEnergy"/"Water")`.
  - Registra Factories de los spawners: `Container.BindFactory<...>().WithId(...).FromComponentInNewPrefab(...)`.
- `Assets/Code/Scripts/Services/ObjectPooling/ResourcesPoolManager/ResourcesPoolManager.cs` - Manager que recibe los pools por `[Inject(Id = "Water")]` / `[Inject(Id = "SolarEnergy")]` y los expone para que `PlayerHandler` y otros saquen recursos por nombre.
- `Assets/Code/Scripts/Features/Spawner/SolarPanelSpawnerHandler.cs` y `WaterSpawnerHandler.cs` - Contienen clases `Factory : PlaceholderFactory<...>`.
- `Assets/Resources/Prototypes/SolarBattery.prefab` y `Water.prefab` - Prefabs cargados por path.
- `Assets/Code/SO/Resources/SolarEnergy.asset`, `Water.asset` - Cada uno con su `Path` y `Pool` para ser identificados en el lookup.

**Verificaciones pendientes del Humano:**
- **Typo en la spec:** la spec dice subdirectorio "Protoype" (sin la 't'). La implementacion usa `"Prototypes"` (carpeta real `Assets/Resources/Prototypes/`). Confirmar si el path debe ser corregido o si esto es lo deseado.
- Confirmar que en cada `ResourceSheet.asset` (`SolarEnergy`, `Water`, `Food`) el campo `Path` coincide con el nombre del prefab (sin extension) en `Assets/Resources/Prototypes/`. Por ejemplo, `Water.asset` deberia tener `Path = "Water"`.
- El campo `Pool` debe coincidir con el Id usado en el `GameBindingInstaller` (`"SolarEnergy"`, `"Water"`).