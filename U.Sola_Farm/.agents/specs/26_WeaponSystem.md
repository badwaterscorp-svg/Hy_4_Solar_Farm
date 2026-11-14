## 26 Weapon System (IWeapon)

**Do**
Quiero un sistema de armas desacoplado, con una interfaz `IWeapon` y al menos una implementacion de pistola.

**Requeriment 1**
- Crea `IWeapon.cs` en `Assets/Code/Scripts/Features/Weapon/`:
  ```csharp
  public interface IWeapon
  {
      string Id { get; }
      float Damage { get; }
      float Range { get; }
      float Cooldown { get; }
      bool CanFire();
      void Fire(HPHandler target);
  }
  ```

**Requeriment 2**
- Crea `WeaponModel.cs : ICopy<WeaponModel>` con `string Id`, `float Damage`, `float Range`, `float Cooldown`, `int MaxAmmo`, `float ReloadTime`.

**Requeriment 3**
- Crea `PistolHandler.cs : MonoBehaviour, IWeapon` (en `Features/Weapon/`) con:
  - `[SerializeField] private WeaponModel _model;`
  - `[SerializeField] private Transform _muzzle;` (para VFX / spawn del proyectil).
  - `event Action OnFire;` (para UI / SFX).
  - Metodo `Fire(HPHandler target)`: `target.TakeDamage(_model.Damage)` y dispara el evento.
  - Cooldown interno: `Time.time` + `_model.Cooldown` valida en `CanFire()`.
  - Disparo por click/tap del input (suscripcion a `IInputService` o evento custom).

**Requeriment 4**
- Crea `WeaponCaller.cs` (uui pattern) si se quiere un boton de UI que dispare (`Call()` invoca `Fire` del arma equipada).

**Criterio Aceptacion**
- `IWeapon` permite sustituir el arma facilmente.
- `PistolHandler` hace daño al `HPHandler` del `Enemy` (spec 25).
- Cooldown impide disparo continuo.

## Human Acceptance

**Estado: Pendiente de implementacion**

- [ ] `IWeapon.cs`, `WeaponModel.cs`, `PistolHandler.cs`, `WeaponCaller.cs`.
- [ ] Prefab `Pistol.prefab` con `WeaponModel` configurado.
- [ ] Animacion / VFX de disparo (opcional).
