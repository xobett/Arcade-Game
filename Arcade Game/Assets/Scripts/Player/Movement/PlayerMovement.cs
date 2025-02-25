using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    //Float that sets the walk speed of the player.
    [SerializeField, Range(1f, 5f)] private float walkSpeed;
    //Float that sets the sprint speed of the player.
    [SerializeField, Range(1f, 5f)] private float sprintSpeed;
    //2D Rigidbody of the player.
    private Rigidbody2D playerRb;

    [Header("JUMP SETTINGS")]
    //Float that sets the jump force of the player.
    [SerializeField, Range (0f, 1f)] private float jumpForce = 0.2f;
    //Bool that changes whe
    private bool isJumping;

    [Header("GRAVITY SETTINGS")]
    [SerializeField] private float gravitySpeed = -0.01f;
    private Vector2 gravity;

    [Header("GROUND CHECK SETTINGS")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsGround;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();

        groundCheck = transform.GetChild(0);

        //Target frame rate is changed to 60 fps due problems with input dectection, but since it was changed to 60 fps it was fixed. 
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        IsJumpingCheck();
    }

    private void FixedUpdate()
    {
        MovementAndJump();
        Jump();
    }

    private void Jump ()
    {
        if (isJumping && IsTouching())
        {
            //Sums to the gravity vector in the Y axis the jump force units.
            gravity.y = jumpForce;
        }
    }

    private void MovementAndJump()
    {
        //Constantly rests gravity speed units to the vector in the Y axis.
        gravity.y += gravitySpeed;

        //If the player is touching and gravity is still exerting downwards force, it sets the gravity Y axis to 0, otherwise, if its only touching, it will always set the Y axis to 0, preventing 
        //the player from jumping, due to the jumping method being an addition to the Y axis.
        if (IsTouching() && gravity.y < 0)
        {
            gravity.y = 0f;
        }

        //Moves the player position, in the X axis moves the number of units givem by the horizontal input, and in the Y axis is constantly exerting gravity when it's not touching ground.
        playerRb.MovePosition(playerRb.position + new Vector2(HorizontalInput() * Time.deltaTime, gravity.y));
    }

    private void IsJumpingCheck()
    {
        //This method is used for Jump input, due to the Update method being better used for input than the fixed update.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }

    private float SpeedCheck()
    {
        //Returns current speed depending on the player input, either if it's sprinting or not.
        return IsSprinting() ? sprintSpeed : walkSpeed;
    }

    private bool IsSprinting()
    {
        //Returns true if the player is sprinting with Left Shift.
        return Input.GetKey(KeyCode.LeftShift);
    }

    private float HorizontalInput()
    {
        //Returns Horizontal input multiplied by the current speed check.
        return Input.GetAxis("Horizontal") * SpeedCheck();
    }

    private bool IsTouching()
    {
        //Detects collision with ground.
        return Physics2D.OverlapCircle(groundCheck.position, radius, whatIsGround);
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }
}
