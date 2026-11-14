### BeginMultipeerSession C# Example


Source: https://doc.photonengine.com/fusion/current/technical-samples/fusion-multi-peer-area-of-interest

This function generates multiple clients, starts a game session for each, and loads the main scene additively if the runner is the scene authority. It also logs errors if starting the game fails.

```csharp
public async void BeginMultipeerSession()
{
    mainPanel.SetActive(false);

    for (int i = 0; i < PlayersToSpawn; i++)
    {
        var newRunner = Instantiate(aoiRunnerPrefab);

        StartGameArgs startGameArgs = new StartGameArgs()
        {
            GameMode = currentGameMode,
            PlayerCount = 100,
        };

        var result = await newRunner.StartGame(startGameArgs);

        if (!result.Ok)
        {
            Debug.LogError(result.ErrorMessage);
            return;
        }

        if (newRunner.IsSceneAuthority)
        {
            var loadSceneResult = newRunner.LoadScene(aoiSceneName, loadSceneMode: UnityEngine.SceneManagement.LoadSceneMode.Additive);

            await loadSceneResult;

            if (loadSceneResult.IsDone)
            {
                Debug.Log("Scene Load Complete");
            }
        }

        runnerPanel.AddRunner(newRunner);
    }
}
```

--------------------------------

### Using PlayFabAuthenticator in Fusion Application

Source: https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/authentication/playfab

This example shows how to initiate the PlayFab authentication process and use the obtained authentication values when starting a Fusion game. Ensure the PlayFabAuthenticator class is accessible.

```csharp
public class RunnerManager : MonoBehaviour
{
    private void Awake()
    {
        PlayFabAuthenticator.AuthenticateWithPlayFab();
    }

    private static async Task<NetworkRunner> StartGame(GameMode gameMode)
    {
        var runner = CreateNetworkRunner(); // creates a custom NetworkRunner object

        // Use the PlayFabAuthenticator.AuthValues to authenticate the player
        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode   = gameMode,
            AuthValues = PlayFabAuthenticator.AuthValues // gets the PlayFab authentication values
        });

        return runner;
    }

    // ... other methods
}
```

--------------------------------

### Start Game with Matchmaking Arguments

Source: https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/matchmaking

Use these arguments when starting a new Fusion simulation to customize session creation and filtering for matchmaking.

```csharp
NetworkRunner.StartGame(new StartGameArgs {
  // ...
  // Arguments related to the Matchmaking between peers
  SessionName = [string],
  SessionProperties = [Dictionary<string, SessionProperty>],
  CustomLobbyName = [string],
  EnableClientSessionCreation = [bool],
  PlayerCount = [int],
  IsOpen = [bool],
  IsVisible = [bool],
  MatchmakingMode = [MatchmakingMode],
});
```

--------------------------------

### Start a New Game Session with Custom Properties

Source: https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/matchmaking

Create a new game session as a host and define custom properties that clients can use for filtering. This example uses enums for session properties.

```csharp
// Some predefined types used as values for the Game Session Properties
public enum GameType : int {
  FreeForAll,
  Team,
  Timed
}

public enum GameMap : int {
  Forest,
  City,
  Desert
}

// Utility method to start a Host using a defined GameMap and GameType
public async Task StartHost(NetworkRunner runner, GameMap gameMap, GameType gameType) {

  var customProps = new Dictionary<string, SessionProperty>();

  customProps["map"] = (int)gameMap;
  customProps["type"] = (int)gameType;

  var result = await runner.StartGame(new StartGameArgs() {
    GameMode = GameMode.Host,
    SessionProperties = customProps,
  });

  if (result.Ok) {
    // all good
  } else {
    Debug.LogError($"Failed to Start: {result.ShutdownReason}");
  }
}
```

--------------------------------

### Start Dedicated Server with Default Settings

Source: https://doc.photonengine.com/fusion/current/technical-samples/dedicated-server/fusion-dedicated-server

Run the game server executable in headless mode with logging enabled. This starts a game session with default settings.

```bash
.\FusionDedicatedServerSample.exe -batchmode -nographics -logFile output.log
```

--------------------------------

### Example Headless Server Command Lines

Source: https://doc.photonengine.com/fusion/current/game-samples/br200/quickstart

Examples of command-line arguments for running headless servers for BR200. These arguments configure game mode, player count, and scene loading.

```bash
BR200.exe -batchmode -nographics -dedicatedServer -logFile DeathmatchServer.txt -deathmatch -maxPlayers 60 -scene GenArea3
```

```bash
BR200.exe -batchmode -nographics -dedicatedServer -logFile BattleRoyaleServer.txt -battleRoyale -maxPlayers 200 -scene GenArea7
```

--------------------------------

### Start Host in a Custom Lobby

Source: https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/matchmaking

This method demonstrates how a Server/Host can create a session in a custom lobby. Specify the custom lobby name when starting the game.

```csharp
public async Task StartHost(NetworkRunner runner) {

  var result = await runner.StartGame(new StartGameArgs() {
    GameMode = GameMode.Host,
    CustomLobbyName = "MyCustomLobby"
  });

  if (result.Ok) {
    // all good
  } else {
    Debug.LogError($"Failed to Start: {result.ShutdownReason}");
  }
}
```

--------------------------------

### RPC_Configure Example

Source: https://doc.photonengine.com/fusion/current/manual/rpc

Example of an RPC method declaration that accepts a string, Color, and RpcInfo. The RpcInfo provides metadata about the RPC call.

