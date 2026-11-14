## 25 Enemy (Attacks Player)

**Do**
Quiero un enemigo que persiga/ataque al jugador. Debe tener el script de vida (spec 21) y una animacion de ataque.

**Requeriment 1**
- Crea `EnemyModel.cs : ICopy<EnemyModel>` con `float MoveSpeed`, `float AttackDamage`, `float AttackRange`, `float AttackCooldown`, `int XPValue`.
- Crea `EnemyHandler.cs : MonoBehaviour` (en `Features/Enemy/`) con:
  - `[RequireComponent(typeof(HPHandler))]`
  - `[SerializeField] private EnemyModel _model;`
  - `event Action OnAttack` (para UI / SFX).
  - Metodos: `void Attack(HPHandler target)`, `void TickCooldown(float dt)`.
  - Soporte para `IWeapon` (spec 26) o ataque melee simple.

**Requeriment 2**
- Crea `EnemyMovement.cs` (clase pura) que reciba `Vector3 target` y `float speed` y mueva el `Transform` (o `Rigidbody`) hacia el jugador.
- Conectar con `NavMeshAgent` (spec 28) si el `Enemy` es el `Agente` que sigue al jugador.

**Requeriment 3**
- Crea `EnemyAttackTrigger.cs` (`MonoBehaviour` con `TriggerDetector`):
  - Al entrar el jugador: inicia corutina que cada `_model.AttackCooldown` segundos llama `EnemyHandler.Attack(player.HPHandler)`.
  - Animacion: dispara `Animator.SetTrigger("Attack")` antes del daño.
  - Si el `EnemyHandler` muere (`OnDeath` del `HPHandler`), detiene la corutina y reproduce `Animator.SetTrigger("Die")`.

**Criterio Aceptacion**
- Al acercarse, el enemigo reproduce una animacion de ataque y resta HP al jugador.
- Al morir, el enemigo deja de atacar.
- Sin scripts de vida previos, este spec queda bloqueado (ver spec 21).

## Human Acceptance

**Estado: Bloqueado por spec 21 (HP)**

- [ ] `EnemyModel.cs`, `EnemyHandler.cs`, `EnemyMovement.cs`, `EnemyAttackTrigger.cs`.
- [ ] Prefab `Enemy.prefab` con `Animator`, `HPHandler`, `NavMeshAgent`.
- [ ] Animaciones `Attack` y `Die` configuradas.
