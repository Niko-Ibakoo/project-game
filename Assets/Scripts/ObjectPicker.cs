using System.Xml.Serialization;
using UnityEditor.Callbacks;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    public WeaponScript weaponScript;
    public Transform holdPoint;
    public float pickUpRange = 2f;
    public LayerMask pickupLayer;

    private GameObject heldObject = null;


    // Update is called once per frame
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
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(heldObject != null)
            {
                ThrowObject();
            }
        }

    }

    void TryPickUpObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, pickUpRange, pickupLayer))
        {
            //check if the object has a rigidbody
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            if (rb != null)
            {

                heldObject = hit.collider.gameObject;
                //disable physics while holding the object
                rb.isKinematic = true;

                //parent the object to the hold point
                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;//center it

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
}
