## 📂 Estructura del Proyecto (Asset Mapping)
---
name: ufolder
description: Es obligatorio seguir esta jerarquía para mantener el orden. La IA debe sugerir estas rutas al crear nuevos archivos.

---

### 🎨 Art (Arte y Visuales)

* **Assets/Art/3D/**:
    * `Models/`: Mallas y geometrías (FBX, OBJ).
    * `Materials/`: Materiales y archivos .mat.

* **Assets/Art/2D/**:
    * `UI/`: Elementos de interfaz, iconos y botones.
    * `Sprites/`: Elementos de juego 2D y personajes.
    * `Texture/`: Texturas para modelos 3D o terrenos.

---
### 🎵 Audio (Sonido y Música)

* **Assets/Audio/Music/**:
    * `BGM/`: Música de fondo del juego (loop principal, temas de menú).
    * `Tracks/`: Pistas musicales específicas por nivel o escenario.
    * `Stingers/`: Transiciones musicales y stingers de eventos.

* **Assets/Audio/Sounds/**:
    * `SFX/`: Efectos de sonido del juego (disparos, pasos, impactos).
    * `UI/`: Sonidos de interfaz (clicks, hover, confirmaciones).
    * `Ambient/`: Sonidos ambientales (lluvia, viento, multitudes).

* **Assets/Audio/Dialogs/**:
    * `VO/`: Voz en off de personajes (diálogos principales).
    * `NPC/`: Diálogos de personajes no jugables.
    * `Cutscenes/`: Audio de cinemáticas y escenas narrativas.

---

### 🎬 Animations (Animaciones)

* **Assets/Animations/**:
    * `Characters/`: Animaciones de personajes jugables y NPCS.
        * `Player/`: Animaciones del jugador (idle, walk, run, jump, attack).
        * `Enemies/`: Animaciones de enemigos (idle, patrol, chase, attack, death).
        * `NPCs/`: Animaciones de personajes no jugables (idle, talk, walk).
    * `UI/`: Animaciones de elementos de interfaz (botones, menús, transiciones).
    * `Environment/`: Animaciones de entorno (puertas, plataformas móviles, trampas).
    * `VFX/`: Efectos visuales animados (partículas, explosiones, glows).
    * `Cutscenes/`: Animaciones de cinemáticas y escenas narrativas.

---

### 💻 Code (Lógica y Programación)

* **Assets/Code/Scripts/**:
    * `Features/`: Lógica específica por mecánica (Ej: Player, Combat, Inventory).
    * `Services/`: Sistemas globales o Singletons (Ej: AudioManager, NetworkManager, DataManager).
    * `Utils/`: Helpers, constantes y extensiones de C#.
    * `Installers/`: Configuraciones de Inyección de Dependencias (Zenject).

* **Assets/Code/SO/**: Instancias de ScriptableObjects (Datos y Configuraciones).

* **Assets/Code/Shaders/**: HLSL, Shader Graphs y Subgraphs.

---

### 🗺️ Level (Diseño de Niveles)

* **Assets/Level/Prefabs/**: Versiones reutilizables de objetos de juego.

* **Assets/Level/Scenes/**: Archivos de escena (.unity).

---

### 📦 Otros

* **Assets/ExternalAssets/**: Plugins de terceros y paquetes de la Asset Store. (Prohibido modificar este código).

* **Assets/Resources/**: Solo para carga dinámica estrictamente necesaria.

* **Assets/StreamingAssets/**: Archivos binarios o videos de carga directa.

---



## 2️⃣ Archivo .gitignore para Unity

### Ubicación Obligatoria

El archivo `.gitignore` debe estar en la **raíz del proyecto**, junto a la carpeta `Assets/`.

```
ProjectRoot/
├── .gitignore          # ← AQUÍ
├── Assets/
├── ProjectSettings/
├── Packages/
└── UserSettings/
```

### Contenido del .gitignore

```gitignore
# =============================================================================
# UNITY GITIGNORE - Optimizado para proyectos Unity
# =============================================================================

# --- Unity Engine ---
[Ll]ibrary/
[Tt]emp/
[Oo]bj/
[Bb]uild/
[Bb]uilds/
[Ll]ogs/
[Uu]ser[Ss]ettings/

# --- Remover excepto archivos .gitkeep ---
[Uu]ser[Ss]ettings/

# --- Remover excepto archivos necesarios ---
ProjectSettings/

# --- Remover excepto archivos necesarios ---
Packages/

# --- Visual Studio / VS Code ---
.vs/
.vscode/
*.sln
*.suo
*.user
*.userprefs
*.pidb
*.userosscache
*.sln.docstates

# --- JetBrains Rider ---
.idea/
*.resharper
*.resharper.user

# --- Mac OS ---
.DS_Store
.AppleDouble
.LSOverride
._*

# --- Build Results ---
[Tt]est[Rr]esult/
[Bb]uild/
[Ll]og/

# --- NuGet ---
*.nupkg
*.snupkg
.nuget/
packages/
project.lock.json
project.fragment.lock.json

# --- Gradle ---
.gradle/

# --- Crashlytics ---
crashlytics-build.properties

# --- Credentials ---
*.key
*.pem
*.pfx
*.p12
credentials.json
secrets.json

# --- Unity Generated ---
ExportedObj/
*.app
*.apk
*.aab
*.unitypackage
*.jar
*.keystore

# --- Temporary Files ---
*.tmp
*.bak
*.swp
*~

# --- IDE Specific ---
*.orig
*.rej
*.orig.*
*.rej.*

# --- TextMate ---
*.tmpl
*.tmLanguage.osc
*.tmPreferences

# --- Sublime Text ---
*.sublime-*

# --- Backup Files ---
*.bak
*.backup
```

---

## 3️⃣ Reglas de Comportamiento para la IA

### Al Crear un Nuevo Proyecto Unity

1. **Verificar existencia del .gitignore**:
   - Si el proyecto NO tiene `.gitignore` en la raíz, crear uno nuevo con el contenido estándar.

2. **Estructura inicial**:
   ```
   ProjectRoot/
   ├── Assets/
   │   ├── Art/
   │   │   ├── 3D/
   │   │   └── 2D/
   │   ├── Audio/
   │   │   ├── Music/
   │   │   ├── Sounds/
   │   │   └── Dialogs/
   │   ├── Animations/
   │   │   ├── Characters/
   │   │   ├── UI/
   │   │   └── Environment/
   │   ├── Code/
   │   │   ├── Scripts/
   │   │   └── SO/
   │   └── Level/
   ├── ProjectSettings/
   ├── Packages/
   ├── UserSettings/
   └── .gitignore
   ```

3. **Inicializar repositorio Git**:
   - Ejecutar `git init` en la raíz del proyecto.
   - Asegurar que `.gitignore` esté incluido.

### Al Crear Archivos

1. **Ubicación**: Antes de crear un script, verificar en qué subcarpeta de `Code/Scripts/` encaja mejor.

2. **Contexto**: Si el script requiere datos persistentes, sugerir crear un ScriptableObject en `Code/SO/`.

3. **Organización**: No crear carpetas nuevas en la raíz de `Assets/` sin preguntar.

4. **Arquitectura**: Priorizar el desacoplamiento. Los scripts en `Features/` no deben conocerse entre sí directamente; usar eventos o umediato+r.

---

## 4️⃣ Checklist de Configuración de Proyecto

```
□ Crear .gitignore en la raíz del proyecto A lado de Assets
□ Crear estructura de carpetas Assets/
□ Inicializar repositorio Git
□ Configurar .gitattributes (opcional)
□ Ignorar archivos de usuario de Unity
□ Proteger credenciales sensibles
□ Documentar excepciones específicas del proyecto
```

---


## 5️⃣ Ejemplo de Flujo de Trabajo

```
1. Usuario: "Crear un nuevo proyecto Unity"

2. IA responde:
   - Crear estructura básica
   - Generar .gitignore en raíz
   - Crear carpetas de Audio y Animations
   - Sugerir estructura de Assets/

3. Usuario: "Crear script de jugador"

4. IA responde:
   - Proponer ubicación: Assets/Code/Scripts/Features/Player/
   - Sugerir archivos:
     ├── PlayerHandler.cs
     ├── PlayerModel.cs
     └── PlayerMovement.cs

5. Usuario: "Agregar animación de muerte del boss"

6. IA responde:
   - Ubicación sugerida: Assets/Animations/Characters/Enemies/
   - Archivos:
     ├── BossDeath.anim
     └── BossAnimatorController.controller
```


## Recursos adicionales
- [Unity.gitignore oficial](https://github.com/github/gitignore/blob/main/Unity.gitignore)
- [Gitignore.io - Unity](https://www.toptal.com/developers/gitignore/api/unity)
- [Best Practices Unity Git](https://docs.unity3d.com/Manual/Git.html)
- [Audio Best Practices Unity](https://docs.unity3d.com/Manual/Audio.html)
- [Animation System Unity](https://docs.unity3d.com/Manual/AnimationOverview.html)