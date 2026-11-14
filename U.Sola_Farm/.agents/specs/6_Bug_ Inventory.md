## 6 Bug inventory
**Context**
En este momento en ResourceInventoryService agrega un objeto, quiero cambiar esto

**Requeriments**
-haz que cada vez que se agregue un resource con el mismo nombre o que ya este su referencia aumente su varible cantidad, si no esta creala en ResourceModel

-crea un event action para el ResourceModel que se invoque cuando su cantidad haya cambiado, cob el primer parametro, valor actual y valor anterior

## Human Acceptance

**Estado: Implementado**

Archivos relacionados:
- `Assets/Code/Scripts/Features/Resources/ResourceModel.cs`:
  - `public int Amount;` - Cantidad modificable.
  - `public void AddAmount(int newAmount)` y `RemoveAmount(int removeAmount)` - Modifican `Amount` e invocan `OnAmountChanged(Amount, previous)`.
  - `public event Action<int, int> OnAmountChanged;` - Evento con firma `(valorActual, valorAnterior)`.
  - `public ResourceModel Copy()` - Permite clonar instancias.
- `Assets/Code/Scripts/Features/BackPack/BackPack.cs` (ex-`ResourceInventoryService`):
  - `AddResource(ResourceModel)` - Busca por nombre (`r.Name == resource.Name`), si existe llama `existing.AddAmount(1)`, si no crea `resource.Copy()` y le suma 1. **No agrega duplicados.**
  - `RemoveResource(ResourceModel)` - Resta y elimina si `Amount <= 0`.

**Verificaciones pendientes del Humano:**
- Confirmar que `OnAmountChanged` se usa en alguna vista (ej. `BackPackAmountView` actualmente solo escucha `IInventoryService.OnModelChanged`, no `ResourceModel.OnAmountChanged`).
- Validar que el debug.Log comentado en `ResourceModel.AddAmount`/`RemoveAmount` se descomente o se remplace por un sistema de logging mas solido.