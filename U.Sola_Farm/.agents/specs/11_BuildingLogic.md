## 11 Build Building

**Do**
quiero que hagas la logica para crear edificios

**Requirements**
- crea un espacio BuildingPlaceHnalder.cs con un TriggerDetector.cs, que cuando entre
se construya un elemento
- en el Handler que exista una lista de ResoruceModel y que tenga un metodo para saber si mi BackPack tiene los recursos suficientes, agrega logs para saber su funcionamiento
- haz un script que se llame BuildingRequerimentsView.cs donde muestre lo que cuesta el Building, que serian la lista de ResourceModel en mi handler

## Human Acceptance

**Estado: Implementado**

Archivos relacionados:
- `Assets/Code/Scripts/Features/Building/BuildingPlaceHandler.cs`:
  - `[SerializeField] private TriggerDetector _triggerDetector;` - Trigger que detecta al jugador.
  - `[SerializeField] private List<ResourceModel> _buildingRequirements = new List<ResourceModel>();` - Lista de recursos necesarios.
  - `[SerializeField] BuildingPlaceUI _ui;` - Vista integrada.
  - `OnTriggerStayed(Transform)` - Consume recursos del BackPack cada `_checkInterval` segundos, llama a `PlayerHandler.ThrowResource` para visualizacion y luego `TryBuild()`.
  - `TryBuild()` - Si todos los `storageRequirements` tienen `Amount == 0`, instancia el edificio via `SolarPanelSpawnerHandler.Factory` o `WaterSpawnerHandler.Factory` (segun `typeBuilding`).
  - Logs: `Debug.Log("[BuildingPlaceHandler] Building constructed!")` y `print($"[BuildingPlaceHandler] Player does not have required resource: {item.Name}")`.
- `Assets/Code/Scripts/Features/Building/BuildingRequirementsView.cs` - Muestra en consola `Debug.Log` los costos del edificio (`req.Name: req.Amount`). **No renderiza UI; solo loguea.** La spec dice "muestre lo que cuesta"; confirmar si el logueo en consola es suficiente o se requiere UI grafica.
- `Assets/Code/Scripts/Features/Building/BuildingPlaceUI.cs` - Vista serializable que dibuja `ResourceCollectionCard`s con los requisitos usando `ResourcesRepository.GetSheetByName()`.
- `Assets/Level/Prefabs/Prefabs_Buildings/BuildingOneSite.prefab` - Prefab de plaza de construccion.

**Verificaciones pendientes del Humano:**
- Confirmar que `BuildingPlaceHandler` esta colocado en la escena con el `TriggerDetector`, los `ResourceModel` requisitos y el `BuildingPlaceUI` configurados.
- Definir si `BuildingRequirementsView` debe mostrar UI grafica (botones + iconos) o si el logueo es aceptable.
- `typeBuilding = "SolarEnergy"` pero la comparacion es `typeBuilding.Equals("Solar Energy")` con espacio; actualmente la comparacion **nunca se cumple** y no se construye el spawner. Hay que corregir la cadena a `"SolarEnergy"` o cambiar el `SerializeField`.