```APIDOC
## RPC_Configure Example

### Description
This RPC method `RPC_Configure` is designed to set player name and color. It demonstrates the usage of `RpcInfo` to access metadata about the RPC call, such as tick, source player, channel, and invocation origin.

### Method Signature
```csharp
[Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
public void RPC_Configure(string name, Color color, RpcInfo info = default)
```

### Parameters
* `name` (string) - The name to configure.
* `color` (Color) - The color to configure.
* `info` (RpcInfo) - Optional parameter providing metadata about the RPC call.
```

--------------------------------

### NetworkRunner StartGame Example

Source: https://doc.photonengine.com/fusion/current/game-samples/pirate-adventure/tech-design

Demonstrates the basic usage of NetworkRunner.StartGame, which works in WebGL builds.

```csharp
await NetworkRunner.StartGame
```

--------------------------------

### Configuring StartGameArgs for Shared Mode

Source: https://doc.photonengine.com/fusion/current/game-samples/fusion-quiz-network

Sets up the arguments for starting a game session, specifying the GameMode as Shared, an optional SessionName, and the maximum PlayerCount. If joinRandomRoom is true or LocalRoomName is empty, matchmaking will attempt to join an open session or create a new one with a GUID. Otherwise, it tries to join or create a session with the specified LocalRoomName.

```csharp
public async void StartGame(bool joinRandomRoom)
{
    StartGameArgs startGameArgs = new StartGameArgs()
    {
        GameMode = GameMode.Shared,
        SessionName = joinRandomRoom ? string.Empty : LocalRoomName,
        PlayerCount = 20,
    };
    // ...
}
```

--------------------------------

### Start Fusion Game with Custom Realtime Client

Source: https://doc.photonengine.com/fusion/current/manual/advanced/custom-realtime-client

This method shows how to start a Fusion `NetworkRunner` and pass a custom `RealtimeClient` instance. If no `RealtimeClient` is provided, Fusion creates and manages its own.

```csharp
private async Task StartGame(GameMode gameMode)
{
    Debug.Log("Starting game...");

    _runner = Instantiate(runnerPrefab);

    var sceneInfo = new NetworkSceneInfo();
    var sceneRef = SceneRef.FromIndex(0);
    if (sceneRef.IsValid)
    {
        sceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Additive);
    }

    var result = await _runner.StartGame(new StartGameArgs()
    {
        GameMode = gameMode,
        RealtimeClient = _client, // passing the custom Realtime Client
        Scene = sceneInfo
    });

    if (result.Ok)
    {
        Debug.Log($"Runner has started: {_runner.SessionInfo.Name}");
    }
}
```

--------------------------------

### Instantiating and Starting a NetworkRunner

Source: https://doc.photonengine.com/fusion/current/game-samples/fusion-quiz-network

Instantiates a NetworkRunner prefab and attempts to start a game session with the provided startGameArgs. It handles the result, updating the UI to display the room name on success or showing an error message and returning to the main menu on failure. Debug logs are used for error reporting.

```csharp
// ...
NetworkRunner newRunner = Instantiate(_networkRunnerPrefab);

StartGameResult result = await newRunner.StartGame(startGameArgs);

if (result.Ok)
{
    roomName.text = "Room:  " + newRunner.SessionInfo.Name;
    GoToGame();
}
else
{
    roomName.text = string.Empty;

    GoToMainMenu();

    errorMessageObject.SetActive(true);
    TextMeshProUGUI gui = errorMessageObject.GetComponentInChildren<TextMeshProUGUI>();
    if (gui) {
        gui.text = result.ErrorMessage;
    }

    Debug.LogError(result.ErrorMessage);
}
// ...
```

--------------------------------

### Start NetworkRunner in Current Scene

Source: https://doc.photonengine.com/fusion/current/manual/network-runner

Use this to start the NetworkRunner in the currently active scene. Ensure the scene is added to the build settings.

```csharp
var sceneRef =  SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
NetworkSceneInfo info = new NetworkSceneInfo();
info.AddSceneRef(sceneRef, LoadSceneMode.Single);

runner.StartGame(new StartGameArgs()
{
    Scene = info,
    GameMode = GameMode.Shared,
});
```

--------------------------------

### Custom Object Provider Example in C#

Source: https://doc.photonengine.com/fusion/current/manual/advanced/advanced-spawning

Implement a custom NetworkObjectProvider to create NetworkObjects at runtime. This example shows how to detect custom spawn requests and bake a dynamically created GameObject into a NetworkObject.

```csharp
public class BakingObjectProvider : NetworkObjectProviderDefault
{
  // For this sample, we are using very high flag values to indicate custom.
  // Other values will fall through the default instantiation handling.
  public const int CUSTOM_PREFAB_FLAG = 100000;

  // The NetworkObjectBaker class can be reused and is Runner independent.
  private static NetworkObjectBaker _baker;
  private static NetworkObjectBaker Baker => _baker ??= new NetworkObjectBaker();

  public override NetworkObjectAcquireResult AcquirePrefabInstance(NetworkRunner runner, in NetworkPrefabAcquireContext context, out NetworkObject result)
  {
    // Detect if this is a custom spawn by its high prefabID value we are passing.
    // The Spawn call will need to pass this value instead of a prefab.
    if (context.PrefabId.RawValue == CUSTOM_PREFAB_FLAG)
    {
      var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
      var no = go.AddComponent<NetworkObject>();
      go.AddComponent<NetworkTransform>();
      go.name = $"Custom Object";

      // Baking is required for the NetworkObject to be valid for spawning.
      Baker.Bake(go);

      // Move the object to the applicable Runner Scene/PhysicsScene/DontDestroyOnLoad
      // These implementations exist in the INetworkSceneManager assigned to the runner.
      if (context.DontDestroyOnLoad)
      {
        runner.MakeDontDestroyOnLoad(go);
      }
      else
      {
        runner.MoveToRunnerScene(go);
      }

      // We are finished. Return the NetworkObject and report success.
      result = no;
      return NetworkObjectAcquireResult.Success;
    }

    // For all other spawns, use the default spawning.
    return base.AcquirePrefabInstance(runner, context, out result);
  }
}
```

--------------------------------

### Start Fusion NetworkRunner and Session

Source: https://doc.photonengine.com/fusion/current/tutorials/host-mode-basics/2-setting-up-a-scene

This method initializes the Fusion NetworkRunner, enables input provision, and starts a game session. It configures the session with a game mode, session name, and scene.

```csharp
private NetworkRunner _runner;

async void StartGame(GameMode mode)
{
    // Create the Fusion runner and let it know that we will be providing user input
    _runner = gameObject.AddComponent<NetworkRunner>();
    _runner.ProvideInput = true;

    // Create the NetworkSceneInfo from the current scene
    var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
    var sceneInfo = new NetworkSceneInfo();
    if (scene.IsValid)
    {
        sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
    }

    // Start or join (depends on gamemode) a session with a specific name
    await _runner.StartGame(new StartGameArgs()
    {
        GameMode = mode,
        SessionName = "TestRoom",
        Scene = scene,
        SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
    });
}
```

--------------------------------

### Start Fusion in Server Mode (Pre-defined Local EndPoint)

Source: https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/matchmaking

Specify a particular Local EndPoint for the server. The peer will fail to start if the port is already in use. Use NetAddress.Any(port) if only the port needs to be specified.

```csharp
NetworkRunner.StartGame(new StartGameArgs() {
  GameMode = GameMode.Server,
  Address = NetAddress.CreateFromIpPort("192.168.0.10", 27015) // or NetAddress.Any(27015)
  // other args
});
```

--------------------------------

### Start Fusion Dedicated Server with Multiplay Configuration

Source: https://doc.photonengine.com/fusion/current/technical-samples/dedicated-server/fusion-dedicated-server-multiplay

Use this method to start the Fusion runner for a dedicated server. It configures the Photon App Settings with a custom region if provided and binds the network address to the specified port on all interfaces (0.0.0.0).

```csharp
private Task<StartGameResult> StartSimulation(
  NetworkRunner runner,
  string SessionName,
  ushort port,
  string customRegion,
) {

  // Build Custom Photon Config
  var photonSettings = PhotonAppSettings.Instance.AppSettings.GetCopy();

  if (string.IsNullOrEmpty(customRegion) == false) {
    photonSettings.FixedRegion = customRegion.ToLower();
  }

  // Start Runner
  return runner.StartGame(new StartGameArgs() {
    SessionName = SessionName,                  // Custom Session Name
    GameMode = GameMode.Server,                 // Game mode always set to Server
    Address = NetAddress.Any(port),             // EndPoint to bind: 0.0.0.0:port
    CustomPhotonAppSettings = photonSettings,   // Custom Photon App Settings
    // other arguments
  });
}
```

--------------------------------

### Start Dedicated Server with Custom Arguments

Source: https://doc.photonengine.com/fusion/current/technical-samples/dedicated-server/fusion-dedicated-server

Run the game server with custom session name, region, lobby, port, and properties. This allows for fine-grained control over the game session.

```bash
.\FusionDedicatedServerSample.exe -batchmode -nographics -logFile output.log -session my-custom-session -region us -lobby my-custom-lobby -port 30001 -Pmap city -Ptype free-for-all
```

--------------------------------

### Start Game With Custom Region

Source: https://doc.photonengine.com/fusion/current/connection-and-authentication/regions

Use this method to start a NetworkRunner with a specific region. Ensure the region string is in lowercase.

```csharp
public class FusionInit : MonoBehaviour {

    // ...

    // Start a NetworkRunner with a Game Mode and connect to the specified Region
    public Task<StartGameResult> StartSimulation(NetworkRunner runner, GameMode gameMode, string region) {

        var appSettings = BuildCustomAppSetting(region);

        return runner.StartGame(new StartGameArgs() {
            // ...
            GameMode = gameMode,
            CustomPhotonAppSettings = appSettings
        });
    }

    private FusionAppSettings BuildCustomAppSetting(string region, string customAppID = null, string appVersion = "1.0.0") {

        var appSettings = PhotonAppSettings.Global.AppSettings.GetCopy();;

        appSettings.UseNameServer = true;
        appSettings.AppVersion = appVersion;

        if (string.IsNullOrEmpty(customAppID) == false) {
            appSettings.AppIdFusion = customAppID;
        }

        if (string.IsNullOrEmpty(region) == false) {
            appSettings.FixedRegion = region.ToLower();
        }

        // If the Region is set to China (CN),
        // the Name Server will be automatically changed to the right one
        // appSettings.Server = "ns.photonengine.cn";

        return appSettings;
    }
}
```

--------------------------------

### Example Player Data Manipulation

Source: https://doc.photonengine.com/fusion/current/game-samples/fusion-razor-madness

Demonstrates how to retrieve player data and then use it to despawn the player's instance or access their nickname.

```csharp
//e.g
 PlayerData data = GetPlayerData(player, Runner);
 Runner.despawn(data.Instance);
 string playerNick = data.Nick;
```

--------------------------------

### Static RPC Example

Source: https://doc.photonengine.com/fusion/current/manual/rpc

Demonstrates a static RPC that can be called from any client and is sent to all clients, requiring a NetworkRunner as the first parameter.

```APIDOC
## Rpc_MyStaticRpc

### Description
A static RPC that can be called from any client and is sent to all clients.

### Method Signature
`public static void Rpc_MyStaticRpc(NetworkRunner runner, int a)`

### Attribute
`[Rpc]`

### Parameters
- **runner** (NetworkRunner) - The network runner instance.
- **a** (int) - An integer parameter for the RPC.

### Example Usage
```csharp
[Rpc]
public static void Rpc_MyStaticRpc(NetworkRunner runner, int a) { }
```
```

--------------------------------

### Sample Registry Implementation

Source: https://doc.photonengine.com/fusion/current/industries-samples/industries-addons/fusion-industries-addons-subscriber-registry

A basic example of a custom registry class inheriting from Registry<T>. This class will manage registrations for subscribers of the specified type.

```csharp
class SampleRegistry : Registry<SampleSubscriber> {}
```

--------------------------------

### RPC_Configure with HostMode

Source: https://doc.photonengine.com/fusion/current/manual/rpc

Example demonstrating how to configure an RPC to include the host's PlayerRef when Fusion is running in HostMode.

```APIDOC
## RPC_Configure with HostMode

### Description
This RPC method `RPC_Configure` is adjusted to use `RpcHostMode.SourceIsHostPlayer`. This ensures that when Fusion is in HostMode, the `RpcInfo.Source` will contain the host's local `PlayerRef` instead of `PlayerRef.None`.

### Method Signature
```csharp
[Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority, HostMode = RpcHostMode.SourceIsHostPlayer)]
public void RPC_Configure(string name, Color color, RpcInfo info = default)
```

### Parameters
* `name` (string) - The name to configure.
* `color` (Color) - The color to configure.
* `info` (RpcInfo) - Optional parameter providing metadata about the RPC call, including source player information when HostMode is configured appropriately.
```

