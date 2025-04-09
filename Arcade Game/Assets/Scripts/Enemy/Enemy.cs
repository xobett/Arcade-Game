using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float enemyHealth;

    void Start()
    {
        enemyHealth = 100f;
    }

    public void TakeDamage(float damageTaken)
    {
        enemyHealth -= damageTaken;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            HighScoreManager.Instance.AddScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameManager.Instance.AddGroundedEnemy();
        }
    }
}
