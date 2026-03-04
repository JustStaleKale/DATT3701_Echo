using UnityEngine;

public class PingSwitch : MonoBehaviour
{
    public GameObject door;
    private Animator doorAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {

        doorAnimator.SetBool("character_nearby", true);
    }
    
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Echo"))
    //     {
    //         doorAnimator.SetBool("character_nearby", false);
    //     }
    // }
}
