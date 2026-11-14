# 1️⃣ Caller (Bridge entre Scripts)

Un Caller hace de puente entre un scirpt monobehabior con datos esclusivos del editor de unity a un singleton o a otro script específico

## Ejemplo de Estructura

en utils si no hay una lógica muy extensa, pero si existe Feature específico

```
Utils/
    TeleportCaller.cs

or

Features/
    Teleport/
        TeleportFadeDisplayer.cs
        TeleportManager.cs
        TeleportCaller.cs
```

---

## Regla Clave

**Inicio**: Caller es el script principio, tiene el sufijo caller.
**Puente**: metodo para enviar y un metodo para recibir.
**Destino**: Es un script que tiene un metodo que recibe el dato enviado por el caller.

---

## Ejemplo: SaveCaller

### SaveCaller (Inicio)

```csharp

public class SaveCaller : Monobehaviour
{
    [SerializedField] private int Llvel;
    [SerializedField] private int stage;

    public void CallSaveLevel()
    {
        SaveManager.Instance.SaveStage(level,stage);
    }
}
```
en cierto casos el model es un struct y no una class, y no heredaria de ICopy


### SaveManager (Lógica Destino)

```csharp
public class SaveManager:Singleton<SaveManager>
{

    public SaveStage(int level,int stage)
    {
        print("Stage Saved");
    }
}
```