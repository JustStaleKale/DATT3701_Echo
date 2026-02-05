using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EchoSignal : MonoBehaviour
{
    public Rigidbody rb;
    public float range = 5f;
    public float duration = 1f;
    public int maxBounces = 3;

    private Vector3 lastVelocity;
    private float currentSpeed;
    private Vector3 direction;
    private int bounces = 0;

    public float lifeTime = 3f;

    private List<Collider> revealedColliders;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        revealedColliders = new List<Collider>();
    } 

    // Update is called once per frame
    void LateUpdate()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            foreach (Collider c in revealedColliders)
            {
                if (c != null && c.gameObject.layer == LayerMask.NameToLayer("Revealed"))
                {
                    c.gameObject.layer = LayerMask.NameToLayer("Invisible");
                }
            }
            revealedColliders.Clear();
            Destroy(gameObject);
        }
        lastVelocity = rb.linearVelocity;
    }

    // private void OnCollisionEnter(Collision other) {
    //     // Ping(this, null);
    //     if (bounces <= maxBounces)
    //     {
    //         bounces++;
    //         currentSpeed = lastVelocity.magnitude;
    //         direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);
    //         rb.linearVelocity = direction * Mathf.Max(currentSpeed, 0f);
    //         //rb.AddForce(direction * 10f, ForceMode.Impulse);
    //     }
    //     else
    //     {
    //         foreach (Collider c in revealedColliders)
    //         {
    //             if (c != null && c.gameObject.layer == LayerMask.NameToLayer("Revealed"))
    //             {
    //                 c.gameObject.layer = LayerMask.NameToLayer("Invisible");
    //             }
    //         }
    //         revealedColliders.Clear();
    //         Destroy(gameObject);
    //     }
    // }

    //Unlimited Bounces
    private void OnCollisionEnter(Collision other) {
        currentSpeed = lastVelocity.magnitude;
        direction = Vector3.Reflect(lastVelocity.normalized, other.contacts[0].normal);
        rb.linearVelocity = direction * Mathf.Max(currentSpeed, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        revealedColliders.Add(other);
        //Debug.Log("Triggered with " + other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Invisible"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Revealed");
        }
    }
    
    // private void OnTriggerExit(Collider other)
    // {
    //     //Debug.Log("Exited trigger with " + other.gameObject.name);
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Revealed"))
    //     {
    //         other.gameObject.layer = LayerMask.NameToLayer("Invisible");
    //     }
    // }

    // public void Ping(Component sender, object data)
    // {
    //     StartCoroutine(OverlapPing());
    // }


    // IEnumerator OverlapPing()
    // {
    //     Collider[] colliders = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Invisible", "Revealed"));
    //     foreach (Collider c in colliders)
    //     {
    //         if (c.gameObject.layer == LayerMask.NameToLayer("Invisible"))
    //         {
    //             c.gameObject.layer = LayerMask.NameToLayer("Revealed");
    //         }
    //     }
    //     yield return new WaitForSeconds(duration);
    //     foreach (Collider c in colliders)
    //     {
    //         if (c.gameObject.layer == LayerMask.NameToLayer("Revealed"))
    //         {
    //             c.gameObject.layer = LayerMask.NameToLayer("Invisible");
    //         }
    //     }
    // }
}
