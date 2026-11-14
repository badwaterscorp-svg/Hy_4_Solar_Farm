## 15 Animacion de dialogo

**Do**
Quiero implementar una logica de dialogos con la que ya tengo hecha

**Requirements**
- analiza los scripts de la carpeta Assets/ExternalAssets/Anima DG Control/Scripts/PerCharacters
- usa ufeature para hacer un panel de dialogo

## Human Acceptance

**Estado: Implementado**

Archivos relacionados:
- `Assets/ExternalAssets/Anima DG Control/Scripts/PerCharacters/ITypingAnimaStrategy.cs` - Interfaz de estrategias de animacion de tipeo.
- `Assets/ExternalAssets/Anima DG Control/Scripts/PerCharacters/Strategies/` - Estrategias concretas: `AnimCharDisplayByFall`, `AnimCharHorizon`, `AnimCharRandomPos`, `AnimCharRise`, `AnimCharScaleFade`.
- `Assets/ExternalAssets/Anima DG Control/Scripts/PerCharacters/DemoDialogAnimation.cs` y `DemoDialogManager.cs` - Demos existentes.
- `Assets/ExternalAssets/Anima DG Control/Scripts/AnimationUIController.cs` - Controlador de animaciones de UI usado por el BackPack.
- `Assets/Code/Scripts/Features/Dialog/DialogAnimation.cs` - Wrapper de la animacion de dialogo con TMP y `DOTweenTMPAnimator`. Soporta `ITypingAnimaStrategy` para los efectos.
- `Assets/Code/Scripts/Features/Dialog/DialogHandler.cs` - Manejador del panel de dialogo. Implementa:
  - `StartDialog(DialogSheet)` - Inicia el dialogo.
  - `Next()` - Avanza a la siguiente linea.
  - `ShowFullText()` - Muestra la linea completa sin animacion.
  - `OnDialogStarted`, `OnDialogFinished`, `OnLineChanged` eventos.
  - Usa `DOText` para animar la aparicion del texto.
- `Assets/Code/Scripts/Features/Dialog/DialogManager.cs` - Punto de entrada que une `DialogHandler` con `DialogSheet` y muestra/oculta el `containerDialog`.
- `Assets/Code/Scripts/Features/Dialog/DialogSheet.cs` - ScriptableObject con `sptUser` (sprite) y `List<DialogLine>` (speakerName, text, animationStrategy).
- `Assets/Code/SO/Dialogs/DailogTest.asset` - Dialogo de prueba.
- `Assets/Level/Prefabs/Prefabs_UI/CanvasDialog.prefab` - Prefab del panel UI de dialogo.

**Verificaciones pendientes del Humano:**
- Conectar `DialogManager.Do()` al evento de input que corresponda (click/tap) para alternar entre `StartDialog`/`Next`/`ShowFullText`.
- En `DialogHandler` el campo `defaultStrategy = "AnimCharScaleFade"` se declara pero no se usa (la animacion se hace con `DOTweenTMPAnimator` en `DialogAnimation`); decidir si se elimina o si se cablea a las estrategias de `ITypingAnimaStrategy`.
- Poblar `DailogTest.asset` con las lineas de prueba y verificar visualmente las animaciones de cada strategy.