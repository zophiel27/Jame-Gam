using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrowManager : MonoBehaviour
{
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI arrowDecrementText;
    private int arrows;
    private Animator arrowTextAnimator;
    private Animator arrowTextDecrementAnimator;
    void Start()
    {
        arrowTextAnimator = arrowText.GetComponent<Animator>();
        arrowTextDecrementAnimator = arrowDecrementText.GetComponent<Animator>();
    }
    void Update()
    {
        arrowText.text = "ARROWS: " + arrows.ToString();
    }
    public void SetArrows(int arrowsNum){
        arrows = arrowsNum;
    }
    public void PlayArrowAnimation()
    {
        Debug.Log("PlayArrowAnimation called");
        arrowTextAnimator.SetTrigger("Pop");
        arrowTextDecrementAnimator.SetTrigger("Decrement");
    }
}
