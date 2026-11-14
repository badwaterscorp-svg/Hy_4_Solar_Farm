## 20 Store Screen (Sell Resources for Money)

**Do**
Quiero una pantalla para vender los recursos del BackPack a cambio de `Money`.

**Requeriment 1**
- Crea `StoreScreenManager.cs` en `Assets/Code/Scripts/Features/Money/View/` (hereda de `Singleton` o `MonoBehaviour` con `Zenject` segun convenga).
- `[SerializeField] private Transform _cardContainer;` y `ResourceCollectionCard _cardPrefab;` (reutiliza la card existente).
- En `RefreshCards()` itera los `ResourceModel` del BackPack (vía `PlayerHandler.Instance.AccessBackPackHandler().GetBackPackData()`) y por cada uno crea una card que muestre sprite + cantidad disponible.
- Suscribirse a `IInventoryService.OnModelChanged` para refrescar al cambiar.

**Requeriment 2**
- Crea `ButtonSellResource.cs` (`uui` pattern, extiende `BaseButtonAttendant`) en `Features/Money/UI/`.
- `[SerializeField] private ResourceSheet _resourceSheet;` y `[SerializeField] private int _sellPrice;` (precio en Money por unidad).
- En `Start()` suscribe `buttonComponent.AddListener(Sell);`.
- `Sell()`: por cada unidad del recurso en el BackPack (ej. la cantidad actual), llama `MoneyService.Instance.Add(_sellPrice * count)` y luego `BackPack.RemoveResource` hasta vaciar o vender una unidad configurable.

**Requeriment 3**
- Crea un `ButtonSellAll.cs` (uui) en `Features/Money/UI/` que venda **todos** los recursos de un tipo (o todos los del BackPack) de una sola vez, sumando el `Money` total.

**Criterio Aceptacion**
- La store se refresca automaticamente al cambiar el BackPack.
- Vender descuenta del BackPack y suma al `MoneyService`.
- La cantidad de Money se refleja en `MoneyView`.

## Human Acceptance

**Estado: Pendiente de implementacion**

- [ ] `StoreScreenManager.cs`.
- [ ] `ButtonSellResource.cs`.
- [ ] `ButtonSellAll.cs`.
- [ ] Prefab del screen (`Assets/Level/Prefabs/Prefabs_UI/StoreScreen.prefab`).
- [ ] Asignar `_sellPrice` en cada `ButtonSellResource` (datos de diseno).
