using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int itemCount;
    private int enemyCount;
    private int score;
    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private TextMeshProUGUI scoreText;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void DefeatedEnemies()
    {
        enemyCount++;
        enemyText.text = enemyCount.ToString();
    }
    public void Items()
    {
        itemCount++;
        itemText.text = itemCount.ToString();
    }
    public void Score()
    {
        score++;
        scoreText.text = score.ToString();
    }

}
