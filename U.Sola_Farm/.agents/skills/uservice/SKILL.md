# Services (Infraestructura Global)

---
name: uservice
description: - Vive toda la vida del juego, es reutilizable por múltiples Features, no pertenece a una Feature específica
---


Ejemplos:

- AudioService
- SaveService
- NetworkService
- PoolService
- SceneLoaderService

- **Persistencia**: No debe depender de objetos específicos de una escena, no deben heredar de monobehavioure.
- **Single Responsibility**: Un servicio solo hace una cosa (ej. `SaveService` solo guarda/carga).
---