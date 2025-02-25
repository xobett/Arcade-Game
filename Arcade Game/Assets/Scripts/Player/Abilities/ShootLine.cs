using UnityEngine;

public class ShootLine : MonoBehaviour
{
    //Line used to visualize player shooting ability.
    [SerializeField] private LineRenderer line;

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
        if (Input.GetMouseButton(0))
        {
            LineDrawing();
            LineRaycast();
        }
        //If player is stopped holding left mouse clic, it will reset the shooting line positions.
        else if (Input.GetMouseButtonUp(0))
        {
            ResetLinePositions();
        }
    }

    private void LineRaycast()
    {



        //Corregir raycast, porque no esta apuntando bien.


        //Creates a Vector3 to store the direction the raycast will go to, by substracting the mouse position with the player position.
        Vector3 raycastDirection = GetMousePosition() - transform.position;

        //Creates a raycast from the player to the mouse position.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection);

        if (hit)
        {
            Debug.Log($"{hit.collider.name}");
        }

        //Draws the raycast from the player to the mouse position.
        Debug.DrawRay(transform.position, raycastDirection, Color.cyan);
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
    }
}
