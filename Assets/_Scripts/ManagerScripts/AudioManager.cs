using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    public AudioSource bgm;
    // Singleton instance.
	public static AudioManager Instance = null;

    void Start()
    {
        StartCoroutine(BGM());
    }
    void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}
    public void PlaySound(Component sender, object data)
    {
        AudioSource.PlayClipAtPoint(soundList[(int)data], sender.transform.position);
    }

    public void PlayPing(Component sender, object data)
    {
        AudioSource.PlayClipAtPoint(soundList[1], sender.transform.position);
    }

    public void PlayShootPing(Component sender, object data)
    {
        AudioSource.PlayClipAtPoint(soundList[2], sender.transform.position);
    }

    IEnumerator BGM()
    {
        // bgm.clip = soundList[4];
        // bgm.loop = false;
        // bgm.Play();

        // while(bgm.isPlaying)
        // {
        //     yield return null;
        // }

        bgm.clip = soundList[0];
        bgm.loop = true;
        bgm.Play();
        while(bgm.isPlaying)
        {
            yield return null;
        }

    }

}
