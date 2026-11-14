## 22 Walking Damages Player (Hazard Trigger)

**Do**
Quiero que al caminar el jugador pierda vida si esta dentro de un trigger de peligro.

**Requeriment 1**
- Crea `HazardZone.cs` en `Assets/Code/Scripts/Features/HP/` (o donde este el sistema de vida).
- `[SerializeField] private TriggerDetector _triggerDetector;`
- `[SerializeField] private float _damagePerSecond = 5f;`
- En `OnEnable` suscribe `_triggerDetector.OnTriggerStayed += OnStayed;` y en `OnDisable` desuscribe.
- `OnStayed(Transform other)`: si `other.TryGetComponent<HPHandler>(out var hp)`, aplica `hp.TakeDamage(_damagePerSecond * Time.deltaTime)` cada frame que permanezca dentro.
- Logs opcionales para depurar.

**Criterio Aceptacion**
- Caminar dentro de la zona reduce la HP del jugador de forma continua.
- Al salir del trigger (`OnTriggerExited`) ya no se aplica daño.
- La `HPBarView` refleja el cambio.

## Human Acceptance

**Estado: Pendiente de implementacion**

- [ ] `HazardZone.cs`.
- [ ] Prefab `HazardZone.prefab` con un `TriggerDetector` y un collider `isTrigger = true`.
- [ ] Asignar `_damagePerSecond` desde el editor.
