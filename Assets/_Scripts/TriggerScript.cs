using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Collider trigger;
    public GameEvent triggerEvent;

    private void Start()
    {
        trigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered " + gameObject.name);
            triggerEvent.Raise(this, null);
        }
    }
}
