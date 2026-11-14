## 13 BackPack screen

**Do**
vamos a crea una lista de los recursos que tengo

**Requirements**
- Existe un ResourceRepository.cs donde encontraras una lista de ScriptableObjects ResourcesSheet
- Crea una ResourceCollectionCard.cs donde exista una Image, un TMP_Text amount, y un metodo para dibujar datos
- Compara Crea Un BackPackScreenManager.cs donde crees y muestres las Cards y las dibujes dependiendo de lo que tengo en mi backPack comparando los nombres de ResourceSheet.Model y el resourceModel almacenados en mi backPack, dibuja el sprite de ResourceSheet en el image de la Card

## Human Acceptance

**Estado: Implementado**

Archivos relacionados:
- `Assets/Code/Scripts/Features/Resources/ResourcesRepository.cs` (no `ResourceRepository.cs` como dice la spec) - Singleton con `ResourceSheet[] sheets` y `GetSheetByName(string)`.
- `Assets/Code/Scripts/Features/BackPack/View/ResourceCollectionCard.cs`:
  - `[SerializeField] private Image _icon;` y `[SerializeField] private TMP_Text _amountText;`.
  - `Draw(ResourceSheet sheet, int amount)` y `Configure(...)` - Asignan `sheet.Spt` al Image y la cantidad al TMP_Text.
  - `Draw(ResourceModel model)` - Actualiza solo la cantidad si el nombre coincide.
- `Assets/Code/Scripts/Features/BackPack/View/BackPackScreenManager.cs`:
  - `[SerializeField] private Transform _cardContainer;` y `ResourceCollectionCard _cardPrefab;`.
  - `[Inject] IInventoryService` - Inyecta el inventario.
  - `RefreshCards()` - Limpia, itera los `ResourceModel` del BackPack, busca el `ResourceSheet` por nombre con `ResourcesRepository.Instance.GetSheetByName(...)`, instancia una Card y llama a `card.Draw(sheet, resource.Amount)`.
  - Se suscribe a `_inventoryService.OnModelChanged` para refrescar.
- `Assets/Code/SO/Resources/Food.asset`, `SolarEnergy.asset`, `Water.asset` - `ResourceSheet` assets con sprite y modelo para mostrar.
- `Assets/Level/Prefabs/Prefabs_UI/Card_Resource.prefab` - Prefab de la Card (debe contener `ResourceCollectionCard`, una Image y un TMP_Text).
- `Assets/Level/Prefabs/Prefabs_UI/CanvasDialog.prefab` - Otros UIs de HUD.

**Verificaciones pendientes del Humano:**
- La spec menciona `ResourceRepository.cs`; el nombre real es `ResourcesRepository.cs` (con 's'). Confirmar nomenclatura.
- Asegurar que el prefab `Card_Resource.prefab` tiene los componentes `Image` y `TMP_Text` enlazados a los campos `_icon` y `_amountText` de `ResourceCollectionCard`.
- Asignar `_cardContainer` (un transform padre donde instanciar las cards) y `_cardPrefab` en el `BackPackScreenManager` dentro de la UI del juego.