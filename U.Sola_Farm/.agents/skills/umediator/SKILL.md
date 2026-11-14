# 🧩 Patrón Mediator con Contexto de Estado

## 1️⃣ Concepto General

El **Patrón Mediator** es un patrón de diseño conductual que facilita la comunicación entre objetos, reduciendo el acoplamiento directo entre ellos. En Unity, este patrón es especialmente útil para gestionar la comunicación entre sistemas del juego sin crear dependencias complejas.

### Tu Implementación: GameStateMediator

Tu código implementa una variante del Mediator combinada con gestión de estado centralizada:

```csharp
public partial class GameStateContext
{
    // Clase interna que actúa como mediador
    public class GameStateMediator
    {
        private static Dictionary<GameEventType, UnityEvent> dictionary =
            new Dictionary<GameEventType, UnityEvent>();

        // Métodos para suscribir, desuscribir y publicar eventos
    }
}

// Gestión de estado centralizado
public partial class GameStateContext
{
    public static GameEventType State { get; private set; }
    public static void ChangeState(GameEventType newState) { ... }
}
```

---

## 2️⃣ Estructura del Patrón

```
┌─────────────────────────────────────────────────────────────────┐
│                      GameStateContext                           │
│                                                                 │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │                   GameStateMediator                       │  │
│  │  Dictionary<GameEventType, UnityEvent>                    │  │
│  │  ├── Subscribe(event, action)                             │  │
│  │  ├── Unsubscribe(event, action)                           │  │
│  │  └── Publish(event)                                       │  │
│  └───────────────────────────────────────────────────────────┘  │
│                                                                 │
│  State Management:                                              │
│  ├── State: GameEventType                                       │  │
│  └── ChangeState(newState) → Publish(newState)                 │
└─────────────────────────────────────────────────────────────────┘
                              │
        ┌─────────────────────┼─────────────────────┐
        │                     │                     │
        ▼                     ▼                     ▼
   ┌─────────┐          ┌─────────┐          ┌─────────┐
   │Component│          │Component│          │Component│
   │    A    │          │    B    │          │    C    │
   └─────────┘          └─────────┘          └─────────┘
```

---

## 3️⃣ Flujo de Comunicación

```
1. Suscripción:
   Component A ──Subscribe(GameEventType.EnemyKilled, OnEnemyKilled)──▶ GameStateMediator
   Component B ──Subscribe(GameEventType.EnemyKilled, OnEnemyKilled)──▶ GameStateMediator

2. Publicación:
   Component C ──ChangeState(GameEventType.EnemyKilled)──▶ GameStateMediator.Publish()

3. Notificación:
   GameStateMediator ──Invoke()──▶ OnEnemyKilled() en Component A
   GameStateMediator ──Invoke()──▶ OnEnemyKilled() en Component B
```

---

## 4️⃣ Código Completo de Referencia

### GameStateContext.cs

```csharp
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class GameStateContext
{
    public class GameStateMediator
    {
        private static Dictionary<GameEventType, UnityEvent> dictionary =
            new Dictionary<GameEventType, UnityEvent>();

        /// <summary>
        /// Suscribe una acción a un tipo de evento específico.
        /// Si el evento no existe en el diccionario, lo crea.
        /// </summary>
        public static void Subscribe(GameEventType _event, UnityAction action)
        {
            if (dictionary.TryGetValue(_event, out var collection))
            {
                collection.AddListener(action);
            }
            else
            {
                var thisEvent = new UnityEvent();
                thisEvent.AddListener(action);
                dictionary.Add(_event, thisEvent);
            }
        }

        /// <summary>
        /// Desuscribe una acción de un tipo de evento específico.
        /// </summary>
        public static void Unsubscribe(GameEventType _event, UnityAction action)
        {
            if (dictionary.TryGetValue(_event, out var collection))
            {
                collection.RemoveListener(action);
            }
        }

        /// <summary>
        /// Publica un evento, notificando a todos los suscriptores.
        /// </summary>
        internal static void Publish(GameEventType type)
        {
            if (dictionary.TryGetValue(type, out var collection))
            {
                collection.Invoke();
                Debug.Log("Event Published: " + type);
            }
        }
    }
}

public partial class GameStateContext
{
    /// <summary>
    /// Estado actual del juego.
    /// </summary>
    public static GameEventType State { get; private set; }

    /// <summary>
    /// Cambia el estado del juego y publica el evento correspondiente.
    /// </summary>
    public static void ChangeState(GameEventType newState)
    {
        State = newState;
        GameStateMediator.Publish(newState);
    }
}
```

