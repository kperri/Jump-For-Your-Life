using UnityEngine;
using UnityEngine.UI;

public class MenuEffects : MonoBehaviour
{

    public float colorTransitionSpeed;
    public Text muteMusic;
    public Text muteAudio;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    Text title;
    Color titleColor;
    Color yellow;
    float musicVolume;
    float sfxVolume;
    bool result;

    void Start ()
    {
        title = GetComponentInChildren<Text> ();
        titleColor = Color.red;
        title.color = titleColor;
        yellow = new Color (1.0f, 1.0f, 0.0f);
        musicVolume = GetAudioLevels (musicSource);
        sfxVolume = GetAudioLevels (sfxSource);

        if (musicVolume == 0.0f)
        {
            muteMusic.text = "Mute Music";
        }
        else
        {
            muteMusic.text = "Unmute Music";
        }

        if (sfxVolume == 0.0f)
        {
            muteAudio.text = "Mute SFX";
        }
        else
        {
            muteAudio.text = "Unmute SFX";
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (titleColor.b < 1.0f && titleColor.r == 1.0f)
        {
            titleColor = Color.Lerp (titleColor, Color.magenta, 1.0f * (Time.deltaTime * colorTransitionSpeed));
            title.color = titleColor;
            if (titleColor.b >= 0.95f)
            {
                titleColor.b = 1.0f;
            }
        }
        else if (titleColor.r > 0.0f && titleColor.b == 1.0f)
        {
            titleColor = Color.Lerp (titleColor, Color.blue, 1.0f * (Time.deltaTime * colorTransitionSpeed));
            title.color = titleColor;
            if (titleColor.r <= 0.05f)
            {
                titleColor.r = 0.0f;
            }
        }
        else if (titleColor.g < 1.0f && titleColor.b == 1.0f)
        {
            titleColor = Color.Lerp (titleColor, Color.cyan, 1.0f * (Time.deltaTime * colorTransitionSpeed));
            title.color = titleColor;
            if (titleColor.g >= 0.95f)
            {
                titleColor.g = 1.0f;
            }
        }
        else if (titleColor.b > 0.0f && titleColor.g == 1.0f)
        {
            titleColor = Color.Lerp (titleColor, Color.green, 1.0f * (Time.deltaTime * colorTransitionSpeed));
            title.color = titleColor;
            if (titleColor.b <= 0.05f)
            {
                titleColor.b = 0.0f;
            }
        }
        else if (titleColor.r < 1.0f && titleColor.g == 1.0f)
        {
            titleColor = Color.Lerp (titleColor, yellow, 1.0f * (Time.deltaTime * colorTransitionSpeed));
            title.color = titleColor;
            if (titleColor.r >= 0.95f)
            {
                titleColor.r = 1.0f;
            }
        }
        else if (titleColor.g > 0.0f && titleColor.r == 1.0f)
        {
            titleColor = Color.Lerp (titleColor, Color.red, 1.0f * (Time.deltaTime * colorTransitionSpeed));
            title.color = titleColor;
            if (titleColor.g <= 0.05f)
            {
                titleColor.b = 0.0f;
            }
        }
    }

    public void MuteMusic (AudioSource source)
    {
        if (muteMusic.text.Equals ("Unmute Music"))
        {
            print ("unmute music");
            muteMusic.text = "Mute Music";
            source.outputAudioMixerGroup.audioMixer.SetFloat ("Volume", 0.0f);
            source.time = 0.0f;
        }
        else if (muteMusic.text.Equals ("Mute Music"))
        {
            print ("mute music");
            muteMusic.text = "Unmute Music";
            source.outputAudioMixerGroup.audioMixer.SetFloat ("Volume", -80.0f);
        }
    }

    public void MuteAudioEffects (AudioSource source)
    {
        if (muteAudio.text.Equals ("Unmute SFX"))
        {
            print ("unmute audio");
            muteAudio.text = "Mute SFX";
            source.outputAudioMixerGroup.audioMixer.SetFloat ("Volume", 0.0f);
            source.time = 0.0f;
        }
        else if (muteAudio.text.Equals ("Mute SFX"))
        {
            print ("mute audio");
            muteAudio.text = "Unmute SFX";
            source.outputAudioMixerGroup.audioMixer.SetFloat ("Volume", -80.0f);
        }
    }

    float GetAudioLevels (AudioSource source)
    {
        float volume;
        bool result = source.outputAudioMixerGroup.audioMixer.GetFloat ("Volume", out volume);

        if (result)
        {
            return volume;
        }
        else
        {
            return 0.0f;
        }
    }
}
