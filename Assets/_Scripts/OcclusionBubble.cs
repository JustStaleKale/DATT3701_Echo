using System.Collections;
using UnityEngine;

public class OcclusionBubble : MonoBehaviour
{
    private Vector3 initialScale;
    private Vector3 maxScale;
    public Collider bubbleCollider;
    public float minRange = 2f;
    public float maxRange = 5f;
    public float duration = 1f;

    void Start()
    {
        initialScale = new Vector3(minRange, minRange, minRange);
        maxScale = new Vector3(maxRange, maxRange, maxRange);
        transform.localScale = initialScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered with " + other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Invisible"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Revealed");
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exited trigger with " + other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Revealed"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Invisible");
        }
    }

    IEnumerator ExpandAndReset()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration/2)
        {
            transform.localScale = Vector3.Lerp(initialScale, maxScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        elapsedTime = 0f;
        while (elapsedTime < duration/2)
        {
            transform.localScale = Vector3.Lerp(maxScale, initialScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = initialScale;
    }

    IEnumerator OverlapPing()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxRange, LayerMask.GetMask("Invisible", "Revealed"));
        foreach (Collider c in colliders)
        {
            if (c.gameObject.layer == LayerMask.NameToLayer("Invisible"))
            {
                c.gameObject.layer = LayerMask.NameToLayer("Revealed");
            }
        }
        yield return new WaitForSeconds(duration);
        foreach (Collider c in colliders)
        {
            if (c.gameObject.layer == LayerMask.NameToLayer("Revealed"))
            {
                c.gameObject.layer = LayerMask.NameToLayer("Invisible");
            }
        }
    }
    public void OnPing(Component sender, object data)
    {
        StopAllCoroutines();
        transform.localScale = initialScale;
        StartCoroutine(ExpandAndReset());
    }

    public void NoRbPing(Component sender, object data) //Ping objects without rigidbodies
    {
        StopAllCoroutines();
        StartCoroutine(OverlapPing());
    }

}
