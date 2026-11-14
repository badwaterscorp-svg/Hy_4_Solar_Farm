## 19 Improve Sheet (Global Improvements)

**Do**
Quiero un `ScriptableObject` global que represente una mejora genérica del juego. Debe ser reutilizable: el `AmountMax` del BackPack, velocidad del jugador, daño de un arma, vida máxima, etc.

**Requeriment 1**
- Crea `ImproveSheet.cs` (ScriptableObject) en `Assets/Code/Scripts/Features/Improve/`.
- Campos serializados:
  - `string Id` (identificador único, ej. `"BackPackCapacity"`, `"PlayerSpeed"`, `"WeaponDamage"`).
  - `string DisplayName` (nombre legible para UI).
  - `Sprite Icon` (icono a mostrar en UI).
  - `ImproveType Type` (enum: `BackPackCapacity`, `PlayerSpeed`, `PlayerMaxHP`, `WeaponDamage`, etc. — extensible).
  - `int AmountPerLevel` (cuanto suma por nivel, ej. 5 para BackPack, 0.5f equivalente para speed).
  - `int MaxLevel` (nivel maximo permitido).
  - `List<ImproveCost> Costs` (costos por nivel — ver Requeriment 2).
- Crea una clase serializable interna `ImproveCost` con campos `ResourceModel Resource` y `int Amount` (costo en recursos). Mas adelante se sumara costo en `Money` (usar `Money.asset` como un `ResourceSheet` mas).

**Requeriment 2**
- Crea `ImproveManager.cs` (Singleton, en `Features/Improve/`).
- Metodos publicos:
  - `int GetLevel(string id)`.
  - `int GetMaxLevel(string id)`.
  - `bool CanImprove(string id)`.
  - `bool TryImprove(string id, IInventoryService backPack, IMoneyService money)` — valida costo, descuenta, sube nivel, dispara evento.
- Persistencia: una sola `PlayerPrefs` key `KeyStorage.ImprovementsLevels_I = "IMPROVEMENTS_LEVELS"` con un JSON `ImproveLevelsWrapper { Dictionary<string, int> levels }` (usar `JsonUtility` con una lista serializable alternativa si `Dictionary` no es serializable).
- Evento: `event Action<string, int> OnImproved` (id, nuevoNivel).

**Criterio Aceptacion**
- Un `ImproveSheet` puede crearse desde el menu de Unity (`Create > SO > Improve Sheet`).
- `ImproveManager` carga niveles al iniciar y los persiste en cada `TryImprove` exitoso.
- `BackPackImproveScreen` debe migrar de su logica hardcodeada a usar `ImproveSheet("BackPackCapacity")`.

## Human Acceptance

**Estado: Pendiente de implementacion**

- [ ] `ImproveSheet.cs` ScriptableObject.
- [ ] `ImproveManager.cs` singleton.
- [ ] `KeyStorage.ImprovementsLevels_I`.
- [ ] Assets de ejemplo (`BackPackCapacity.asset`, `PlayerSpeed.asset`).
- [ ] `BackPackImproveScreen` migrado a `ImproveManager`.
