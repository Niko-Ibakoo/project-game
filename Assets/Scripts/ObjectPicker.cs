using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI; //import for UI components


public class ObjectPicker : MonoBehaviour
{

    public Transform holdPoint;
    public float pickUpRange = 2f;
    public LayerMask pickable;
    public GameObject heldObject = null;
    public Image snappablePointer, defaultPointer;

    void Update()
    {


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
            else if (heldObject == null)
            {
                Debug.Log("TYING TO DETACH OBJECT");
                DetachObject();
            }
        }
        // Add this for attaching objects
        if (Input.GetKeyDown(KeyCode.E) && heldObject != null)
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
            snappablePointer.enabled = false;
            defaultPointer.enabled = true;

        }

    }

    void TryPickUpObject()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, pickUpRange, pickable))
        {

            //check if the object has a rigidbody

            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            Collider col = hit.transform.GetComponent<Collider>(); // Get the collider
            if (rb != null)
            {

                heldObject = hit.collider.gameObject;
                col.enabled = false;

                //disable physics while holding the object
                rb.isKinematic = true;

                //set the position and rotation of the hold point to match the object being picked up
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
            // Re-enable the collider
            Collider col = heldObject.GetComponent<Collider>();
            if (rb != null && Input.GetKeyDown(KeyCode.Mouse0))
            {
                rb.isKinematic = false;
                col.enabled = true;
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
            Collider col = heldObject.transform.GetComponent<Collider>(); // Get the collider
            if (rb != null && Input.GetKeyDown(KeyCode.Mouse1))
            {
                rb.isKinematic = false;
                col.enabled = true;
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
            // If the object has an snapPoint component and can attach the heldObject (checks via CanAttach method)
            AttachPoint attachPoint = hit.collider.GetComponent<AttachPoint>();
            if (attachPoint != null)
            {
                // Check if the attach point can attach the held object
                if (attachPoint.CanAttach(heldObject))
                {
                    Debug.Log($"Attaching {heldObject.name} to {hit.collider.name}");
                    // Attach the object
                    attachPoint.Attach(heldObject);
                    heldObject = null;
                }
            }

        }
    }

    //all this does is display a message if the player is looking at a snapping point
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
            if (hit.collider.CompareTag("SnapPoint") && heldObject.CompareTag("Attachable"))
            {
                if (heldObject.name != null && hit.collider.GetComponent<AttachPoint>().itemTag == heldObject.name)
                {
                    snappablePointer.enabled = true;
                    defaultPointer.enabled = false;
                    return;
                }


            }

        }
        snappablePointer.enabled = false;
        defaultPointer.enabled = true;
    }

    void DetachObject()
    {
        //TO DO...
    }
}





