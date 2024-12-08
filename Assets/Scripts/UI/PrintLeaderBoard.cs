using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PrintLeaderBoard : MonoBehaviour
{
    public TMP_Text[] scoreText;
    public ScoreManager scoreManager;
    void Start()
    {
        scoreManager=GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        printRanking();
    }

    public void printRanking()
    {
        for(int i=0;i<9;i++)
        {
            string scoreString = string.Join(" ", scoreManager.Scores[i]);
            scoreText[i].text += scoreString;
        }
    }
}
