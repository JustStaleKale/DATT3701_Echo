using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public Collider triggerCollider;
    public Transform door;
    private bool open;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerCollider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        open = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            door.position += new Vector3(0, 5f, 0) * Time.deltaTime;
        }
    }
}
