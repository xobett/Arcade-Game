using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private static HighScoreManager instance;
    public static HighScoreManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = new GameObject().AddComponent<HighScoreManager>();
                instance.name = instance.ToString();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public int Score { get; private set; }

    private void Start()
    {
        Score = 0;
    }

    public void AddScore()
    {
        Score += 10; 
    }
}
