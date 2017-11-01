using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager audioManager;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    float musicVolume;
    float sfxVolume;
    bool result;

    void Awake ()
    {
        if (audioManager == null)
        {
            DontDestroyOnLoad (gameObject);
            audioManager = this;
        }
        else if (audioManager != this)
        {
            Destroy (gameObject);
        }
    }
}
