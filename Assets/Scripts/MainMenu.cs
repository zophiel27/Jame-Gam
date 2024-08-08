using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public void QuitGame()
    {
        PlaySound();
        Invoke(nameof(Quit),0.2f);
    }
    public void PlaySound(){
            audioSource.Play();
    }
    private void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
