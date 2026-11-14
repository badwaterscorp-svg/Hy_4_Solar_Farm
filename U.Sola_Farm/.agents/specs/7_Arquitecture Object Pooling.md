## 7 Object Pool Arquitecture

**Do**
Quiero hacer un object pooling utilizando el api de unity take the following link https://docs.unity3d.com/6000.5/Documentation/ScriptReference/Pool.ObjectPool_1.html
**Requieremnts**
- usa ufolder debe estar en services/ObjectPooling/[name service]
- Crea un pooling para los ResourceHandlers

## Human Acceptance

**Estado: Implementado**

Archivos relacionados (carpeta correcta segun la spec):
- `Assets/Code/Scripts/Services/ObjectPooling/IResourcePoolService.cs` - Contrato del pool (`Get`, `Release`, `CountActive`, `CountInactive`).
- `Assets/Code/Scripts/Services/ObjectPooling/ResourcePoolService.cs` - Implementacion con `UnityEngine.Pool.ObjectPool<ResourceHandler>` (API oficial Unity 6), `defaultCapacity = 10`, `maxSize = 100`, `collectionCheck = true`. Carga el prefab prototipo desde `Resources/Prototypes/{Sheet.Path}` en `CreateItem`.
- `Assets/Code/Scripts/Services/ObjectPooling/ResourcesPoolManager/ResourcesPoolManager.cs` - Manager central que registra pools por nombre y los expone a otros sistemas (ej. `PlayerHandler` lanza recursos al construir usando este manager).
- `Assets/Code/Scripts/Features/Resources/ResourceHandler.cs` - Tipo del objeto pool.
- `Assets/Resources/Prototypes/SolarBattery.prefab`, `Water.prefab` - Prototipos cargados por el pool.

**Verificaciones pendientes del Humano:**
- Confirmar que el `path` configurado en cada `ResourceSheet` (SolarEnergy, Water, Food) coincide con el nombre del prefab en `Assets/Resources/Prototypes/`.
- Verificar en Play Mode que `pool.CountActive`/`CountInactive` cambian al recoger/soltar recursos y que no hay errores de doble-release (`collectionCheck = true`).