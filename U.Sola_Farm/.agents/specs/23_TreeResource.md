## 23 Tree (Resource Source)

**Tarea para artista / integrador — NO requiere codigo por ahora.**

**Do**
Crear un modelo de arbolito que sirva como fuente de recursos (ej. Food / Wood).

**Requerimentos de assets**
- Modelo 3D del arbol en `Assets/Art/3D/Models/Tree.fbx` (o `.prefab` directo en `Assets/Level/Prefabs/Resources/Tree.prefab`).
- Animaciones o estados visuales opcionales:
  - Idle (sano)
  - Shaking (cuando el jugador lo golpea)
  - Empty / Stump (cuando se agotan los recursos)
- Collider `isTrigger = true` para que `TriggerDetector` lo detecte.
- Tags: `"Tree"` o `Layer` dedicado (revisar `B_Extension/Simple/LayerColliderDetection`).

**Requerimentos logicos (cuando se apruebe codigo)**
- Conectar con un futuro `TreeHandler.cs` que use `BaseSpawnerResource` o un nuevo `BaseSourceResource` (spawnea N `ResourceHandler` configurables, similar a `SolarPanelSpawnerHandler`).
- Spawnear el `ResourceSheet` apropiado (e.g. Food).

**Criterio Aceptacion (assets)**
- Prefab del arbol visible en escena.
- Collider trigger funcionando.
- Materiales con la paleta del proyecto (`Assets/Art/3D/Materials/`).

## Human Acceptance

**Estado: Pendiente de assets**

- [ ] `Tree.fbx` o `Tree.prefab` creado.
- [ ] Collider trigger configurado.
- [ ] (futuro) Conexion con `TreeHandler.cs`.
