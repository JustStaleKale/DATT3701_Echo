using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 1f, 0f);
    public Transform target;
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
