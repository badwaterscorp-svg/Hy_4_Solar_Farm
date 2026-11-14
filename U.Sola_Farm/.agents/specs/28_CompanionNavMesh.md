## 28 Companion / NavMesh Follower

**Do**
Quiero un "ayudante" (NPC o mascota) que siga al jugador via `NavMeshAgent`.

**Requeriment 1**
- Crea `CompanionHandler.cs : MonoBehaviour` en `Assets/Code/Scripts/Features/Companion/`.
- `[RequireComponent(typeof(NavMeshAgent))]`
- `[SerializeField] private Transform _player;` (asignable desde el editor o inyectado por Zenject como `PlayerHandler.Instance.transform`).
- `void Update()`: `_navAgent.SetDestination(_player.position)`.
- Distancia minima de seguimiento: `[SerializeField] private float _stopDistance = 1.5f;` — si la distancia al jugador es menor, `_navAgent.isStopped = true`, si no, `_navAgent.isStopped = false`.

**Requeriment 2**
- Investigar si el `NavMesh` se puede modificar en tiempo de ejecucion:
  - **Unity 6 / AI Navigation package**: si se usa `Unity.AI.Navigation` con `NavMeshSurface`, la superficie se puede **rebuild en runtime** con `NavMeshSurface.BuildNavMesh()`.
  - Documentar el hallazgo y proponer integracion:
    - Opcion A: bake una sola vez al inicio (estatico).
    - Opcion B: rebuild en eventos especificos (al colocar un edificio, al expandir terreno `FertileTerrainHandler`, al destruir un obstaculo).
- Crea `NavMeshRebuilder.cs` (helper) que reciba una lista de `NavMeshSurface` y los rebuilds en `BuildNavMesh()` cuando se llame `Rebuild()`.

**Requeriment 3**
- `CompanionCaller.cs` (uui) si se quiere un boton que ordene al companion ir a un punto (waypoint).

**Criterio Aceptacion**
- El companion persigue al jugador.
- Si el jugador se aleja mas de `_stopDistance`, lo sigue.
- El NavMesh del entorno permite el pathfinding (pre-bake o rebuild dinamico segun hallazgos).

## Human Acceptance

**Estado: Pendiente de investigacion + implementacion**

- [ ] Investigar `Unity.AI.Navigation` y `NavMeshSurface` en el proyecto (verificar si esta en `Packages/manifest.json`).
- [ ] `CompanionHandler.cs`.
- [ ] (opcional) `NavMeshRebuilder.cs` para terrenos dinamicos (`FertileTerrainHandler`).
- [ ] Prefab `Companion.prefab` con `NavMeshAgent` y Animator.
