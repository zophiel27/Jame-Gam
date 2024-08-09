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
    private Dictionary<string, int> levelHighScores = new Dictionary<string, int>();

    void Start()
    {
        score = 0;
        scoreTextAnimator = scoreText.GetComponent<Animator>();
        scoreTextIncrementAnimator = scoreIncrementText.GetComponent<Animator>();
        scoreTextBonusAnimator = BonusText.GetComponent<Animator>();
        LoadHighScores();
    }
    

    // Method to check and update the highest score for a level
    public void CheckAndSetHighScore(string levelName)
    {
        int currentScore = GetPoints();
        if (levelHighScores.ContainsKey(levelName))
        {
            if (currentScore > levelHighScores[levelName])
            {
                levelHighScores[levelName] = currentScore;
                SaveHighScores();
            }
        }
        else
        {
            levelHighScores[levelName] = currentScore;
            SaveHighScores();
        }
    }

    // Save the high scores to PlayerPrefs
    private void SaveHighScores()
    {
        foreach (var level in levelHighScores)
        {
            PlayerPrefs.SetInt(level.Key, level.Value);
        }
        PlayerPrefs.Save();
    }

    // Load the high scores from PlayerPrefs
    private void LoadHighScores()
    {
        string[] levelNames = { "Level1", "Level2", "Level3", "Level4", "Level5","Level6" };
        foreach (string level in levelNames)
        {
            if (PlayerPrefs.HasKey(level))
            {
                levelHighScores[level] = PlayerPrefs.GetInt(level);
            }
            else
            {
                levelHighScores[level] = 0; // Initialize with 0 if no score is recorded yet
            }
        }
    }

    // Get the high score for a specific level
    public int GetHighScore(string levelName)
    {
        if (levelHighScores.ContainsKey(levelName))
        {
            return levelHighScores[levelName];
        }
        return 0;
    }
    public void SetHighScoreText(string levelName)
    {
        HighscoreText.text = "HIGHSCORE: " + GetHighScore(levelName).ToString();
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