--------------------------------

### Setup and Manage Custom Realtime Client

Source: https://doc.photonengine.com/fusion/current/manual/advanced/custom-realtime-client

This script demonstrates how to set up and manage a custom Photon Realtime Client for Fusion. It includes methods for connecting to Photon Cloud, joining lobbies, connecting to rooms, and disconnecting. Ensure `SetupForFusion()` is called on both `RealtimeClient` and `MatchmakingArguments` when using them with Fusion.

```csharp
using System.Threading.Tasks;
using Fusion;
using Fusion.Matchmaking;
using Fusion.Photon.Realtime;
using Photon.Realtime;
using UnityEngine;

public class SimpleMatchmaker : MonoBehaviour
{

    // NetworkRunner Prefab Reference
    public NetworkRunner runnerPrefab;

    // Internal references
    private RealtimeClient _client;
    private NetworkRunner _runner;

    // Setup Realtime Client if necessary
    private void SetupRealtimeClient()
    {
        if (_client != null) return;

        // Create a Realtime Client and set it up for Fusion usage
        _client = new RealtimeClient().SetupForFusion(); // must be invoked, see below
        _client.AddCallbackTarget(this);
    }

    private void Start()
    {
        // Keep it around
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        // Check if NetworkRunner is valid
        if (_runner == false)
        {
            // Run updates
            _client?.Service();
        }
    }

    // Join a Lobby to look for a Session to Join
    private async Task JoinSessionLobby()
    {
        Debug.Log("Join Session Lobby...");

        SetupRealtimeClient();

        // Connect to the Photon Cloud
        await _client.ConnectUsingSettingsAsync(PhotonAppSettings.Global.AppSettings);

        // Join Lobby
        await _client.JoinLobbyAsync();

        if (_client.InLobby)
        {
            Debug.Log($"Joined Lobby: {_client.CurrentLobby.Name} [{_client.CurrentLobby.Type}]");
        }
    }

    // Connect to a given Room Name or join a random one
    // if no roomName is passed
    private async Task ConnectToRoom(string roomName = null)
    {
        Debug.Log($"ConnectToRoom: {roomName}");

        SetupRealtimeClient();

        // Setup Matchmaking
        var matchmakingArguments = new MatchmakingArguments
        {
            RoomName = roomName // if null, a random name will be generated by the Photon Cloud
        }.SetupForFusion(); // must be invoked, see below

        await _client.ConnectToRoomAsync(matchmakingArguments);

        if (_client.InRoom)
        {
            Debug.Log($"Client is in room: {_client.CurrentRoom.Name} [{_client.CurrentRegion}]");
        }
        else
        {
            Debug.LogError("Unable to join Room");
        }
    }

    // Disconnect the client from the Photon Cloud
    private async Task DisconnectFromCloud()
    {
        Debug.Log("Disconnecting...");

        if (_client != null)
        {
            await _client.DisconnectAsync();
        }

        _client = null;
    }
}
```

