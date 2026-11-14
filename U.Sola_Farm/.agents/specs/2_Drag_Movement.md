## 2 Player movement

**Do**
- utiliza el skill ufeature para crear el movimiento de mi personaje
**Requeriment 1**
- debes utilizar el new input system, y InputReference
- Se debe mostrar en la ui un punto central donde empezo el drag
- se debe limitar un circulo hasta donde el drag ha llegado
**Criterio Aceptacion**
- se debe usar solo un dedo
- mi jugador se debe mover haca la direccion que se dirije la posicion actual con diferencia a la posicion inicial

## Human Acceptance

**Estado: Implementado (parcial)**

Archivos relacionados:
- `Assets/Code/Scripts/Services/DragInputService.cs` - Usa el nuevo Input System (`InputActionAsset`, `InputAction`, `Click`/`Point` del action map "UI"). Inyecta via Zenject como `IInputService`.
- `Assets/Code/Scripts/Services/Interfaces/IInputService.cs` - Contrato del servicio de input.
- `Assets/Code/Scripts/Features/Player/PlayerMovement.cs` - Calcula `Vector3 moveDir` a partir de `(direction.x, 0, direction.y)` y mueve el `Rigidbody` con `MovePosition`.
- `Assets/Code/Scripts/Features/Player/PlayerHandler.cs` - En `FixedUpdate` toma `PointPosition - DragStartPosition` como direccion y `strength = dragDir.magnitude` para mover.
- `Assets/Code/Scripts/Features/Player/PlayerModel.cs` - Modelo con `MoveSpeed`.
- `Assets/Code/GameBindingInstaller.cs` - Inyecta `DragInputService` como `IInputService`.

**Verificaciones pendientes del Humano:**
- Confirmar que el `InputActionAsset` (`InputSystem_Actions.inputactions`) tiene el action map "UI" con las acciones `Click` y `Point` configuradas para un solo dedo (touch).
- Crear/asignar el prefab del Player en escena con `PlayerHandler`, `Rigidbody`, `PlayerModel`, `BackPackHandler`, `TriggerDetector`, `Animator` y los parametros `Started`/`Speed` configurados en el Animator.
- **Punto UI de inicio del drag y limite circular:** `DragReadUIService` (en el mismo archivo) solo imprime la direccion en consola, **NO** dibuja el punto inicial ni el circulo limite en pantalla. Falta implementar la visualizacion.
- Verificar visualmente que la rotacion del personaje (`LookRotation`) y la animacion respondan a la fuerza del drag.