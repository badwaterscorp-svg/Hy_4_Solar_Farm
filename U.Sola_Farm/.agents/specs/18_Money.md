## 18 Money / Currency System

**Do**
Quiero hacer la logica para una moneda del juego llamada `Money`, que se gana vendiendo recursos en una `ScreenStore` (spec futura) y al completar misiones, y se gasta en mejoras como `BackPackImprove` (spec 10) u otras futuras.

**Requeriment 1**
- Crea una interfaz `IMoneyService` con un evento `OnBalanceChanged` con la firma `(int currentBalance, int previousBalance)`, y los metodos `int GetBalance()`, `void Add(int amount)`, `bool Remove(int amount)`.
- Crea `MoneyService : Singleton<MoneyService>, IMoneyService`. Persiste un unico `int _balance` en `PlayerPrefs` bajo la nueva llave `KeyStorage.MoneyBalance_I = "MONEY_BALANCE"`. Carga el valor en `Awake`. `Remove` retorna `false` y no modifica el balance si `amount > _balance` (opcion a). Tras cualquier cambio persiste con `PlayerPrefs.SetInt` + `PlayerPrefs.Save` y dispara `OnBalanceChanged(currentBalance, previousBalance)`.
- Agrega la constante `MoneyBalance_I` a `Assets/ExternalAssets/B_Extension/Base/KeyStorage.cs`.

**Requeriment 2**
- Crea un `ResourceSheet` para Money en `Assets/Code/SO/Resources/Money.asset` con `Name = "Money"`, `Pool = "Money"`, `Path = "Money"`, `Sprite` asignado por el artista, y un `ResourceModel` interno con `Name = "Money"`. Esto permite dibujarlo con la misma `ResourceCollectionCard` que el resto de recursos.

**Requeriment 3**
- Crea `Features/Money/MoneyCaller.cs` siguiendo la convencion ucaller: `[SerializeField] int _amount`, `enum MoneyOperation { Add, Remove }`, y un metodo `Call()` que envie la operacion a `MoneyService.Instance`.
- Crea `Features/Money/View/MoneyView.cs` siguiendo el patron Displayer: `[SerializeField] ResourceCollectionCard _card`, `[SerializeField] ResourceSheet _moneySheet`, se suscribe a `MoneyService.Instance.OnBalanceChanged` en `OnEnable`, desuscribe en `OnDisable`, y en `Redraw()` llama `_card.Configure(_moneySheet, MoneyService.Instance.GetBalance())`.

**Requeriment 4**
- En `Assets/Code/GameBindingInstaller.cs` agrega `Container.BindInstance<IMoneyService>(new MoneyService()).AsSingle().NonLazy();` para que pueda inyectarse via Zenject.

**Criterio Aceptacion**
- Llamar `MoneyService.Instance.Add(100)` desde cualquier `MonoBehaviour` actualiza la card al instante.
- Cerrar y reabrir el juego restaura el ultimo balance.
- `Remove` nunca deja el balance en negativo: retorna `false` si no alcanza.
- El balance se persiste en `PlayerPrefs` con la llave `MONEY_BALANCE` y puede leerse/sobreescribirse manualmente para pruebas.
- La `ResourceCollectionCard` que muestra el balance usa el `ResourceSheet` `Money.asset` (mismo flujo visual que los demas recursos del BackPack).

## Human Acceptance

**Estado: Pendiente de implementacion**

- [ ] `IMoneyService.cs` creado.
- [ ] `MoneyService.cs` creado y enlazado en `GameBindingInstaller`.
- [ ] `KeyStorage.MoneyBalance_I` agregado.
- [ ] `Money.asset` creado (sprite a asignar por el artista).
- [ ] `MoneyCaller.cs` creado.
- [ ] `MoneyView.cs` creado y asignado a un prefab de UI.
