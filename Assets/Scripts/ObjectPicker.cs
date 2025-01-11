using UnityEngine;
using UnityEngine.UI; //import for UI components


public class ObjectPicker : MonoBehaviour
{

    public Transform holdPoint;
    public float pickUpRange = 10f;
    public LayerMask pickable;
    private GameObject heldObject = null;
    public Image attachMessage;

    //store the layer of the heldObject so I can make it invisible when picked up


    // Update is called once per frame
    void Update()
    {
        // Check if the player is looking at a snapping point

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (heldObject == null)
            {
                TryPickUpObject();

            }
            else
            {
                DropObject();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (heldObject != null)
            {
                ThrowObject();

            }
        }
        // Add this for attaching objects
        if (Input.GetKeyDown(KeyCode.R) && heldObject != null)
        {
            TryAttachObject();
        }

        if (heldObject != null)
        {
            CheckForSnappingPoint();
        }
        else
        {
            // Ensure the attach message is hidden when no object is held
            attachMessage.enabled = false;
        }

    }

    void TryPickUpObject()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, pickUpRange, pickable))
        {
            //check if the object has a rigidbody
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {

                heldObject = hit.collider.gameObject;

                //disable physics while holding the object
                rb.isKinematic = true;

                //set the position and rotation of the hold point to match the object being ppicked up
                holdPoint.position = hit.transform.position;
                holdPoint.rotation = hit.transform.rotation;

                //parent the object to the hold point
                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;//center it

                //Resets the rotation relative to the holdPoint, ensuring the object maintains its original orientation.
                heldObject.transform.localRotation = Quaternion.identity;




            }
        }
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            // Re-enable physics
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null && Input.GetKeyDown(KeyCode.Mouse0))
            {
                rb.isKinematic = false;
            }

            // Unparent the object
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }

    void ThrowObject()
    {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null && Input.GetKeyDown(KeyCode.Mouse1))
            {
                rb.isKinematic = false;
                heldObject.transform.SetParent(null);
                heldObject = null;
                rb.AddForce(Camera.main.transform.forward * 30f, ForceMode.Impulse);
            }

        }
    }

    void TryAttachObject()
    {
        //the ray we shoot from player to forward or wherever the mouse position is
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //check if the ray hits anything within the specified range (pickupRange)
        if (Physics.Raycast(ray, out RaycastHit hit, pickUpRange))
        {
            // If the object has an AttachPoint component and can attach the heldObject (checks via CanAttach method)
            AttachPoint attachPoint = hit.transform.GetComponent<AttachPoint>();
            if (attachPoint != null)
            {
                // Call the Attach method on the AttachPoint to attach the held object
                attachPoint.Attach(heldObject);
                // Clear the reference to the heldObject, as it is now attached and no longer being held
                heldObject = null;
            }
        }
    }

    void CheckForSnappingPoint()
    {
        //this is a very basic snapping point check, all it does it checks if the camera is looking at an object with the SnapPoint tag
        //Ideally it should check if the heldobject and the snapping point are compatible e.g only attach a tire to a tiresnap, instead of attaching anythingto a snap
        // I could create a tag and compare the tags of the held object and the snapping point if they are the same then it can snap.
        //e.g radiatorSnap for the snap and the same for the radiator
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if the camera is looking at an object with the SnapPoint tag, show the attach message
        if (Physics.Raycast(ray, out RaycastHit hit, pickUpRange))
        {
            //check the tags here and then enable or disable the attach message
            if (hit.transform.CompareTag("SnapPoint"))
            {
                Debug.Log("you are looking at a snapping point");
                attachMessage.enabled = true;
                return;
            }

        }
        attachMessage.enabled = false;
    }
}
