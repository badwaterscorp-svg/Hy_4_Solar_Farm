## 24 Rock (Resource Source)

**Tarea para artista / integrador — NO requiere codigo por ahora.**

**Do**
Crear un modelo de roca que sirva como fuente de recursos (ej. Stone / Mineral).

**Requerimentos de assets**
- Modelo 3D de la roca en `Assets/Art/3D/Models/Rock.fbx` o `Assets/Level/Prefabs/Resources/Rock.prefab`.
- Variantes visuales opcionales:
  - Sano
  - Agrietado (despues de varios hits)
  - Roto / spawneable dropeable
- Collider `isTrigger = true` para `TriggerDetector`.
- Materiales consistentes con `Assets/Art/3D/Materials/`.

**Requerimentos logicos (cuando se apruebe codigo)**
- Conectar con un futuro `RockHandler.cs` que reuse `BaseSpawnerResource` o un nuevo `BaseSourceResource` parametrizable.
- Spawneable dropeable: `ResourceSheet` con un nuevo `ResourceModel` (ej. `Name = "Stone"`).

**Criterio Aceptacion (assets)**
- Prefab visible en escena.
- Collider trigger funcionando.

## Human Acceptance

**Estado: Pendiente de assets**

- [ ] `Rock.fbx` o `Rock.prefab`.
- [ ] Collider trigger configurado.
- [ ] (futuro) `RockHandler.cs` y `Stone.asset`.
