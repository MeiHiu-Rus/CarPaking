using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import thư viện UI

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip parking;
    public AudioClip carsound;
    public AudioClip smoke;
    public AudioClip carAcciden;

    private bool isMusicOn = true;
    private bool isSFXOn = true;

    private float musicVolume = 1.0f;
    

    // Thêm biến cho slider
    [SerializeField] Slider musicSlider;
    

    private void Start()
    {
        // Gán giá trị âm lượng ban đầu cho slider
        musicSlider.value = musicVolume;
        

        // Đăng ký sự kiện cho Slider
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        

        PlayMusic();
    }

    public void PlayMusic()
    {
        if (isMusicOn)
        {
            musicSource.clip = background;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (isSFXOn)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        if (isMusicOn)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
        }
    }

    public void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    
}
