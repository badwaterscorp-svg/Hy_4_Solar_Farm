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

- **Inyección**: Siempre solicita la `interface` en el constructor de la Feature.
- **Persistencia**: No debe depender de objetos específicos de una escena.
- **Single Responsibility**: Un servicio solo hace una cosa (ej. `SaveService` solo guarda/carga).
---

##  Ejemplo: AudioService

```csharp
public interface IAudioService
{
    void Play(string id);
}

public class AudioService : IAudioService
{
    public void Play(string id)
    {
        Debug.Log("Playing sound: " + id);
    }
}
```

---

##  Ejemplo: PoolService

```csharp
public interface IPoolService<T>
{
    T Get();
    void Release(T obj);
}
```

El pool pertenece a Core, no a una Feature específica.

---

#  Infrastructure (Adaptadores externos)

Infrastructure conecta sistemas externos con tu arquitectura.

Ejemplos:

- PhotonAdapter
- FirebaseAdapter
- AnalyticsAdapter

---

## 🌐 Ejemplo: Network Adapter

```csharp
public interface INetworkService
{
    void Connect();
}

public class PhotonAdapter : INetworkService
{
    public void Connect()
    {
        // Lógica específica de Photon
    }
}
```

La Feature nunca habla directamente con Photon.
Habla con INetworkService.

---