## 1 Create Folders

**Do**
- utiliza el skill ufolder crea las carpetas de mi proyecto de unity
**Requeriment []**

**Criterio Aceptacion**

## Human Acceptance

**Estado: Implementado**

Carpetas creadas siguiendo la convencion `Assets/Code/Scripts/{Features,Services,Utils}` y `Assets/Code/SO/{Dialogs,Resources}`:

- `Assets/Code/Scripts/Features/`
  - `BackPack/`, `Building/`, `Collector/`, `Dialog/`, `Missions/`, `Player/`, `Resources/`, `Spawner/`, `Terrain/`
- `Assets/Code/Scripts/Services/`
  - `GameMediator/`, `Interfaces/`, `ObjectPooling/`
- `Assets/Code/SO/Dialogs/` (con `DailogTest.asset`)
- `Assets/Code/SO/Resources/` (con `Food.asset`, `SolarEnergy.asset`, `Water.asset`)

**Pendiente:**
- `Utils/` existe pero esta vacia.
- No existe `Assets/Code/Scripts/Installers/` con contenido (la instalacion se hace via `GameBindingInstaller.cs` en `Assets/Code/`).