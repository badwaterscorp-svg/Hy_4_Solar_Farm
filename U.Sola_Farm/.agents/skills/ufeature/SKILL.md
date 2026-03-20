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
    `Features/[Nombre]/` -> Contiene Model, Controller, View, sub-lógicas e Installer.

**Desacoplamiento de Unity**:
    - `Model`, `Handler` y lógicas específicas (como `BossAttack`) **NO** deben heredar de `MonoBehaviour`.
    - Solo la `Handler` interactúa con el motor de Unity.
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

### BossMovement (Lógica específica)

```csharp
public class BossMovement
{
    private BossModel _model;
    private Transform _transform;

    public BossMovement(BossModel model, Transform transform)
    {
        _model = model;
        _transform = transform;
    }

    public void Move(Vector3 direction)
    {
        _transform.position += direction * _model.MoveSpeed * Time.deltaTime;
    }
}
```

### BossAttack

```csharp
public class BossAttack
{
    private IAudioService _audioService;
    private IPoolService<Bullet> _bulletPool;
    private BossHandler handler;

    public BossAttack(
        IAudioService audioService,
        IPoolService<Bullet> bulletPool
        )
    {
        _audioService = audioService;
        _bulletPool = bulletPool;
    }

    public void Shoot()
    {
        var bullet = _bulletPool.Get();
        _audioService.Play("boss_shoot");
    }
}
```

---

### BossHandler (Orquestador)

```csharp
public class BossHandler:Monobehaviour
{
    private BossMovement _movement;
    private BossAttack _attack;
    private BossHealth _health;

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
}
```