using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent gameEvent;
    [System.Serializable] public class CustomGameEvent: UnityEvent<Component, object>{};
    public CustomGameEvent response;
    private void OnEnable()
    {
        if (gameEvent != null)
        {
            gameEvent.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        if (gameEvent != null)
        {
            gameEvent.UnRegisterListener(this);
        }
    }

    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
