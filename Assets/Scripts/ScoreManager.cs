using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int score = 0;

    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    public void AddPoints(){
        score += 10;
        scoreText.text = "SCORE: " + score.ToString();
    }
    public int GetPoints(){
        return score;
    }
}
