using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource a_music;
    [SerializeField] private AudioSource a_sound;

    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioClip[] spookySounds;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();

        if (SceneManager.GetActiveScene().buildIndex > 1)
        {
            //Container.rateCount++;
            //if (Container.rateCount > 3)
            //    Container.rateCount = 0;

            //Container.gamePlayed++;
        }
    }

    public void PlayMusic()
    {
        if (a_music != null && Container.music.Equals(1))
            a_music.Play();
    }

    public void PauseMusic()
    {
        if (a_music != null)
            a_music.Pause();
    }

    public void PlaySound(int index)
    {
        if (a_sound != null)
            a_sound.PlayOneShot(sounds[index]);
    }

    public void PlaySpookySound(int index)
    {
        if (a_sound != null)
            a_sound.PlayOneShot(spookySounds[index]);
    }

    public void PlaySound(int index, bool loop)
    {
        if (a_sound != null && !a_sound.isPlaying)
            a_sound.PlayOneShot(sounds[index]);
    }

    public void StopSound()
    {
        a_sound.Stop();
    }
}