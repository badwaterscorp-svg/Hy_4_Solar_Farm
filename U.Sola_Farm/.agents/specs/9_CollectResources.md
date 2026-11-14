## Collector Resources

**Do**
- crea un ResourceCollector.cs

**Requirements**
- it should have a TriggerDetector.cs list
and subscribe on trigger entered
- when collides, using the method mentioned above

## Human Acceptance

**Estado: Implementado**

Archivos relacionados:
- `Assets/Code/Scripts/Features/Collector/ResourceCollector.cs`:
  - `[SerializeField] private List<TriggerDetector> _triggerDetectors = new List<TriggerDetector>();` - Lista de `TriggerDetector` asignable desde el editor.
  - En `Awake()` se suscribe a `detector.OnTriggerEntered += OnResourceTriggerEnter;` y se desuscribe en `OnDestroy()`.
  - `Initialize(IInventoryService inventoryService)` para inyectar el servicio.
  - `OnResourceTriggerEnter(Transform resourceTransform)` obtiene el `ResourceModel` del Transform y llama a `_inventoryService.AddResource(resource)`.
- `Assets/Code/Scripts/Features/Player/PlayerHandler.cs` - Tambien implementa recoleccion via `TriggerDetector.OnTriggerEntered`, usando `DOMove` para traer el recurso hacia el jugador antes de agregarlo (logica adicional no contemplada en la spec pero compatible).
- `Assets/ExternalAssets/B_Extension/Simple/LayerColliderDetection/TriggerDetector.cs` - Script base usado.

**Verificaciones pendientes del Humano:**
- Confirmar que `ResourceCollector.Initialize(IInventoryService)` se llama desde la escena o un installer (puede que `PlayerHandler` ya cubra esta logica y `ResourceCollector` quede como sistema alternativo).
- Asignar los `TriggerDetector` correspondientes al prefab de recoleccion en escena.