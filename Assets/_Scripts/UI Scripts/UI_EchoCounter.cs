using TMPro;
using UnityEngine;

public class UI_EchoCounter : MonoBehaviour
{
    public TextMeshProUGUI echoCountText;
    public PlayerStats playerStats;

    // Update is called once per frame
    void Update()
    {
        echoCountText.text = "Echoes: " + playerStats.currentAmmo.ToString() + " / " + playerStats.maxAmmo.ToString();
        
    }
}
