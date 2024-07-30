using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : MonoBehaviour
{
    public Text arrowText;
    private int arrows;

    public void SetArrows(int arrowsNum){
        arrows = arrowsNum;
        arrowText.text = "ARROWS: " + arrows.ToString();
    }
}
