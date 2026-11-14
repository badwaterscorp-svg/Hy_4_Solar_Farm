## 14 Data building Site

**Do**
Quiero que guardes los datos de BuildingPlaceHandler.cs

**Requirements**
- crea BuildingSiteDataService.js guiate de la skill uservice
- BuildingPlaceHandler tiene un storageRequeriments que es una copia de _buildingRequirments
- primero detecta que la llave "BuildingSite"+IDSite en playerprefs .HasKey al principio y si existe agrega 
el Wrapper obtenido del PlayerPrefs
- en el metodo ConsumeResource crea un metodo que Actualice el Playerprefs.SetString("BuildingSite"+IDSite)
y guarda un WrapperModel que ya existe con los storageRequirements

## Human Acceptance

**Estado: Implementado (con nota sobre extension)**

Archivos relacionados:
- `Assets/Code/Scripts/Services/BuildingSiteDataService.cs` (no `.js` como decia la spec - Unity usa C#):
  - `Save(string idSite, ResourceWrapper wrapper)` - `JsonUtility.ToJson(wrapper)` + `PlayerPrefs.SetString("BuildingSite" + idSite, json)`.
  - `TryLoad(string idSite, out ResourceWrapper wrapper)` - `PlayerPrefs.HasKey("BuildingSite" + idSite)` + `FromJson<ResourceWrapper>`.
- `Assets/Code/Scripts/Services/IBuildingDataService.cs` - Interfaz `Save` / `TryLoad`.
- `Assets/Code/Scripts/Features/Building/BuildingPlaceHandler.cs`:
  - `[HideInInspector] public List<ResourceModel> storageRequirements = new List<ResourceModel>();` - Copia de `_buildingRequirements` (rellenada en `Awake`).
  - En `Awake()`: Si `_dataService.TryLoad(IDSite, out ResourceWrapper wrapper)` recupera `storageRequirements` desde el JSON; si no, inicializa con copia de `_buildingRequirements`.
  - En `ConsumeResource()` se llama a `SaveStorageRequirements()` que hace `_dataService.Save(IDSite, new ResourceWrapper { resources = storageRequirements })`.
- `Assets/Code/GameBindingInstaller.cs` - `Container.BindInstance<IBuildingDataService>(new BuildingSiteDataService()).AsSingle().NonLazy();`.

**Verificaciones pendientes del Humano:**
- Confirmar que `BuildingPlaceHandler.IDSite` es un identificador unico por plaza de construccion en la escena (de lo contrario los datos se pisaran entre plazas).
- Mover la constante de la key `"BuildingSite"` a `KeyStorage.cs` para centralizar las llaves de `PlayerPrefs`.
- Probar en Play Mode: construir parcialmente, cerrar el juego, reabrir y verificar que el progreso se restaura.