using System.Xml;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public bool spin = false;

    // Update is called once per frame
    void Update()
    {
        if (spin) 
        {
            gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
        }
    }

    private void Spinner()
    {
        gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
    }
}
