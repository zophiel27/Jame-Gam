using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrowManager : MonoBehaviour
{
    public TextMeshProUGUI arrowText;
    private int arrows;

    public void SetArrows(int arrowsNum){
        arrows = arrowsNum;
        arrowText.text = "ARROWS: " + arrows.ToString();
    }
}
