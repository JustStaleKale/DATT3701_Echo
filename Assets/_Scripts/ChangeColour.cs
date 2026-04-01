using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Light doorLight;
    private Renderer lightRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightRenderer = gameObject.GetComponent<Renderer>();
        doorLight.color = Color.red;
        lightRenderer.material.SetColor("_EmissionColor", Color.red);
    }

    public void ChangeToGreen()
    {
        doorLight.color = Color.green;
        lightRenderer.material.SetColor("_EmissionColor", Color.green);
    }
}
