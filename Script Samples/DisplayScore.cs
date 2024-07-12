using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public ScoreKeeper scoreKeeper;
    public TextMeshProUGUI yourScore;
    public TextMeshProUGUI highScore;

    public void Start()
    {
        yourScore.text = string.Format("Your Score: {0:000}", scoreKeeper.score);
        highScore.text = string.Format("High Score: {0:000}", scoreKeeper.highScore);
    }
}
