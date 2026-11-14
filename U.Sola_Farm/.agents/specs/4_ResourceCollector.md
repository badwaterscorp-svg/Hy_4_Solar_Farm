## 4 Resource Collector

**Do**
- revisa el feature "Resources" ,recuerda ufolder, para encontrar la carpeta, quiero almacenar los Resources en player prefs
**Requeriment 1**
- debes utilizar CollsionDetection.cs, y afiliate al on collision enter
- cuando ocurre una collision agregar al siguiente servicio (guiate de uservice) ResourceInventoryService
- quiero que agreges a una lista de ResourceModel dentro de ResourceInventoryService, una copia de ResourceModel ontenida por el metodo copy del ICopy
- usa un metodo para guardar el inventario en un json Revisa ResourceWrapper.cs , la llave de playerPrefs sera la debes crear en KeyStorage.cs, ya esta creada
buscala en ExternalAssets/B_Extensions/(debes buscar dentro de esta carpeta que no se donde esta)

## Human Acceptance

**Estado: Implementado**

Archivos relacionados:
- `Assets/Code/Scripts/Features/Resources/ResourceWrapper.cs` - `List<ResourceModel> resources` listo para serializar con `JsonUtility`.
- `Assets/Code/Scripts/Features/BackPack/BackPack.cs`:
  - `SaveInventory()` - Serializa la lista a JSON y lo guarda con `PlayerPrefs.SetString(KeyStorage.BackPackItems, json); PlayerPrefs.Save();`
  - `LoadInventory()` - Recupera el JSON y reconstruye la lista.
  - `AddResource(ResourceModel)` - Agrega una copia con `resource.Copy()` y llama a `SaveInventory()`.
  - `RemoveResource(ResourceModel)` - Resta cantidad o elimina si llega a 0 y guarda.
- `Assets/Code/Scripts/Services/GameManager.cs` - Singleton que invoca `_rService.LoadInventory()` en `Awake` (carga el inventario al iniciar el juego).
- `Assets/ExternalAssets/B_Extension/Base/KeyStorage.cs` - Contiene la constante `BackPackItems = "BACKPACK_ITEMS"` (tambien `ResourceInventory = "KEY_RESOURCE_INVENTORY"`).
- `Assets/Code/Scripts/Features/Collector/ResourceCollector.cs` - Se suscribe a `TriggerDetector.OnTriggerEntered` y agrega recursos al inventario.

**Verificaciones pendientes del Humano:**
- Confirmar que `GameManager` este en la escena (o instanciado via `GameManager.prefab` en `Assets/Level/Prefabs/`) para que el inventario cargue al iniciar.
- Probar en Play Mode: recoger recursos, cerrar y reabrir el juego, y verificar que el inventario persiste.
- Considerar usar `KeyStorage.ResourceInventory` en lugar de `KeyStorage.BackPackItems` (la spec no es explicita; revisar consistencia).
- La spec menciona `CollisionDetector.OnCollisionEnter` pero la implementacion usa `TriggerDetector.OnTriggerEntered`. Confirmar si es valido.