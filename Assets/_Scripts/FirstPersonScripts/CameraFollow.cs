using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        transform.position = target.position;
    }
}
