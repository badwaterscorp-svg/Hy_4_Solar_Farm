## 17 Missions

**Do**
Quiero hacer un manager de misiones

**Requirements**
- crea una interfaz Imission, que tenga el metodo iniciarMision y TerminarMision (trafucido al inglés)
- crea in base mission handler que herede de monobehaviour y vojtenga la IMission
- crea un missionManager.cs que tenga una lista de missionHandler que se puedan serializar por medio de IMission, revisa el codigo de BadReferenceSerilizable para hacer esto

## Human Acceptance

**Estado: Implementado (parcial)**

Archivos relacionados:
- `Assets/Code/Scripts/Features/Missions/IMission.cs`:
  - `void StartMission();` y `void CompleteMission();` (ingles).
  - Tambien expone `string GetMissionState();` (extension util).
- `Assets/Code/Scripts/Features/Missions/BaseMissionHandler.cs`:
  - `public abstract class BaseMissionHandler : MonoBehaviour, IMission` - Hereda de `MonoBehaviour` e implementa `IMission`. Metodos `StartMission`, `CompleteMission`, `GetMissionState` quedan abstractos.
  - Expone `[field: SerializeField] UnityEvent OnMissionComplete;` para Unity Events desde el editor.
- `Assets/Code/Scripts/Features/Missions/MissionManager.cs`:
  - `[RequireBadInterface(typeof(IMission))] [SerializeField] private List<MissionSeeCinematic> _missions = new List<MissionSeeCinematic>();` - **Nota: la lista esta tipada a `MissionSeeCinematic` (clase concreta), no a `IMission` con wrapper.** Hay una clase `MissionHolder` (no usada en la lista) que muestra el patron con `RequireBadInterface`.
  - Metodos `StartMission(int)`, `CompleteMission(int)`, `StartAllMissions()`.
- `Assets/Code/Scripts/Features/Missions/Mission Implementation/MissionSeeCinematic.cs` - Implementacion concreta que extiende `BaseMissionHandler`. Marca un cinematic como visto usando `ICinematicDataService`.
- `Assets/Code/Scripts/Services/ICinematicDataService.cs` y `CinematicDataService.cs` - Persistencia de cinemáticas (PlayerPrefs).
- `Assets/ExternalAssets/B_Extension/Advance/Serialized Interface/RequireBadInterfaceAttribute.cs` - Atributo para serializar interfaces.
- `Assets/ExternalAssets/B_Extension/Advance/Serialized Interface/InterfaceHolder.cs` - Holder alternativo con `IMission Mission => _mission as IMission;` (mismo patron que `MissionHolder` en `MissionManager.cs`).

**Verificaciones pendientes del Humano:**
- **No se sigue el patron `MissionHolder`** (el que aparece en `InterfaceHolder.cs` / `MissionHolder` dentro de `MissionManager.cs`): la lista `_missions` es de tipo `MissionSeeCinematic` (clase concreta). Si se quiere usar la serializacion por interfaz, cambiar a `List<MissionHolder>` o usar `InterfaceHolder<T>` con `RequireBadInterface` en cada elemento. Confirmar.
- Conectar `MissionSeeCinematic.OnMissionComplete` (UnityEvent) a la logica que publique `GameEventType.MissionAccomplished` en el `GameStateMediator`.
- Crear mas `BaseMissionHandler` concretos si se necesitan mas tipos de misiones.
- Llamar a `MissionManager.StartAllMissions()` o `StartMission(int)` desde un punto de entrada del juego (al pulsar Start, al colisionar, etc).