--------------------------------

### Server-Side Authentication Response Examples

Source: https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/authentication/custom-authentication

Examples of JSON responses from a custom authentication server. Use `ResultCode: 1` for success with a `UserId`, and `ResultCode: 3` or `ResultCode: 2` for different types of failures.

```json
{ "ResultCode": 1, "UserId": <userId> }
```

```json
{ "ResultCode": 3, "Message": "Invalid parameters." }
```

```json
{ "ResultCode": 2, "Message": "Authentication failed. Wrong credentials." }
```

--------------------------------

### Start Fusion in Server Mode (Full Automatic)

Source: https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/matchmaking

Use this method when a pre-defined port is not necessary. The STUN Protocol is used to resolve the Public EndPoint.

```csharp
NetworkRunner.StartGame(new StartGameArgs() {
    GameMode = GameMode.Server
    // other args
});
```

--------------------------------

### JoinGame Webhook Request JSON Example

Source: https://doc.photonengine.com/fusion/current/manual/advanced/webhooks

Example JSON payload for the JoinGame webhook. Includes AppId, GameId, UserId, and AuthCookie.

```json
{
  "AppId": "*******************",
  "GameId": "0:eu:db757806-8570-45aa",
  "UserId": "db757806-8570-45aa",
  "AuthCookie": {
    "Secret": "**********"
  }
}
```

--------------------------------

### Platform Movement Script Example

Source: https://doc.photonengine.com/fusion/current/addons/advanced-kcc/features

Implement this script on platform objects to enable tracking and propagation of movement to the KCC. Ensure it has a Rigidbody and implements IPlatform. The execution order must be lower than PlatformProcessor.

```csharp
[DefaultExecutionOrder(-1000)]
[RequireComponent(typeof(Rigidbody))]
public sealed class PlatformMovement : NetworkTRSPProcessor, IPlatform, IBeforeAllTicks
{
    public Transform Transform;
    public Rigidbody Rigidbody;

    public override void Spawned()
    {
        // Enable simulation also for proxy object.
        Runner.SetIsSimulated(Object, true);
    }

    public override void FixedUpdateNetwork()
    {
        CalculateNextState(out Vector3 nextPosition, out Quaternion nextRotation);

        // Set network state of NetworkTRSP.
        State.Position = nextPosition;
        State.Rotation = nextRotation;

        // Update engine components.
        Transform.position = nextPosition;
        Transform.rotation = nextRotation;
        Rigidbody.position = nextPosition;
    }

    public override void Render()
    {
        CalculateNextState(out Vector3 nextPosition, out Quaternion nextRotation);

        // Update only engine components, do not store in network state.
        Transform.position = nextPosition;
        Transform.rotation = nextRotation;
        Rigidbody.position = nextPosition;
    }

    void IBeforeAllTicks.BeforeAllTicks(bool resimulation, int tickCount)
    {
        // Restore state of the object before simulation.
        Transform.SetPositionAndRotation(State.Position, State.Rotation);
        Rigidbody.position = State.Position;
    }

    private void CalculateNextState(out Vector3 nextPosition, out Quaternion nextRotation)
    {
        nextPosition = State.Position;
        nextRotation = State.Rotation;

        // Movement based on waypoints, AnimationClip, ...
    }
}
```

--------------------------------

### Initialize Enemy AI State Machine with Transitions

Source: https://doc.photonengine.com/fusion/current/addons/fsm

