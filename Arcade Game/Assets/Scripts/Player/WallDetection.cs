using UnityEngine;

public class WallDetection : MonoBehaviour
{
    [SerializeField, Range(0, 5)] private float detectionDistance;
    
    [SerializeField] private LayerMask whatIsWall;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detection();  
    }

    private void Detection()
    {
        Vector3 rightDirection = transform.right * detectionDistance;
        Vector3 leftDirection = -transform.right * detectionDistance;

        Debug.DrawRay(transform.position, rightDirection, Color.magenta);
        Debug.DrawRay(transform.position, leftDirection, Color.magenta);

        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, rightDirection, detectionDistance, whatIsWall);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, leftDirection, detectionDistance, whatIsWall);

        if (rightHit || leftHit)
        {
            Camera.main.GetComponent<CameraFollow>().playerIsNearWall = true;
        }
        else
        {
            Camera.main.GetComponent<CameraFollow>().playerIsNearWall = false;
        }


    }
}
