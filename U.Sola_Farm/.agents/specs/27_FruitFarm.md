## 27 Fruit Farm

**Tarea para artista / integrador — NO requiere codigo por ahora.**

**Do**
Crear un modelo de granja de fruticas que sirva como fuente pasiva de recursos (alimento).

**Requerimentos de assets**
- Modelo 3D en `Assets/Art/3D/Models/FruitFarm.fbx` o `Assets/Level/Prefabs/Resources/FruitFarm.prefab`.
- Estados visuales:
  - Vacio (sin frutas).
  - Creciendo (animacion loop).
  - Lleno (frutas visibles listas para recoger).
- Collider `isTrigger = true`.
- Posiblemente varios `Transform[] _spawnPoints` donde apareceran las frutas (igual que `SolarPanelSpawnerHandler`).

**Requerimentos logicos (cuando se apruebe codigo)**
- Conectar con un futuro `FruitFarmHandler.cs` que herede de `BaseSpawnerResource` (reusar logica de spawn + pool).
- Spawnea `ResourceSheet` con un nuevo `ResourceModel` (ej. `Name = "Fruit"` o reutilizar `Food`).

**Criterio Aceptacion (assets)**
- Prefab visible en escena.
- Estados visuales diferenciados (vacio / creciendo / lleno).

## Human Acceptance

**Estado: Pendiente de assets**

- [ ] `FruitFarm.fbx` / `FruitFarm.prefab`.
- [ ] Estados visuales.
- [ ] (futuro) `FruitFarmHandler.cs`.
