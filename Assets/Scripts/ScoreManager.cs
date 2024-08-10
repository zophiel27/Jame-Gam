using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI HighscoreText;
    public TextMeshProUGUI BonusText;
    public TextMeshProUGUI scoreIncrementText;
    private int score;// = 0;
    private Animator scoreTextAnimator;
    private Animator scoreTextIncrementAnimator;
    private Animator scoreTextBonusAnimator;
    private bool newHighScore = false;
    
    void Start()
    {
        score = 0;
        scoreTextAnimator = scoreText.GetComponent<Animator>();
        scoreTextIncrementAnimator = scoreIncrementText.GetComponent<Animator>();
        scoreTextBonusAnimator = BonusText.GetComponent<Animator>();
    }

    // Method to check and update the highest score for a level
    public void CheckAndSetHighScore(string levelName)
    {
        int currentScore = GetPoints();
        int savedHighScore = PlayerPrefs.GetInt(levelName, 0); // Default to 0 if no score is saved

        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt(levelName, currentScore);
            PlayerPrefs.Save();
            newHighScore = true;
        }
    }

    // Get the high score for a specific level
    public int GetHighScore(string levelName)
    {
        return PlayerPrefs.GetInt(levelName, 0); // Default to 0 if no score is saved
    }

    // Set the high score text for a specific level
    public void SetHighScoreText(string levelName)
    {
        HighscoreText.text = "High Score: " + GetHighScore(levelName).ToString();
    }
    public bool checkNewHighScore()
    {
        return newHighScore;
    }
    void Update()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }
    public void AddPoints(){
        score += 10;
    }
    public void AddBonus(int bonus){
        int bonusPoints = 10*bonus;
        score += bonusPoints;
        BonusText.text = "+" + bonusPoints.ToString();
    }
    public int GetPoints(){
        return score;
    }
    [ContextMenu("PlayScoreAnimation")]
    public void PlayScoreAnimation()
    {
        Debug.Log("ScoreAnimation called");
        scoreTextAnimator.SetTrigger("scorePop");
        scoreTextIncrementAnimator.SetTrigger("Increment");
    }
    [ContextMenu("PlayBonusAnimation")]
    public void PlayBonusAnimation()
    {
        scoreTextBonusAnimator.SetTrigger("BonusIncrement");
    }
}
