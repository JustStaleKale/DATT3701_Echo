using UnityEngine;

public class SendWinState : MonoBehaviour
{
    public Collider sender;
    public GameEvent winEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sender == null)
        {
            sender = GetComponent<Collider>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            winEvent.Raise(this, true);
        }
    }
}
