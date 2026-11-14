## 3 Resource System

**Do**
- utiliza el skill ufeature para crear la lógica de recolección

**Requeriment 1**
- debes utilizar CollsionDetection.cs, y afiliate al on collision enter
- cuando ocurre una collision agregar al siguiente servicio (guiate de uservice) ResourceInventoryService
- crea un scriptable object ResourceSheet.cs dentro usa un modelo llamado ResourceModel.cs
- dentro del proyecto ya hay una interface que se llamada ICopy recuerda el skill ufeature,
quiero que agreges a una lista de ResourceModel dentro de ResourceInventoryService, una copia de ResourceModel ontenida por el metodo copy del ICopy
**Criterio Aceptacion**
- se debe tener 

## Human Acceptance

**Estado: Implementado (con desviaciones)**

Archivos relacionados:
- `Assets/Code/Scripts/Features/Resources/ResourceModel.cs` - Modelo con `Name`, `Amount`, evento `OnAmountChanged`, `Copy()` y metodos `AddAmount`/`RemoveAmount`. Implementa `ICopy<ResourceModel>`.
- `Assets/Code/Scripts/Features/Resources/ResourceSheet.cs` - ScriptableObject con `path`, `pool`, `Sprite spt`, `ResourceModel model` y `GetModelCopy()`.
- `Assets/Code/Scripts/Features/Resources/ResourceWrapper.cs` - Wrapper serializable con `List<ResourceModel> resources` para JSON.
- `Assets/Code/Scripts/Features/BackPack/BackPack.cs` - **Renombrado de `ResourceInventoryService` a `BackPack`**, implementa `IInventoryService` y guarda/agrega copias de `ResourceModel` usando `resource.Copy()`.
- `Assets/Code/Scripts/Features/Resources/ResourceHandler.cs` - Recurso spawneable, expone `Sheet` y notifica via `OnDeactive`.
- `Assets/Code/Scripts/Services/Interfaces/IInventoryService.cs` - Contrato del servicio de inventario.
- `Assets/Code/Scripts/Features/Collector/ResourceCollector.cs` - Recolector que se suscribe a `TriggerDetector.OnTriggerEntered` (no `CollisionDetector.OnCollisionEnter` como pide la spec). Llama a `_inventoryService.AddResource(resource)`.
- `Assets/ExternalAssets/B_Extension/Base/ICopy.cs` - Interfaz base utilizada por `ResourceModel` y `PlayerModel`.
- `Assets/ExternalAssets/B_Extension/Simple/LayerColliderDetection/CollisionDetector.cs` y `TriggerDetector.cs` - Detectores disponibles (se usa `TriggerDetector`, no `CollisionDetector`).

Assets / ScriptableObjects:
- `Assets/Code/SO/Resources/Food.asset`, `SolarEnergy.asset`, `Water.asset` - SOs de recursos.

**Verificaciones pendientes del Humano:**
- La spec pide `CollisionDetector.cs` con `OnCollisionEnter`, pero la implementacion actual usa `TriggerDetector` con `OnTriggerEntered` en `ResourceCollector.cs`. Confirmar si esto es aceptable (el flujo funciona, pero no es lo que la spec describia literalmente).
- Renombrar a `ResourceInventoryService` o confirmar que el nombre `BackPack` es el definitivo.
- Verificar que `ResourceCollector.Initialize(IInventoryService)` se llame desde un `Installer` o un `MonoBehaviour` (actualmente parece no inyectarse: revisar si se asigna `IInventoryService` en el Inspector o se inyecta via Zenject en runtime).