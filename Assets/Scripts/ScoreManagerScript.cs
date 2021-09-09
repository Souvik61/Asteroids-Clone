using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
    public TMP_Text scoreText;
    private uint playerScore;
    public uint PlayerScore
    {
        get
        {
            return playerScore;
        }
        set 
        {
            playerScore = value;
        }
    }
    private void OnEnable()
    {
        AllEventsScript.OnAsteroidDestroy += OnAsteroidDestroyed;
    }

    private void OnDisable()
    {
        AllEventsScript.OnAsteroidDestroy -= OnAsteroidDestroyed;
    }

    private void Awake()
    {
        playerScore = 0;
    }

    void ResetScore()
    {
        playerScore = 0;
    }

    void OnAsteroidDestroyed(string astTag)
    {
        switch (astTag)
        {
            case "small_asteroid":
                playerScore += 100;
                break;
            case "medium_asteroid":
                playerScore += 50;
                break;
            case "large_asteroid":
                playerScore += 20;
                break;
            default:
                break;
        }

        scoreText.text = playerScore.ToString("000000");
    }

}
