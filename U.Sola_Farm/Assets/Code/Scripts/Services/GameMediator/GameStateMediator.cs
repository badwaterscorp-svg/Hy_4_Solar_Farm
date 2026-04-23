using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class GameStateContext 
{
    public class GameStateMediator
    {
        private static Dictionary<GameEventType, UnityEvent> dictionary = new Dictionary<GameEventType, UnityEvent>();

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


        public static void Unsubscribe(GameEventType _event, UnityAction action)
        {
            if (dictionary.TryGetValue(_event, out var collection))
            {
                collection.RemoveListener(action);
            }
        }

        internal static void Publish(GameEventType type)
        {
            if (dictionary.TryGetValue(type, out var collection))
            {
                collection.Invoke();
                Debug.Log("ada:" + type);
            }
        }
    }
}


public partial class GameStateContext
{
    public static GameEventType State { get; private set; }
    public static void ChangeState(GameEventType newState)
    {
        State = newState;
        GameStateMediator.Publish(newState);
    }
}

