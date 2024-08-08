using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    private int level;
    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel",1);
        for(int i=0; i<buttons.Length; i++)
        {
            if(i+1>unlockedLevel)
            {
                buttons[i].interactable = false;
            }
        }
        for(int i=0; i<unlockedLevel;  i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        level=levelId;
        Invoke(nameof(StartLevel), 0.2f);
    }
     [ContextMenu("Reset Progress")]
    public void ResetProgress()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();
        Awake();
    }
    private void StartLevel()
    {
        string levelName = "Level " + level;
        SceneManager.LoadScene(levelName);
    }
}
