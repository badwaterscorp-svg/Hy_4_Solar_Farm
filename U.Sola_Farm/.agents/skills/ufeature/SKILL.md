# 1️⃣ Features (Módulos Funcionales)

Una Feature es un módulo autocontenido que representa una funcionalidad del juego.
Ejemplo:

```
Features/Boss/
    BossHandler.cs
    BossModel.cs
    BossMovement.cs
    BossAttack.cs
    BossHealth.cs
```

## Regla clave

Una Feature debe poder entenderse y modificarse sin tocar otras partes del sistema.
**Comunicación**:
    Cada Feature debe vivir en su propia carpeta dentro de `Features/`:
    `Features/[Nombre]/`.

**Desacoplamiento de Unity**:
    - `Manager`, `Handler`, `EventListener`, `Displayer`, `Sheet`.
    - El `Displayer` suele estar, contener, o comunicarse con objecto de la skill uui
**Comunicación**:
    - El `Handler` avisa a lógica específica.
    - La lógica específica (como `BossAttack`) obtiene datos del `Model`

---

##  Ejemplo: Boss Feature

### BossModel (Estado puro)
Mi modelo no usa constructor, solo se utiliza para mostrar y obtener los datos.
En el projecto ya existe una interfaz que se llama ICopy para obtener una copia de mi modelo

```csharp
[System.Serializable]
public class BossModel: ICopy<BossModel>
{
    public float Health;
    public float MoveSpeed;

    public BossModel Copy()
    {
        return (BossModel)this.MemberwiseClone();
    }
}
```

---

### BossHandler (Instancia Especifica)

```csharp
public class BossHandler:Monobehaviour
{
    private BossMovement _movement;
    private BossAttack _attack;
    private BossHealth _health;
    private BossModel _model

    private void Start()
    {
        _movement = new BossMovement();
        _attack = new BossAttack();
        _health = new BossHealth();
    }

    public void Update()
    {
        _movement.Move(Vector3.forward);
    }

    public void Attack()
    {
        _attack.Shoot();
    }

    public void Configure(BossSheet sheet)
    {
        _model = sheet.model;
    }
}
```

---

### BossManager (Orquestador)

Maneja configura o escucha varios handlers, esto es ideal en logica que tiene que comunicarse con varios, o elegir cual es el `Handler` actual


```csharp
public class BossManager:Singleton<BossManager>
{
    private BossHandler[] handlers;
    private BossSheet bossData;

    private void Start()
    {
        foreach(var handler in handlers)
        {
            handler.Configure(bossData);
            handler.OnShot += Listen
        }
    }

    private void Listen()
    {
        Console.Log("Shot");
    }
}
```


### BossSheet (Data)

es un scriptable object y suele estar en una lista del manager e injectarse al handler en la configuración

```csharp
public class BossSheet:ScriptableObject
{
    [Serializedfield] private Sprite iconBoss;
    public Sprite IconBoss => iconBoss;
    [Serializedfield] private BossModel model;
    public BossModel ModelCopy =>model.Copy();
    
}
```