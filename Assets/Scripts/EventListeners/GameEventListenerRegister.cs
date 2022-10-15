using System.Collections.Generic;
using UnityEngine;

public class GameEventListenerRegister : MonoBehaviour
{
    public List<GameEventListener> eventListeners;

    private void OnEnable() {
        foreach (GameEventListener eventListener in eventListeners)
        {
            eventListener.Event.Register(eventListener);
        }
    }
    
    private void OnDisable() {
        foreach (GameEventListener eventListener in eventListeners)
        {
            eventListener.Event.UnRegister(eventListener);
        }
    }
}
