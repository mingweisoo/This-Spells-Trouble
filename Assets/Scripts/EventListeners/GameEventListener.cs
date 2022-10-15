using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventListener
{
    public GameEvent Event;

    public UnityEvent Response;

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
