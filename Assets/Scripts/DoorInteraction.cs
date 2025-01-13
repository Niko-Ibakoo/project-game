using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject door; // Assign the door GameObject in the Inspector
    public float openAngle = 90f; // Angle to open the door
    public float openSpeed = 2f; // Speed of opening the door

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("handle"))
                {
                    Debug.Log("Handle clicked");
                    Debug.Log(hit.transform.name);
                }
            }
        }
    }

}
