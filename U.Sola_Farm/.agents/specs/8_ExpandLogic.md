## 8 Expand Grass
**DO**
- quiero que hagas la logica para expnadir un terreno

**Requirements**
- usa doTween para expandir FertileTerrainHandler.cs
- una animacion para expandir y otra para reducir
- que se pueda asignar un vector2 desde el editor, y que solo se expanda en x & z, y siempre sera 0.5

## Human Acceptance

**Estado: Implementado (parcial)**

Archivos relacionados:
- `Assets/Code/Scripts/Features/Terrain/FertileTerrainHandler.cs`:
  - Usa `DG.Tweening.DOScale` para animar la expansion y la reduccion.
  - `[SerializeField] private Vector2 _expandSize = new Vector2(1f, 1f);` - Vector2 asignable desde el editor.
  - `Expand()` aplica `Ease.Linear`, `Reduce()` aplica `Ease.InBack`. Toggle disponible con `ToggleExpand()`.
  - `[ContextMenu("Expand")]` / `[ContextMenu("Reduce")]` para probar desde el editor.

**Verificaciones pendientes del Humano:**
- **El requerimiento dice "y siempre sera 0.5"** - La implementacion usa `_expandSize.y` como delta en Y, no fuerza 0.5. El delta en Y esta en `targetScale.y = _originalScale.y + _expandSize.y;`. Si la intencion es que Y siempre sea 0.5, hay que hardcodear 0.5f en vez de usar `_expandSize.y`. Confirmar.
- Probar visualmente con `DoTween` instalado (presente en `Assets/Code/SO/` y dependencias de Packages) que las animaciones se reproducen sin stutter.
- Asignar el prefab `Assets/Level/Prefabs/GrassArea.prefab` en la escena con valores deseados de `_expandSize` y `_duration`.