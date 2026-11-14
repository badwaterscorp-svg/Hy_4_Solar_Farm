# 1️⃣ Sublógica (Módulos Funcionales)

Una Sublógica es un módulo autocontenido que representa una funcionalidad del juego.

## Ejemplo de Estructura

```
Features/Boss/
    BossHandler.cs
    BossModel.cs
    BossMovement.cs
    BossAttack.cs
    BossHealth.cs
```

---

## Regla Clave

**Segregación**: Si el código es demasiado largo o hay un script con varias funcionalidades, se aplicará el **Principio de Responsabilidad Única**.

---

## Comunicación

- Las Sublógicas modifican a los features en `Features/`
- `Features/[Nombre]/Subclases` → Contiene Model, Handler y sub-lógicas.

---

## Desacoplamiento de Unity

- `Model`, `Handler` y lógicas específicas (como `BossAttack`) **NO** deben heredar de `MonoBehaviour`.
- Solo la clase `Handler` interactúa con el motor de Unity, por lo tanto es `MonoBehaviour`.

**Comunicación**:
- El `Handler` notifica a la lógica específica.
- La lógica específica (como `BossAttack`) obtiene datos del `Model`.

---

## Ejemplo: Boss Feature

### BossModel (Estado Puro)

Mi modelo no usa constructor, solo se utiliza para mostrar y obtener los datos.
En el proyecto ya existe una interfaz que se llama `ICopy` para obtener una copia de mi modelo.

```csharp
[System.Serializable]
public class BossModel : ICopy<BossModel>
{
    public float Health;
    public float MoveSpeed;

    public BossModel Copy()
    {
        return (BossModel)this.MemberwiseClone();
    }
}
```
en cierto casos el model es un struct y no una class, y no heredaria de ICopy


### BossMovement (Lógica Específica)

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
    private BossHandler _handler;

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

### BossHandler (Orquestador)

```csharp
public class BossHandler : MonoBehaviour
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

    private void Update()
    {
        _movement.Move(Vector3.forward);
    }

    public void Attack()
    {
        _attack.Shoot();
    }
}
```