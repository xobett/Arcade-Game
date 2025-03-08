using UnityEngine;

public class PlayerPinPon : MonoBehaviour
{
    [SerializeField] private float velocity;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
    }

    float AxisMove()
    {
        return Input.GetAxis("Horizontal");
    }

    void Move()
    {
       rb.MovePosition(rb.position + new Vector2(AxisMove() * velocity * Time.deltaTime, 0));
    }
}
