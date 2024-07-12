using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScoreKeeper: ScriptableObject
{
    public int score = 0;
    public int highScore = 0;

    public void resetScore()
    {
        score = 0;
    }

    //sets new highscore if the old one is beat
    public void compareScore()
    {
        if(score > highScore)
        {
            highScore = score;
        }
    }
}
