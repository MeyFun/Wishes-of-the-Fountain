using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Tracks")]
    public AudioClip mainMenuMusic;
    public AudioClip animationMusic;
    public AudioClip selectionMusic;
    public AudioClip wish1LevelMusic;
    public AudioClip wish2LevelMusic;
    public AudioClip wish3LevelMusic;

    [Header("Audio Clips")]
    public AudioClip buttonSound;
    public AudioClip walkSound;
    public AudioClip coinSound;
    public AudioClip waterBloobSound;
    public AudioClip waterSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip grabObjectSound;
    public AudioClip putObjectSound;
    public AudioClip carSound;
    public AudioClip carCrash;
    public AudioClip launghBoySound;
    public AudioClip cryBoySound;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}