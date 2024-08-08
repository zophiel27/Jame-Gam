using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource menuMusicSource;
    void Start()
    {
        // Load the saved state of menu music
        bool isMuted = PlayerPrefs.GetInt("MenuMusicMuted", 0) == 1;
        menuMusicSource.mute = isMuted;
        bool isMuted2 = PlayerPrefs.GetInt("AllSoundsMuted", 0) == 1;
        audioSource.mute = isMuted2;
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
    }
    public void ToggleMenuMusic(bool isMuted)
    {
        PlaySound();
        isMuted = !(PlayerPrefs.GetInt("MenuMusicMuted", 0) == 1);
        menuMusicSource.mute = isMuted;
        PlayerPrefs.SetInt("MenuMusicMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void ToggleAllSounds(bool isMuted)
    {
        PlaySound();
        isMuted = !(PlayerPrefs.GetInt("AllSoundsMuted", 0) == 1);
        PlayerPrefs.SetInt("AllSoundsMuted", isMuted ? 1 : 0);
        audioSource.mute = isMuted;
        PlayerPrefs.Save();
    }
    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
