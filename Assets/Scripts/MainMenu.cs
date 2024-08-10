using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource menuMusicSource;
    public Image musicToggleImage;
    public Image sfxToggleImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;
    public Sprite sfxOnSprite;
    public Sprite sfxOffSprite;
    void Start()
    {
        // Load the saved state of menu music
        bool isMuted = PlayerPrefs.GetInt("MenuMusicMuted", 0) == 1;
        menuMusicSource.mute = isMuted;
        musicToggleImage.sprite = isMuted ? musicOffSprite : musicOnSprite;
        isMuted = PlayerPrefs.GetInt("AllSoundsMuted", 0) == 1;
        audioSource.mute = isMuted;
        sfxToggleImage.sprite = isMuted ? sfxOffSprite : sfxOnSprite;
    }
    public void QuitGame()
    {
        PlaySound();
        Invoke(nameof(Quit),0.2f);
    }
    public void PlaySound(){
            audioSource.Play();
    }
    public void ResetGame()
    {
        PlaySound();
        // Clear all PlayerPrefs data
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        menuMusicSource.mute = false;
        musicToggleImage.sprite =  musicOnSprite;
        audioSource.mute = false;
        sfxToggleImage.sprite =  sfxOnSprite;
    }
    public void ToggleMenuMusic(bool isMuted)
    {
        PlaySound();
        isMuted = !(PlayerPrefs.GetInt("MenuMusicMuted", 0) == 1);
        menuMusicSource.mute = isMuted;
        PlayerPrefs.SetInt("MenuMusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();

        musicToggleImage.sprite = isMuted ? musicOffSprite : musicOnSprite;
    }
    public void ToggleAllSounds(bool isMuted)
    {
        PlaySound();
        isMuted = !(PlayerPrefs.GetInt("AllSoundsMuted", 0) == 1);
        PlayerPrefs.SetInt("AllSoundsMuted", isMuted ? 1 : 0);
        audioSource.mute = isMuted;
        PlayerPrefs.Save();

        sfxToggleImage.sprite = isMuted ? sfxOffSprite : sfxOnSprite;
    }
    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
