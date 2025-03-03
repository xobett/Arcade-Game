using UnityEngine;

public class ShootPoint : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
