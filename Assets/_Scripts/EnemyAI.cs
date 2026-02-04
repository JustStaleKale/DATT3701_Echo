using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Collider detectionRange;
    public Collider captureRange;

    public CharacterController characterController;
    public float enemySpeed = 2.2f;
    private bool playerDetected = false;
    private bool playerCaptured = false;
    private Vector3 moveVector;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        // StartCoroutine(PatrolCoroutine());
    }

    void Update()
    {
        if (!playerDetected)
        {
            moveVector = new Vector3(Mathf.Sin(Time.time), 0, Mathf.Cos(Time.time)).normalized;
        } 
        characterController.Move(enemySpeed * Time.deltaTime * moveVector);
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = true;
            moveVector = (other.gameObject.transform.position - transform.position).normalized;
            Debug.Log("Player Detected!");
        }
        if (other.gameObject.CompareTag("Player"))
        {
            playerCaptured = true;
            Debug.Log("Player Captured!");
            // Implement capture logic here
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
            Debug.Log("Player Lost!");
        }
        if (other.gameObject.CompareTag("Player"))
        {
            playerCaptured = false;
            Debug.Log("Player Escaped!");
            // Implement escape logic here
        }
    }

    // private IEnumerator PatrolCoroutine()
    // {
    //     circleVector = new Vector3(Mathf.Sin(Time.time), 0, Mathf.Cos(Time.time)).normalized;
    //     characterController.Move(circleVector * Time.deltaTime * enemySpeed);
    //     yield return null;
    // }

    // private void HandleRotation()
    // {
        
    // }

}