This snippet illustrates the setup of an enemy AI state machine, including defining states and adding a transition between the idle and attack states. The transition is controlled by a custom condition method, CanStartAttack.

```csharp
void IStateMachineOwner.CollectStateMachines(List<IStateMachine> stateMachines)
{
    _enemyAI = new StateMachine<StateBehaviour>("Enemy AI", _idleState, _attackState);
    stateMachines.Add(_enemyAI);

    _idleState.AddTransition(_attackState, CanStartAttack);
}

private bool CanStartAttack()
{
    if (_weapons.HasWeapon == false)
        return false;

    return HasEnemy();
}
```

--------------------------------

### FootstepSetup Class for Audio Configuration

Source: https://doc.photonengine.com/fusion/current/game-samples/br200/interactions

Defines the audio setup for footstep sounds based on surface tags and movement speed (walk/run). Includes fallback sounds.

```csharp
public class FootstepSetup : ScriptableObject
{
    [SerializeField]
    private AudioSetup _fallbackWalkSound;
    [SerializeField]
    private AudioSetup _fallbackRunSound;
    [SerializeField]
    private FootstepData[] _footsteps;

    [Serializable]
    private class FootstepData
    {
        public string Tag;
        public AudioSetup SoundWalk;
        public AudioSetup SoundRun;
    }
}
```

--------------------------------

### Run Fusion Dedicated Server Docker Container with Optional Arguments

Source: https://doc.photonengine.com/fusion/current/technical-samples/dedicated-server/fusion-dedicated-server-docker-image

This example demonstrates running the Fusion Dedicated Server container with optional arguments to customize session name, region, and lobby. The `entrypoint.sh` script within the container parses these arguments.

```bash
docker run -d -p 27015:27015/udp fusion-dedicatedserver -s my_session -r eu -l my_lobby
```

--------------------------------

### Network FSM Setup with Animancer

Source: https://doc.photonengine.com/fusion/current/technical-samples/fusion-animations

Implement the IStateMachineOwner interface to collect and register state machines with the StateMachineController. This ensures necessary data is synchronized across peers.

```csharp
public class Player : NetworkBehaviour, IStateMachineOwner
{
    private PlayerBehaviourMachine _fullBodyMachine;

    void IStateMachineOwner.CollectStateMachines(List<IState> stateMachines)
    {
        var states = GetComponentsInChildren<PlayerStateBehaviour>();
        var animancer = GetComponentInChildren<AnimancerComponent>();

        _fullBodyMachine = new PlayerBehaviourMachine("Full Body", _controller, animancer, states);
        stateMachines.Add(_fullBodyMachine);
    }
}
```

--------------------------------

### Build Fusion Dedicated Server Docker Image

Source: https://doc.photonengine.com/fusion/current/technical-samples/dedicated-server/fusion-dedicated-server-docker-image

Use this command to build the Docker image for your Fusion Dedicated Server. Ensure Docker is installed and running.

```bash
docker build -t <your_custom_image_name> .
```

--------------------------------

### CloseGame Webhook Request JSON Example

Source: https://doc.photonengine.com/fusion/current/manual/advanced/webhooks

Example JSON payload for the CloseGame webhook. Includes GameId and CloseReason.

```json
{
  "GameId": "0:eu:db757806-8570-45aa",
  "CloseReason": 0
}
```

--------------------------------

### Execute Stage Object in Player Class

Source: https://doc.photonengine.com/fusion/current/addons/advanced-kcc/interactions

Demonstrates how to execute a stage object within the KCC system, using DamageBoost as an example.

```csharp
public class Player : NetworkBehaviour
{
    public KCC KCC;

    // The stage is used like a player "attribute".
    private DamageBoost _damageBoost = new DamageBoost();

    public override void FixedUpdateNetwork()
    {
        _damageBoost.Reset();

        KCC.ExecuteStage(_damageBoost);

        float damageMultiplier = _damageBoost.MaxValue;

        // Firing from weapon...
        // The damage multiplier can be combined with base projectile damage.
    }
}
```

--------------------------------

### CreateGame Webhook Response JSON Example

Source: https://doc.photonengine.com/fusion/current/manual/advanced/webhooks

This JSON object is returned by the CreateGame webhook. It can overwrite the GameId and enforce specific room options. Starting in Fusion 2.1, TickConfig can also be set.

```json
{
  "GameId": "0:eu:db757806-8570-45aa",
  "EnterRoomParams": {
    "RoomOptions": {
      "CustomRoomProperties": {
        "GameType": "CUSTOM_GAME_TYPE",
        "CustomData": 101
      }
    }
  },
  "TickConfig": {
    "TickRate": 32,
    "SendInterval": 2
  }
}
```

--------------------------------

### Lag Compensation Component Setup

Source: https://doc.photonengine.com/fusion/current/game-samples/fusion-impostor

This section outlines the component setup required for lag compensation. It details which components need to be added to specific GameObjects within the player and network runner prefabs to ensure accurate hit detection in a networked game.

```text
Additionally, the following changes were made to work with lag compensation:
  * `Hitbox Manager` component was added to the main `NetworkRunner` prefab.
  * `Hitbox Root` component was to the root of the `Player` prefab.
  * `Hitbox` component was added to the `Kill Radius` GameObject in the `Player` prefab and its `Collider` component was removed.
```

--------------------------------

### Basic RPC Example

Source: https://doc.photonengine.com/fusion/current/manual/rpc

Defines a regular CSharp method with the [Rpc] attribute, specifying sources and targets for the RPC call.

