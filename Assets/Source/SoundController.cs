using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _sfx;

    [SerializeField]
    private AudioSource _music;



    public void SetupSFX(float volume)
    {
        if (_sfx == null)
            return;

        _sfx.volume = volume;
    }

    public void SetupMusic(float volume)
    {
        if (_music == null)
            return;

        _music.volume = volume;
    }
}
