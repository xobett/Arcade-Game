using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [Header("TEXT SETTINGS")]
    [SerializeField] private TextMeshProUGUI scoreText;

    void Update()
    {
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreText.text = $"Score: {HighScoreManager.Instance.Score}";
    }
}
