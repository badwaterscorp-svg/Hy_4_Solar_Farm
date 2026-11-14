## 10 BackPack

**Do**
Replace the logic from CarryHandler.cs and turn into BackPack.cs

**Requirements**
- create two methods, one AddResources, check if already Exist, and make RemoceResources

## Human Acceptance

**Estado: Implementado**

Archivos relacionados:
- `Assets/Code/Scripts/Features/BackPack/BackPack.cs` - Reemplaza al CarryHandler. Implementa `IInventoryService`.
  - `AddResource(ResourceModel resource)` - Si el BackPack esta lleno retorna `false`. Si ya existe un recurso con el mismo `Name`, hace `existing.AddAmount(1)`. Si no, crea una copia con `resource.Copy()` y le suma 1. Retorna `true`.
  - `RemoveResource(ResourceModel resource)` - Busca por nombre, resta 1 con `existing.RemoveAmount(1)`, elimina si `Amount <= 0`. Persiste con `SaveInventory()`.
  - Tambien expone `GetResources()`, `GetResourceCount(string)`, `IsBackPackFull`, `AmountMax` (10), `AmountResourcesOn`.
  - Eventos: `OnModelChanged` se dispara en add/remove.
- `Assets/Code/Scripts/Features/BackPack/BackPackHandler.cs` - `MonoBehaviour` que envuelve `BackPack` y lo expone via Zenject (`[Inject]`). Ofrece metodos de conveniencia (`AddResource`, `RemoveResource`, `GetCountResource`, `IsBackPackFull`, `AccessBackPack`).
- `Assets/Code/Scripts/Features/BackPack/BackPackAmountView.cs` - Vista TMP que muestra `AmountResourcesOn/AmountMax` y reacciona a `OnModelChanged`.
- `Assets/Code/GameBindingInstaller.cs` - Vincula `BackPack` como `IInventoryService` singleton.

**Verificaciones pendientes del Humano:**
- No existe un archivo `CarryHandler.cs` en el proyecto (segun la spec debia reemplazarse); verificar que no quede codigo legacy.
- Confirmar que `BackPack.AmountMax = 10` es el valor deseado o exponerlo en el editor.
- Validar visualmente `BackPackAmountView` en la UI del juego.