using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public int enemies;//Set Enemies Publically for Each Level
    public int arrows;//Set Arrows Publically for Each Level

    public bool arrowFired()
    {
        arrows--;
        if(arrows<=0){
            return false;
        }
        else{
            return true;
        }
    }
    public void EnemyDied(){
        enemies--;
        Debug.Log("Enemies left: "+enemies);
        if(enemies<=0){
            UnlockNextLevel();
            SceneManager.LoadScene(0);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); For Next Level
        }
    }
    public void UnlockNextLevel(){
        Debug.Log(PlayerPrefs.GetInt("ReachedIndex"));
        if(SceneManager.GetActiveScene().buildIndex>=PlayerPrefs.GetInt("ReachedIndex"))
        {
            Debug.Log("Unlocking Next Level");
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex+1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel")+1);
            PlayerPrefs.Save();
        }
    }
}
