using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent", order = 3)]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> eventListeners = 
        new List<GameEventListener>();

    public void Raise()
    {
        foreach (GameEventListener eventListener in eventListeners)
        {
            eventListener.OnEventRaised();
        }
    }

    public void Register(GameEventListener eventListener)
    {
        if (!eventListeners.Contains(eventListener))
            eventListeners.Add(eventListener);
    }

    public void UnRegister(GameEventListener eventListener)
    {
        if (eventListeners.Contains(eventListener))
            eventListeners.Remove(eventListener);
    }
}

