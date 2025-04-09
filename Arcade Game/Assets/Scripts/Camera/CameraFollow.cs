using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Transform used to follow player's position.
    [SerializeField] private Transform playerTransform;

    [Header("FOLLOW SETTINGS")]
    //Offset Vector used to customize following distances.
    [SerializeField] private Vector3 offsetFollow = new Vector3(0, 0, 3);

    //Float used to change the camera's following speed;
    [SerializeField, Range(0, 10)] private float followVelocity;

    private Vector3 velocityRef = Vector3.zero;

    private float velocity;

    public bool playerIsNearWall;

    void Start()
    {
        ReferencePlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Follow method is placed in the Fixed Update method due to stuttering in the normal Update method.
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (!playerIsNearWall)
        {
            //Follows the player smoothly, having a Vector3 offset to customize following distances.
            FolowingMethod1();
            //FollowingMethod2();
        }
    }

    private void FollowingMethod2()
    {
        float horizontalFollowing = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref velocity, 1 / followVelocity);

        Vector3 testVector = new Vector3(horizontalFollowing, transform.position.y, transform.position.z);

        transform.position = testVector;
    }

    private void FolowingMethod1()
    {
        if (PlayerIsGrounded())
        {

        }

        transform.position = Vector3.SmoothDamp(transform.position, playerTransform.position + offsetFollow, ref velocityRef, 1f / followVelocity);
    }

    private void ReferencePlayer()
    {
        //References the player transform.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private bool PlayerIsGrounded()
    {
        return playerTransform.gameObject.GetComponent<PlayerMovement>().IsTouching();
    }
}
