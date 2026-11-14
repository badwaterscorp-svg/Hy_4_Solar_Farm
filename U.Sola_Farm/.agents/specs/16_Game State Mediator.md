## 16 Game State Mediator

**Do**
Quiero que hagas un mediador del juego

**Requirements**

- debe tener un metodo subscribirse otro para desusbcrivirse y otro para publicar
- debe tener un enum del tipo de evento de juegos por ejemplo intro,gameStarted, missionCumplished

## Human Acceptance

**Estado: Implementado**

Archivos relacionados:
- `Assets/Code/Scripts/Services/GameMediator/GameStateMediator.cs`:
  - Clase `GameStateContext.GameStateMediator` con `static Subscribe(GameEventType, UnityAction)`, `Unsubscribe(GameEventType, UnityAction)` y `internal Publish(GameEventType)`.
  - Implementado sobre `Dictionary<GameEventType, UnityEvent>`.
- `Assets/Code/Scripts/Services/GameMediator/GameEventType.cs`:
  - `public enum GameEventType { Intro, GameStarted, MissionAccomplished }` - Coincide con los ejemplos de la spec.
- `Assets/Code/Scripts/Services/GameMediator/UI/ButtonStartGame.cs` - Boton UI que dispara el evento.
- `Assets/Code/Scripts/Features/Player/PlayerHandler.cs` - Llama a `GameStateContext.ChangeState(GameEventType.GameStarted)` desde `StartGame()`.
- `Assets/Code/Scripts/Features/Spawner/SolarPanelSpawnerHandler.cs` y `WaterSpawnerHandler.cs` - Se suscriben a `GameEventType.GameStarted` para `StartSpawning()`.

**Verificaciones pendientes del Humano:**
- `Publish` es `internal`, por lo que solo puede ser invocado desde el mismo assembly. Esta bien para la mayoria de los casos, pero si se quiere publicar desde otro assembly, hacerlo `public`.
- Considerar mover `MissionAccomplished` a la carga real del `MissionManager` cuando se complete una mision (actualmente no se observa quien publica este evento).
- Probar el flujo completo: Boton Start -> `GameStarted` -> Spawners arrancan -> Player se mueve.