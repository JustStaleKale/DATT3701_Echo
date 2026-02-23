using UnityEngine;
using System.Collections.Generic;  

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private float speed;
    // [SerializeField]
    // private Vector3 direction;
    [SerializeField]
    private List<GameObject> onBelt;

    void FixedUpdate()
    {
        // For every item on the belt, add force to it in the direction given
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            if (onBelt[i] != null)
            {
                onBelt[i].GetComponent<Rigidbody>().AddForce(-transform.right * speed, ForceMode.Force);
            } else 
            {
                onBelt.Remove(onBelt[i]);
            }
        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
