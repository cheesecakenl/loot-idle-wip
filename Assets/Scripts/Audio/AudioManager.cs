using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;

    [SerializeField] private AudioDatabase audioDatabase;

    private AudioMixer audioMixer;
    private AudioSource audioSourceBGM;
    private AudioSource audioSourceSFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        audioMixer = audioDatabase.audioMixer;

        audioSourceBGM = gameObject.AddComponent<AudioSource>();
        audioSourceSFX = gameObject.AddComponent<AudioSource>();

        audioSourceBGM.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        audioSourceSFX.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
    }

    public void ChangeBgmVolume(float value)
    {
        // Goes from -80 dB to 0 dB
        // Conversion value 1.0f is normal volume 0dB
        // Conversion value 0.5f is -6db
        // Conversion value 0.1f is -20db
        // Conversion value 0.01f is very soft -40dB
        float dB = Mathf.Log10(value) * 20;

        // In the AudioMixer inspector expose the Volume and name it 'VolumeBGM'
        audioMixer.SetFloat("VolumeBGM", dB);
    }

    public void ChangeSfxVolume(float value)
    {
        // Goes from -80 dB to 0 dB
        // Conversion value 1.0f is normal volume 0dB
        // Conversion value 0.5f is -6db
        // Conversion value 0.1f is -20db
        // Conversion value 0.01f is very soft -40dB
        float dB = Mathf.Log10(value) * 20;

        // In the AudioMixer inspector expose the Volume and name it 'VolumeSFX'
        audioMixer.SetFloat("VolumeSFX", dB);
    }

    public void PlayMusic(AudioType audioType, string label)
    {
        if (audioSourceBGM.isPlaying)
        {
            audioSourceBGM.Stop();
        }

        AudioClip clip = audioDatabase.GetAudioClip(audioType, label);

        if (clip == null) return;

        audioSourceBGM.clip = clip;
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();
    }

    public void PlayFX(AudioType audioType, string label)
    {
        AudioClip clip = audioDatabase.GetAudioClip(audioType, label);

        if (clip == null) return;

        audioSourceSFX.pitch = Random.Range(0.95f, 1.05f);
        audioSourceSFX.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        if (audioSourceBGM.isPlaying)
        {
            audioSourceBGM.Stop();
        }
    }
}