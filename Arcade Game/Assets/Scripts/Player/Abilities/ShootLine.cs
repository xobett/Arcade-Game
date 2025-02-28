using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ShootLine : MonoBehaviour
{
    //Line used to visualize player shooting ability.
    [SerializeField] private LineRenderer line;

    [SerializeField] private float lineLifetime;
    [SerializeField] private float lineDuration = 0;

    void Start()
    {
        //Gets line component.
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        //If player is holding left mouse clic, it will draw the shooting line.
        if (IsShooting())
        {
            Line();
        }
        //If player is stopped holding left mouse clic, it will reset the shooting line positions.
        else if (Input.GetMouseButtonUp(0))
        {
            ResetLinePositions();
        }
    }

    private void Line()
    {
        LineDrawing();
        LineRaycast();

        lineDuration += Time.deltaTime;
    }

    private void LineRaycast()
    {
        //Creates a Vector3 to store the direction the raycast will go to, by substracting the mouse position with the player position.
        Vector3 raycastDirection = GetMousePosition() - transform.position;

        //Creates a raycast from the player to the mouse position. Uses the raycast direction vector magnitude to specify the distance of the ray, otherwise it will be infinite.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, raycastDirection.magnitude);
        
        //Checks if a collision occurred within the raycast line.
        if (hit)
        {
            //Checks the collision game object contains the tag Enemy.
            if (hit.collider.CompareTag("Enemy"))
            {
                //Creates a gameobject containing the enemy, for better code readbility.
                GameObject enemy = hit.collider.gameObject;
                //Destroys the enemy
                Destroy(enemy);
            } 
        }

        //Draws the raycast from the player to the mouse position.
        Debug.DrawRay(transform.position, raycastDirection, Color.cyan);
    }

    private bool CanShootLine()
    {
        return lineDuration < lineLifetime;
    }


    private void LineDrawing()
    {
        //Sets line position count to 2.
        line.positionCount = 2;
        //Starting point of the line starts at the player's position.
        line.SetPosition(0, transform.position);
        //Ending point of the line ends at the mouse position.
        line.SetPosition(1, GetMousePosition());
    }

    private Vector3 GetMousePosition()
    {
        //Creates a Vector3 with the mouse position in the world position.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //To avoid the mouse position from going forward in the Z axis, it sets the position to the near camera clip plane.
        mousePosition.z = Camera.main.nearClipPlane;

        //Returns the mouse position created.
        return mousePosition;
    }

    private void ResetLinePositions()
    {
        //Resets the line points to 0.
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);

        lineDuration = 0;
    }

    private bool IsShooting()
    {
        return Input.GetMouseButton(0);
    }

}