```APIDOC
## RPC_Configure

### Description
Configures player name and color via an RPC.

### Method Signature
`public void RPC_Configure(string name, Color color)`

### Attribute
`[Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]`

### Parameters
- **name** (string) - The name to configure.
- **color** (Color) - The color to configure.

### Example Usage
```csharp
[Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
public void RPC_Configure(string name, Color color){
    playerName = name;
    playerColor = color;
}
```
```

--------------------------------

### Initialize Attachment Point Listeners

Source: https://doc.photonengine.com/fusion/current/industries-samples/fusion-vr-training

Sets up listeners for attachment and detachment events on the attachment point in the Start method.

```csharp
void Start()
{
    if (attachmentPoint)
    {
        attachmentPoint.onRegisterAttachment.AddListener(OnSnap);
        attachmentPoint.onUnregisterAttachment.AddListener(OnUnSnap);
    }
}
```

--------------------------------

### LeaveGame Webhook Request JSON Example

Source: https://doc.photonengine.com/fusion/current/manual/advanced/webhooks

Example JSON payload for the LeaveGame webhook. Includes game and user identifiers, actor number, and flags for inactivity and creation failure.

```json
{
  "AppId": "*******************",
  "GameId": "0:eu:db757806-8570-45aa",
  "UserId": "db757806-8570-45aa",
  "ActorNr": 1,
  "AuthCookie": {
    "Secret": "**********"
  },
  "IsInactive": false,
  "FailedOnCreate": false
}
```

--------------------------------

### Initiate Rotation Coroutine

Source: https://doc.photonengine.com/fusion/current/technical-samples/fusion-vr-shared

Starts a coroutine to handle rig rotation, setting rotation state and yielding the FadedRotate coroutine.

```csharp
IEnumerator Rotate(float angle)
{
    timeStarted = Time.time;
    rotating = true;
    yield return rig.FadedRotate(angle);
    rotating = false;
}
```

--------------------------------

### Implementing Child State Machines in C#

Source: https://doc.photonengine.com/fusion/current/addons/fsm

This example demonstrates how to implement child state machines within a parent state. It shows collecting child machines, managing their activation, and handling their lifecycle within the parent's state methods.

```csharp
public class AdvancedAttackBehaviour : StateBehaviour
{
    [SerializeField]
    private PrepareState _prepareState;
    [SerializeField]
    private AttackState _attackState;
    [SerializeField]
    private RecoverState _recoverState;

    private StateMachine<State> _attackMachine;

    protected override void OnCollectChildStateMachines(List<IStateMachine> stateMachines)
    {
        _attackMachine = new StateMachine<State>("Attack Machine", _prepareState, _attackState, _recoverState);
        stateMachines.Add(_attackMachine);
    }

    protected override void OnEnterState()
    {
        // Reset to Prepare state
        _attackMachine.ForceActivateState(_prepareState, true);
    }

    protected override void OnFixedUpdate()
    {
        if (_recoverState.IsFinished == true)
        {
            // Attack finished, deactivate
            Machine.TryDeactivateState(StateId);
        }
    }

    // STATES

    [Serializable]
    public class PrepareState : State
    {
        protected override void OnFixedUpdate()
        {
            if (Machine.StateTime > 1f)
            {
                Machine.TryActivateState<AttackState>();
            }
        }

        protected override void OnEnterStateRender()
        {
            Debug.Log("Preparing attack...");
        }
    }

    [Serializable]
    public class AttackState : State
    {
        protected override void OnFixedUpdate()
        {
            if (Machine.StateTime > 0.5f)
            {
                Machine.TryActivateState<RecoverState>();
            }
        }

        protected override void OnEnterStateRender()
        {
            Debug.Log("Attacking...");
        }
    }

    [Serializable]
    public class RecoverState : State
    {
        public bool IsFinished => Machine.ActiveStateId == StateId && Machine.StateTime > 2f;

        protected override void OnEnterStateRender()
        {
            Debug.Log("Recovering from attack...");
        }
    }
}
```

--------------------------------

### Example 1: Network Object with NetworkRigidbody/NetworkTransform

Source: https://doc.photonengine.com/fusion/current/technical-samples/projectiles-essentials

Showcases spawning a projectile as a NetworkObject and synchronizing its position using NetworkTransform or NetworkRigidbody. This approach is suitable for complex physics projectiles but has instantiation overhead and sends transform updates over the network.

```csharp
public class ProjectileNetworkTransform : NetworkBehaviour
{
    // ... other properties and methods

    public void Initialize(Vector3 position, Quaternion rotation, Vector3 velocity)
    {
        // Set initial position and rotation
        transform.position = position;
        transform.rotation = rotation;

        // Apply initial velocity if using NetworkRigidbody
        if (TryGetComponent<NetworkRigidbody>(out var rb))
        {
            rb.Velocity = velocity;
        }
    }

    // ... other methods for projectile behavior
}
```

--------------------------------

### Sample Subscriber Implementation

Source: https://doc.photonengine.com/fusion/current/industries-samples/industries-addons/fusion-industries-addons-subscriber-registry

Example of a custom subscriber class inheriting from Subscriber<T>. It demonstrates how to define custom availability criteria and override the IsAvailable property to control registration timing.

```csharp
class SampleSubscriber : Subscriber<SampleSubscriber> {
    // Custom availability criteria
    [Networked]
    bool CustomAvailabilityCriteriaMatched { get; set; } = false;

    // Availability override. The data to compute it needs to be synched so that the availability triggers everywhere (and hence the registration)
    public override bool IsAvailable => base.IsAvailable && CustomAvailabilityCriteriaMatched;
}
```

--------------------------------

### RpcFoo with RpcInvokeInfo

Source: https://doc.photonengine.com/fusion/current/manual/rpc

Example of an RPC method that returns RpcInvokeInfo, providing details about the RPC invocation and send operations.

```APIDOC
## RpcFoo with RpcInvokeInfo

### Description
This example shows an RPC method `RpcFoo` that returns an `RpcInvokeInfo` object. This object contains details about the local invocation result and the send operation results for remote peers, such as message size and culled receivers. It is invoked within `FixedUpdateNetwork`.

### Method Signature
```csharp
[Rpc]
public RpcInvokeInfo RpcFoo()
```

### Return Value
* `RpcInvokeInfo` - An object containing information about the RPC invoke and send operations.
  * `LocalInvokeResult`: Indicates the success or failure of the local invoke action.
  * `SendCullResult`: Indicates the success or failure of the RPC invocation for remote peers.
  * `SendResult`: Contains meta information about the RPC send operation, including result flags, message size, and collections of receivers.

### Usage Example
```csharp
public override void FixedUpdateNetwork() {
  var info = RpcFoo();
  Debug.Log(info);
}
```
```

--------------------------------

### Assigning a Custom Object Provider

Source: https://doc.photonengine.com/fusion/current/manual/advanced/advanced-spawning

Pass an instance of a custom `INetworkObjectProvider` when starting the game. Ensure your custom provider implements the necessary methods for acquiring and releasing network objects.

```csharp
runner.StartGame(new StartGameArgs(){ ObjectProvider = new MyObjectProvider()});
```

--------------------------------

### Power-up Spawner using SimulationBehaviour

Source: https://doc.photonengine.com/fusion/current/manual/advanced/simulation-behaviour

Example of a power-up spawner using SimulationBehaviour. It spawns power-ups at a set interval using FixedUpdateNetwork. The spawning logic is server-authoritative and not synchronized with clients.

```csharp
public class BasicPowerUpSpawner : SimulationBehaviour {

    // Local list of prefabs for the available power ups to be spawned.
    [SerializeField] private List<NetworkPrefabRef> _availablePowerUps = new List<NetworkPrefabRef>();

    private float _spawnDelay = 3f;

    public void RegisterOnRunner() {
      // Find the network runner for this gameobject scene. This is useful on a scene object.
      var runner = NetworkRunner.GetRunnerForGameObject(gameObject);

      // Make sure the network runner is started and running.
      if (runner.IsRunning) {
        runner.AddGlobal(this);
      }
    }

    public void RemoveFromRunner() {
      // The same process can be done to remove the SimulationBehaviour.
      var runner = NetworkRunner.GetRunnerForGameObject(gameObject);
      if (runner.IsRunning) {
        runner.RemoveGlobal(this);
      }
    }

    public override void FixedUpdateNetwork() {
      if (Runner.Tick % Mathf.RoundToInt(Runner.TickRate * _spawnDelay) == 0) {
        // Generate a random index to select a power up from the list.
        int randomIndex = Random.Range(0, _availablePowerUps.Count);
        // Spawn the selected power up.
        Runner.Spawn(_availablePowerUps[randomIndex]);
      }
    }
  }
```

--------------------------------

### Implement Host Migration Callback

Source: https://doc.photonengine.com/fusion/current/manual/advanced/host-migration

Implement the `OnHostMigration` callback to handle the host migration process. This involves shutting down the current runner and starting a new one using the provided `HostMigrationToken`.

```csharp
public async void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) {

    // Step 2.1
    // Shutdown the current Runner, this will not be used anymore. Perform any prior setup and tear down of the old Runner

    // The new "ShutdownReason.HostMigration" can be used here to inform why it's being shut down in the "OnShutdown" callback
    await runner.Shutdown(shutdownReason: ShutdownReason.HostMigration);

    // Step 2.2
    // Create a new Runner.
    var newRunner = Instantiate(_runnerPrefab);

    // setup the new runner...

    // Start the new Runner using the "HostMigrationToken" and pass a callback ref in "HostMigrationResume".
    StartGameResult result = await newRunner.StartGame(new StartGameArgs() {
      // SessionName = SessionName,              // ignored, peer never disconnects from the Photon Cloud
      // GameMode = gameMode,                    // ignored, Game Mode comes with the HostMigrationToken
      HostMigrationToken = hostMigrationToken,   // contains all necessary info to restart the Runner
      HostMigrationResume = HostMigrationResume, // this will be invoked to resume the simulation
      // other args
    });

    // Check StartGameResult as usual
    if (result.Ok == false) {
      Debug.LogWarning(result.ShutdownReason);
    } else {
      Debug.Log("Done");
    }
  }
```

--------------------------------

### Join a Random Session with Filters

Source: https://doc.photonengine.com/fusion/current/manual/connection-and-matchmaking/matchmaking

Start a client that joins a random session matching specific criteria, such as a particular game type. This uses the custom properties defined when the session was created.

```csharp
public async Task StartClient(NetworkRunner runner, GameType gameType) {

  var customProps = new Dictionary<string, SessionProperty>() {
    { "type", (int)gameType }
  };

  var result = await runner.StartGame(new StartGameArgs() {
    GameMode = GameMode.Client,
    SessionProperties = customProps,
  });

  if (result.Ok) {
    // all good
  } else {
    Debug.LogError($"Failed to Start: {result.ShutdownReason}");
  }
}
```

--------------------------------

### Dynamically Listen to a Group

Source: https://doc.photonengine.com/fusion/current/industries-samples/industries-addons/fusion-industries-addons-dynamic-audio-group

Asynchronously start listening to a specific audio group. Ensures the Photon Voice client is ready before attempting to change groups.

```csharp
async void ListentoGroupId(byte groupId)
{
    if (!Object.HasStateAuthority) return;
    while (!IsPhotonVoiceready || !isSpeakInitialized) await Task.Delay(10);
    fusionVoiceClient.Client.OpChangeGroups(groupsToRemove: null, groupsToAdd: new byte[] { groupId });
}
```

--------------------------------

### Example 5: Projectile Data Buffer - Kinematic

Source: https://doc.photonengine.com/fusion/current/technical-samples/projectiles-essentials

Implements a kinematic projectile using a data buffer. This approach simulates projectiles that move over time but do not rely on full physics simulation for their trajectory, offering a balance between performance and visual accuracy.

```csharp
public class KinematicProjectile : NetworkBehaviour
{
    [Networked]
    public Vector3 Direction { get; set; }
    [Networked]
    public float Speed { get; set; }

    public void Initialize(Vector3 direction, float speed)
    {
        Direction = direction.normalized;
        Speed = speed;
    }

    public override void FixedUpdateNetwork()
    {
        transform.position += Direction * Speed * Runner.DeltaTime;

        // Add logic for collision detection and despawning
        if (transform.position.magnitude > 1000f) // Example despawn condition
        {
            Runner.Despawn(Object);
        }
    }

    // ... other methods
}
```

--------------------------------

### Configure Custom STUN Server in Photon Fusion

Source: https://doc.photonengine.com/fusion/current/reference/stun-servers

Pass a custom STUN server address to the NetworkRunner when starting a game. This is useful if the default Google STUN servers are unavailable or if you prefer to use a specific server.

```csharp
// When starting the NetworkRunner
runner.StartGame(new StartGameArgs {
  // ...
  // Pass a custom STUN Server
  CustomSTUNServer = "stun1.l.google.com:19302"
});
```

--------------------------------

### Networked TickTimer for Game Start

Source: https://doc.photonengine.com/fusion/current/game-samples/fusion-razor-madness

Utilizes a Networked TickTimer to manage the countdown before the game starts. Ensures the timer is synchronized across all network participants.

```csharp
[Networked]
private TickTimer StartTimer { get; set; }

private void SetLevelStartValues()
{
    //...
    StartTimer = TickTimer.CreateFromSeconds(Runner, 5);
}
```

--------------------------------

### Control State Machine in FixedUpdateNetwork

Source: https://doc.photonengine.com/fusion/current/addons/fsm

Control state machine transitions from `FixedUpdateNetwork`. This example shows how to activate the attack state based on network conditions.

```csharp
using Fusion;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachineController))]
public class EnemyAI : NetworkBehaviour, IStateMachineOwner
{
    [SerializeField]
    private IdleBehaviour _idleState;
    [SerializeField]
    private AttackBehaviour _attackState;

    private StateMachine<StateBehaviour> _enemyAI;

    void IStateMachineOwner.CollectStateMachines(List<IStateMachine> stateMachines)
    {
        _enemyAI = new StateMachine<StateBehaviour>("Enemy AI", _idleState, _attackState);

        stateMachines.Add(_enemyAI);
    }

    public override void FixedUpdateNetwork()
    {
        _enemyAI.TryActivateState(_attackState);
    }
}
```

--------------------------------

### NetworkArray Get and Set Operations

Source: https://doc.photonengine.com/fusion/current/manual/fusion-types/network-collections

Demonstrates using `Set()` and `Get()` methods for modifying and accessing elements in a `NetworkArray<NetworkString<_32>>`. Direct index assignment like `NetArray[3] = "Three";` is not supported for NetworkArray properties.

```csharp
public class CollectionTestsArray : NetworkBehaviour
{
  [Networked]
  [Capacity(4)] // Sets the fixed capacity of the collection
  NetworkArray<NetworkString<_32>> NetArray { get; }

    = MakeInitializer(new NetworkString<_32>[] { "#0", "#1", "#2", "#3" });

  public override void FixedUpdateNetwork()
  {
    NetArray.Set(0, "Zero");
    NetArray.Set(1, "One");
    NetArray.Set(2, "Two");

    // This is invalid with a NetworkDictionary property, use Set() instead.
    // NetArray[3] = "Three";

    NetArray.Set(0, NetArray[0].ToUpper());
    NetArray.Set(1, NetArray.Get(1).ToLower());
    NetArray.Set(2, default);

    for (int i = 0; i < NetArray.Length; ++i)
    {
      Debug.Log($"{i}: '{NetArray[i]}''");
    }
  }
}
```

--------------------------------

### Full PlayerInput Class Example

Source: https://doc.photonengine.com/fusion/current/manual/data-transfer/player-input

This class accumulates all player input in Update and passes it to Fusion in OnInput. It handles look rotation, movement direction, and button presses. Ensure HasInputAuthority is checked before processing input.

```csharp
public sealed class PlayerInput : NetworkBehaviour, INetworkRunnerCallbacks
{
    public Vector2 LookRotation => _input.LookRotation;

    private GameplayInput _input;

    public override void Spawned() => Runner?.AddCallbacks(this);
    public override void Despawned(NetworkRunner runner, bool hasState) => runner?.RemoveCallbacks(this);

    private void Update()
    {
        if (HasInputAuthority == false) return;

        var lookRotationDelta = new Vector2(
            -Mouse.current.delta.y.ReadValue(),
             Mouse.current.delta.x.ReadValue());

        _input.LookRotation = ClampLookRotation(
            _input.LookRotation + lookRotationDelta);

        var keyboard = Keyboard.current;
        var moveDirection = Vector2.zero;
        if (keyboard.wKey.isPressed) moveDirection.y += 1f;
        if (keyboard.sKey.isPressed) moveDirection.y -= 1f;
        if (keyboard.aKey.isPressed) moveDirection.x -= 1f;
        if (keyboard.dKey.isPressed) moveDirection.x += 1f;

        _input.MoveDirection = moveDirection.normalized;

        _input.Buttons.Set(EInputButton.Jump, keyboard.spaceKey.isPressed);
        _input.Buttons.Set(EInputButton.Sprint, keyboard.leftShiftKey.isPressed);
    }

    public void OnInput(NetworkRunner runner, NetworkInput networkInput)
    {
        networkInput.Set(_input);
    }

    private Vector2 ClampLookRotation(Vector2 lookRotation)
    {
        lookRotation.x = Mathf.Clamp(lookRotation.x, -30f, 70f);
        return lookRotation;
    }
}
```

--------------------------------

### Connect to a Fusion Session

Source: https://doc.photonengine.com/fusion/current/technical-samples/fusion-vr-host

Handles the connection logic for starting or joining a Fusion session. Ensure the `Connection Manager` game object has 'Auto Host or Client' selected, or a specific role like 'Host' or 'Client'.

```csharp
private async void Start()
{
    // Launch the connection at start
    if (connectOnStart) await Connect();
}

public async Task Connect()
{
    // Create the scene manager if it does not exist
    if (sceneManager == null) sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();
    if (onWillConnect != null) onWillConnect.Invoke();

    // Start or join (depends on gamemode) a session with a specific name
    var args = new StartGameArgs()
    {
        GameMode = GameMode.Shared,
        Scene = CurrentSceneInfo(),
        SceneManager = sceneManager
    };

    // Connexion criteria (note: actual project code contains alternative options)
    args.SessionName = roomName;

    await runner.StartGame(args);
}

public virtual NetworkSceneInfo CurrentSceneInfo()
{
    var activeScene = SceneManager.GetActiveScene();
    SceneRef sceneRef = default;

    if (activeScene.buildIndex < 0 || activeScene.buildIndex >= SceneManager.sceneCountInBuildSettings)
    {
        Debug.LogError("Current scene is not part of the build settings");
    }
    else
    {
        sceneRef = SceneRef.FromIndex(activeScene.buildIndex);
    }

    var sceneInfo = new NetworkSceneInfo();
    if (sceneRef.IsValid)
    {
        sceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Single);
    }
    return sceneInfo;
}
```