using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIFillBar : MonoBehaviour
{
    public Image fillImage;

    public AudioSource audioSource;
    public AudioClip pingClip;
    public PlayerStats playerStats;

    private float chargeSpeed = 0.25f;
    private float cost = 1f;

    float currentCharge = 1f;
    bool isRecharging = false;

    void Start()
    {
        chargeSpeed = 1/playerStats.pingCooldown;
    }

    void Update()
    {
        //HandleInput();
        RechargeBar();
        UpdateUI();
    }

    // void HandleInput()
    // {
    //     if (Keyboard.current != null &&
    //         Keyboard.current.qKey.wasPressedThisFrame &&
    //         currentCharge >= cost)
    //     {
    //         UseBar();
    //     }
    // }


    public void UseBar()
    {
        currentCharge -= cost;
        currentCharge = Mathf.Clamp01(currentCharge);
        isRecharging = true;

        if (pingClip != null)
        {
            audioSource.PlayOneShot(pingClip);
        }
    }

    void RechargeBar()
    {
        if (!isRecharging) return;

        currentCharge += chargeSpeed * Time.deltaTime;

        if (currentCharge >= 1f)
        {
            currentCharge = 1f;
            isRecharging = false;
        }
    }

    void UpdateUI()
    {
        fillImage.fillAmount = currentCharge;
    }
}
