using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerState : MonoBehaviour {
    public int playerId;
    public Text scoreText;
    
    int score = 0;

    public void IncreaseScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = "Score: " + score;
    }
}
