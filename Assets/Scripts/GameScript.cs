using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public int enemies;//Set Enemies Publically for Each Level
    public int arrows;//Set Arrows Publically for Each Level
    public GameObject levelCompletedUI;
    public GameObject gameOverUI;
    public GameObject restartAndBackUI;
    public GameObject playerUI;
    public GameObject ArrowDecrement;
    public GameObject ScoreIncrement;
    private bool levelcleared = false;
    public AudioSource audioSource;

    void Start()
    {
        FindObjectOfType<ArrowManager>().SetArrows(arrows);
    }
    public void ArrowFired()
    {
        arrows--;
        //setArrowDecrement();
        FindObjectOfType<ArrowManager>().PlayArrowAnimation();
        FindObjectOfType<ArrowManager>().SetArrows(arrows);
        if(arrows<=0){
            //After 5 seconds, Game Over UI will be shown
            Invoke(nameof(GameOver), 4.5f);
        }

    }
    public void EnemyDied(){
        enemies--;
        setScoreIncrement();
        FindObjectOfType<ScoreManager>().PlayScoreAnimation();
        FindObjectOfType<ScoreManager>().AddPoints();
        Debug.Log("Enemies left: "+enemies);
        if(enemies<=0){
            FindObjectOfType<ScoreManager>().AddBonus(arrows);
            levelcleared = true;
            //After 2 seconds, Level Completed UI will be shown
            Invoke(nameof(LevelCompleted), 2f);
        }
    }
    public void UnlockNextLevel(){
        if(SceneManager.GetActiveScene().buildIndex>=PlayerPrefs.GetInt("UnlockedLevel"))
        {
            PlayerPrefs.SetInt("UnlockedLevel", SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.Save();
            Debug.Log("Unlocked Level"+PlayerPrefs.GetInt("UnlockedLevel"));
        }
    }
    public void Back(){
        PlaySound();
        Invoke(nameof(BackLevel), 0.2f);
    }
    public void BackLevel(){
        SceneManager.LoadScene(0);
    }
    public void Restart(){
        PlaySound();
        Invoke(nameof(RestartLevel), 0.2f);
    }
    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel(){
        PlaySound();
        Invoke(nameof(Next), 0.2f);
    }
    public void Next(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    private void LevelCompleted(){
        UnlockNextLevel();
        levelCompletedUI.SetActive(true);
        playerUI.SetActive(false);
        restartAndBackUI.SetActive(false);
        //SceneManager.LoadScene(0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); For Next Level
    }
    private void GameOver(){
        if( !levelcleared)
        {
            gameOverUI.SetActive(true);
            playerUI.SetActive(false);
            restartAndBackUI.SetActive(false);
        }
    }
    private void setArrowDecrement(){
        ArrowDecrement.SetActive(true);
        Invoke(nameof(unsetArrowDecrement),1.5f);
    }
    private void unsetArrowDecrement(){
        ArrowDecrement.SetActive(false);
    }
    private void setScoreIncrement(){
        ScoreIncrement.SetActive(true);
        Invoke(nameof(unsetScoreIncrement),1.5f);
    }
    private void unsetScoreIncrement(){
        ScoreIncrement.SetActive(false);
    }
    public void PlaySound(){
        audioSource.Play();
    }
}
