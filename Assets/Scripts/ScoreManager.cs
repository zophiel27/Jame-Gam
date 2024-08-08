using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreIncrementText;
    private int score;// = 0;
    private Animator scoreTextAnimator;
    private Animator scoreTextIncrementAnimator;

    void Start()
    {
        score = 0;
        scoreTextAnimator = scoreText.GetComponent<Animator>();
        scoreTextIncrementAnimator = scoreIncrementText.GetComponent<Animator>();
    }
    void Update()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }
    public void AddPoints(){
        score += 10;
    }
    public void AddBonus(int bonus){
        score += 10*bonus;
    }
    public int GetPoints(){
        return score;
    }
    public void PlayScoreAnimation()
    {
        Debug.Log("ScoreAnimation called");
        scoreTextAnimator.SetTrigger("scorePop");
        scoreTextIncrementAnimator.SetTrigger("Increment");
    }
}
