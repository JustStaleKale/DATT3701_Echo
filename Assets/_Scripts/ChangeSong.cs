using UnityEngine;

public class ChangeSong : MonoBehaviour
{
    public GameEvent changeBGM;
    private Collider collider;  

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    
    private void OnTriggerEnter(Collider other) {
        changeBGM.Raise(this, 5);
    }
}
