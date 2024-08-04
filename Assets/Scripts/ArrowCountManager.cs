using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrowManager : MonoBehaviour
{
    public TextMeshProUGUI arrowText;
    private int arrows;

    void Update()
    {
        arrowText.text = "ARROWS: " + arrows.ToString();
    }
    public void SetArrows(int arrowsNum){
        arrows = arrowsNum;
    }
}
