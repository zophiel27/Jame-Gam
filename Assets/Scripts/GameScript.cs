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
    public GameObject playerUI;
    private bool levelcleared = false;

    void Start()
    {
        FindObjectOfType<ArrowManager>().SetArrows(arrows);
    }
    public void ArrowFired()
    {
        arrows--;
        FindObjectOfType<ArrowManager>().SetArrows(arrows);
        if(arrows<=0){
            //After 5 seconds, Game Over UI will be shown
            Invoke(nameof(GameOver), 4.5f);
        }

    }
    public void EnemyDied(){
        enemies--;
        FindObjectOfType<ScoreManager>().AddPoints();

        Debug.Log("Enemies left: "+enemies);
        if(enemies<=0){
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
        SceneManager.LoadScene(0);
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    private void LevelCompleted(){
        UnlockNextLevel();
        levelCompletedUI.SetActive(true);
        playerUI.SetActive(false);
        //SceneManager.LoadScene(0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); For Next Level
    }
    private void GameOver(){
        if( !levelcleared)
        {
            gameOverUI.SetActive(true);
            playerUI.SetActive(false);
        }
        
    }
}
