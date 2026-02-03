using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;      
    public float height = 10f;     
    public float smoothSpeed = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}