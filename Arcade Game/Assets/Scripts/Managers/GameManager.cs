using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instance
    public static GameManager Instance { get; private set; }

    public int GroundedEnemies { get; private set; }

    //Game Over settings
    public bool GameOver { get; private set; }

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject hudPanel;

    [SerializeField] private PlayfabManager playfabManager;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (GroundedEnemies == 3 && !GameOver)
        {
            GameOver = true;
            FinishGame();
        }
    }

    public void AddGroundedEnemy()
    {
        GroundedEnemies++;
    }

    [ContextMenu("Try")]
    void FinishGame()
    {
        hudPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;

        playfabManager.UpdateLeaderBoard("High", 1500);

        StartCoroutine(ShowLeaderboard());
    }

    IEnumerator ShowLeaderboard()
    {
        yield return new WaitForSecondsRealtime(3);

        playfabManager.GetLeaderboard();
    }
}
