using UnityEngine;

public class AudioLighting : MonoBehaviour
{
    public AudioSource audioSource;
    public Light targetLight;

    public float updateInterval = 0.1f;
    public int sampleSize = 512;
    public float intensityMultiplier = 5.0f;
    private float[] audioSampleData;
    public float clipLoudness = 0f;
    public float minIntensity = 0f;
    public float maxIntensity = 15f;
    public int pingDuration = 10;
    private bool pingActive = false;
    private int countSeconds = 0;

    private void Start()
    {
        countSeconds = 0;
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (targetLight == null)
        {
            targetLight = GetComponent<Light>();
        }

        audioSampleData = new float[sampleSize];
        InvokeRepeating("UpdateAudioData", 0f, updateInterval);
    }

    private void UpdateAudioData()
    {
        if (pingActive)
        {
            Debug.Log(countSeconds);
            countSeconds++;
            if (countSeconds >= pingDuration)
            {
                pingActive = false;
                countSeconds = 0;
            }
            targetLight.intensity = maxIntensity;
            //pingActive = false;
            return;
        }
        if (audioSource.isPlaying)
        {
            audioSource.GetOutputData(audioSampleData, 0);
            clipLoudness = 0f;

            foreach (var sample in audioSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }

            clipLoudness /= sampleSize;
            float intensity = Mathf.Clamp(clipLoudness * intensityMultiplier, minIntensity, maxIntensity);
            targetLight.intensity = intensity;
        }
        else
        {
            targetLight.intensity = minIntensity;
        }
    }

    public void Ping(Component sender, object data)
    {
        pingActive = (bool)data;
        targetLight.intensity = maxIntensity;
    }
}
