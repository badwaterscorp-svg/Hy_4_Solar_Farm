## 📂 Estructura del Proyecto (Asset Mapping)
---
name: ufolder
description: Es obligatorio seguir esta jerarquía para mantener el orden. La IA debe sugerir estas rutas al crear nuevos archivos.

---

###  Art (Arte y Visuales)
* **Assets/Art/3D/**: 
    * `Models/`: Mallas y geometrías (FBX, OBJ).
    * `Materials/`: Materiales y archivos .mat.

* **Assets/Art/2D/**:
    * `UI/`: Elementos de interfaz, iconos y botones.
    * `Sprites/`: Elementos de juego 2D y personajes.
    * `Texture/`: Texturas para modelos 3D o terrenos.

### Code (Lógica y Programación)
* **Assets/Code/Scripts/**:
    * `Features/`: Lógica específica por mecánica (Ej: Player, Combat, Inventory).
    * `Services/`: Sistemas globales o Singletons (Ej: AudioManager, NetworkManager, DataManager).
    * `Utils/`: Helpers, constantes y extensiones de C#.
    * `Installers/`: Configuraciones de Inyección de Dependencias (Zenject).
* **Assets/Code/SO/**: Instancias de ScriptableObjects (Datos y Configuraciones).
* **Assets/Code/Shaders/**: HLSL, Shader Graphs y Subgraphs.

### Level (Diseño de Niveles)
* **Assets/Level/Prefabs/**: Versiones reutilizables de objetos de juego.
* **Assets/Level/Scenes/**: Archivos de escena (.unity).

### Otros
* **Assets/ExternalAssets/**: Plugins de terceros y paquetes de la Asset Store. (Prohibido modificar este código).
* **Assets/Resources/**: Solo para carga dinámica estrictamente necesaria.
* **Assets/StreamingAssets/**: Archivos binarios o videos de carga directa.

---

## Reglas de Comportamiento para la IA
1. **Ubicación**: Antes de crear un script, verifica en qué subcarpeta de `Code/Scripts/` encaja mejor.
2. **Contexto**: Si el script requiere datos persistentes, sugiere crear un ScriptableObject en `Code/SO/`.
3. **Organización**: No crees carpetas nuevas en la raíz de `Assets/` sin preguntar.
4. **Arquitectura**: Prioriza el desacoplamiento. Los scripts en `Features/` no deben conocerse entre sí directamente; usar `Services/` o eventos

---
