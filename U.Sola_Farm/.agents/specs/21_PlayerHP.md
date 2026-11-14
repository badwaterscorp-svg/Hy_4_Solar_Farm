## 21 Player HP System

**Contexto**
El usuario menciona que "ya tiene un script de la vida" y pide solicitarlo antes de crear el codigo.

**Accion requerida**
- **ANTES de implementar**, localizar y compartir el script de vida existente. Posibles ubicaciones:
  - `Assets/Code/Scripts/Features/HP/` (carpeta mencionada en la exploracion previa pero parecia vacia).
  - `Assets/Code/Scripts/Features/Player/` (puede estar embebido en `PlayerHandler`).
  - `Assets/ExternalAssets/` (alguna extension B_).
- Si no existe, levantar como sub-ticket la creacion de un `HPHandler.cs` y `HPModel.cs` con la siguiente propuesta:

### Propuesta de HP (si no existe script)
- `HPModel : ICopy<HPModel>` con `float Current`, `float Max`.
- `HPHandler : MonoBehaviour` con:
  - `event Action<float, float> OnHPChanged;` (current, max).
  - `event Action OnDeath;`.
  - `void TakeDamage(float amount)`.
  - `void Heal(float amount)`.
  - Persistencia: `KeyStorage.PlayerHP_I = "PLAYER_HP"` (current) y `KeyStorage.PlayerMaxHP_I = "PLAYER_MAX_HP"` (max).
- UI: `HPBarView.cs` (Image fill o TMP_Text) que se suscribe a `OnHPChanged`.

**Criterio Aceptacion**
- Confirmar primero con el humano la existencia del script antes de codificar.

## Human Acceptance

**Estado: Bloqueado — esperando confirmacion**

- [ ] Localizar y compartir el script de vida existente.
- [ ] Si no existe, aprobar la propuesta de `HPModel`/`HPHandler`.
- [ ] Crear `HPBarView.cs` y prefab.