### GameEventType (Enumeración)

```csharp
public enum GameEventType
{
    None,
    GameStart,
    GamePause,
    GameResume,
    GameOver,
    LevelComplete,
    PlayerDeath,
    EnemyKilled,
    BossSpawned,
    BossDefeated,
    CollectiblePickedUp,
    CheckpointReached,
    SaveGame,
    LoadGame
}
```

---

## 5️⃣ Ejemplos de Uso

### Suscripción y Desuscripción

```csharp
public class ScoreManager : MonoBehaviour
{
    private int _score = 0;

    private void OnEnable()
    {
        GameStateContext.GameStateMediator.Subscribe(
            GameEventType.EnemyKilled,
            OnEnemyKilled
        );
        GameStateContext.GameStateMediator.Subscribe(
            GameEventType.CollectiblePickedUp,
            OnCollectiblePickedUp
        );
    }

    private void OnDisable()
    {
        GameStateContext.GameStateMediator.Unsubscribe(
            GameEventType.EnemyKilled,
            OnEnemyKilled
        );
        GameStateContext.GameStateMediator.Unsubscribe(
            GameEventType.CollectiblePickedUp,
            OnCollectiblePickedUp
        );
    }

    private void OnEnemyKilled()
    {
        _score += 100;
        Debug.Log("Score: " + _score);
    }

    private void OnCollectiblePickedUp()
    {
        _score += 10;
        Debug.Log("Score: " + _score);
    }
}
```

### Publicación de Eventos

```csharp
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 3;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Notificar al mediador que este enemigo ha sido derrotado
        GameStateContext.ChangeState(GameEventType.EnemyKilled);
        Destroy(gameObject);
    }
}
```

### Uso con Parametros (Versión Avanzada)

```csharp
// Para eventos con parámetros, usa UnityEvent<T>
public partial class GameStateContext
{
    public class GameStateMediator<T>
    {
        private static Dictionary<GameEventType, UnityEvent<T>> dictionary =
            new Dictionary<GameEventType, UnityEvent<T>>();

        public static void Subscribe(GameEventType _event, UnityAction<T> action)
        {
            if (dictionary.TryGetValue(_event, out var collection))
            {
                collection.AddListener(action);
            }
            else
            {
                var thisEvent = new UnityEvent<T>();
                thisEvent.AddListener(action);
                dictionary.Add(_event, thisEvent);
            }
        }

        public static void Publish(GameEventType type, T parameter)
        {
            if (dictionary.TryGetValue(type, out var collection))
            {
                collection.Invoke(parameter);
            }
        }
    }
}

// Ejemplo de uso con parámetro
public class DamagePopup : MonoBehaviour
{
    private void OnEnable()
    {
        GameStateContext.GameStateMediator<int>.Subscribe(
            GameEventType.EnemyKilled,
            OnEnemyKilledWithScore
        );
    }

    private void OnEnemyKilledWithScore(int scoreValue)
    {
        // Mostrar popup con puntos
        Debug.Log("Enemy defeated! +" + scoreValue + " points");
    }
}
```

---

## 6️⃣ Ventajas y Desventajas

### ✅ Ventajas

| Ventaja | Descripción |
|---------|-------------|
| **Bajo Acoplamiento** | Los componentes no se conocen directamente entre sí |
| **Centralización** | Toda la comunicación pasa por un punto central |
| **Facilidad de Mantenimiento** | Cambios en un componente no afectan a otros |
| **Reutilización** | Los componentes pueden reutilizarse en otros proyectos |
| **Simplifica dependencias** | Reduce la red de dependencias entre objetos |

### ❌ Desventajas

