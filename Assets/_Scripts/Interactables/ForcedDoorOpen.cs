using UnityEngine;

public class ForcedDoorOpen : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    public Component linkedComponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open(Component sender, object data)
    {
        if (sender == linkedComponent)
        {
            animator.SetBool("character_nearby", true);
        }
        
    }
}
