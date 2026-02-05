using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Player Movement Stats")]
    public float walkSpeed = 2f;
    public float crouchSpeed = 1f;
    public float runSpeed = 4f;
    public float rotationSpeed = 10f;

    [Header("Echo Stats")]
    public float echoForce = 100f;
    public float pingCooldown = 1f;
    public float currentAmmo = 3f;
    public float maxAmmo = 3f;
    public float reloadTime = 3f;
    
}