| Desventaja | Descripción |
|------------|-------------|
| **Punto único de fallo** | Si el mediador falla, todo el sistema falla |
| **Cuello de botella** | Puede convertirse en un cuello de botella si hay muchos eventos |
| **Debugging complejo** | Seguir el flujo de mensajes puede ser difícil |
| **Sobrecarga de memoria** | Mantener el diccionario de eventos consume memoria |

---

## 7️⃣ Mejores Prácticas

### 1. Uso de Interfaces

```csharp
public interface IGameEventListener
{
    void OnGameStateChanged(GameEventType newState);
}

public class AudioManager : MonoBehaviour, IGameEventListener
{
    public void OnGameStateChanged(GameEventType newState)
    {
        switch (newState)
        {
            case GameEventType.GamePause:
                AudioListener.pause = true;
                break;
            case GameEventType.GameResume:
                AudioListener.pause = false;
                break;
        }
    }
}
```

### 2. Limpieza de Suscripciones

```csharp
public class ComponentExample : MonoBehaviour
{
    private void OnEnable()
    {
        // Suscribirse
        GameStateContext.GameStateMediator.Subscribe(
            GameEventType.GamePause,
            OnGamePaused
        );
    }

    private void OnDisable()
    {
        // IMPORTANTE: Siempre desuscribirse
        GameStateContext.GameStateMediator.Unsubscribe(
            GameEventType.GamePause,
            OnGamePaused
        );
    }

    private void OnDestroy()
    {
        // Doble verificación para evitar memory leaks
        GameStateContext.GameStateMediator.Unsubscribe(
            GameEventType.GamePause,
            OnGamePaused
        );
    }

    private void OnGamePaused()
    {
        Debug.Log("Game Paused!");
    }
}
```

### 3. Patrón Observer vs Mediator

| Aspecto | Observer | Mediator |
|---------|----------|----------|
| **Relación** | 1 a muchos | Muchos a muchos |
| **Conocimiento** | Sujeto conoce observadores | Componentes no se conocen |
| **Acoplamiento** | Medio | Bajo |
| **Uso en Unity** | UnityEvents, Delegates | Tu implementación actual |

---

## 8️⃣ Comparación con Otros Patrones

### Mediator vs Sublógica

| Aspecto | Sublógica | Mediator |
|---------|-----------|----------|
| **Enfoque** | Modularidad por funcionalidad | Comunicación entre módulos |
| **Estructura** | Handler + Model + Sublógicas | Mediador central + Eventos |
| **Responsabilidad** | Encapsula lógica de un feature | Gestiona comunicación entre features |
| **MonoBehaviour** | Solo Handler es MonoBehaviour | Todo puede ser POCO |
| **Uso típico** | Features individuales | Sistemas transversales |

### Combinación Recomendada

```
GameStateContext (Mediator)
    │
    ├── GameStateMediator (Comunicación de eventos)
    │
    └── Features/ (Sublógica)
        ├── Boss/
        │   ├── BossHandler (MonoBehaviour)
        │   ├── BossModel
        │   └── BossAttack, BossMovement, BossHealth
        │
        ├── Player/
        │   ├── PlayerHandler (MonoBehaviour)
        │   ├── PlayerModel
        │   └── PlayerMovement, PlayerCombat, PlayerHealth
        │
        └── Score/
            ├── ScoreHandler (MonoBehaviour)
            └── ScoreModel
```

---

## 9️⃣ Resumen

El **Patrón Mediator con Contexto de Estado** es ideal para:

- ✅ Sistemas de eventos centralizados
- ✅ Comunicación entre sistemas no relacionados
- ✅ Reducción de dependencias en proyectos grandes
- ✅ Estados de juego con transiciones claras

**Recomendación**: Combina este patrón con tu skill de **Sublógica** para obtener lo mejor de ambos mundos: modularidad funcional y comunicación desacoplada.

---

## 🔗 Recursos Adicionales

- [Patrón Mediator - Gang of Four](https://refactoring.guru/design-patterns/mediator)
- [Unity Event System](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/api/UnityEngine.EventSystems.html)
- [ScriptableObject Events](https://unity.com/how-to/architect-game-code-scriptable-objects)