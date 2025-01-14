using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    public string itemTag;

    public bool CanAttach(GameObject objToAttach)
    {
        RequiredTag tagObj = objToAttach.GetComponent<RequiredTag>();
        if (tagObj != null)
        {
            // Check if the object has a valid type for attaching
            return objToAttach.CompareTag("Attachable") && tagObj.requiredTag == itemTag; // Modify as needed
        }
        return false;
    }

    public void Attach(GameObject objToAttach)
    {
        // Align the object to the exact position and rotation of the AttachPoint
        objToAttach.transform.position = transform.position;
        objToAttach.transform.rotation = transform.rotation;

        // Parent the object to the AttachPoint to maintain alignment
        objToAttach.transform.SetParent(transform);


        //disable physics
        Rigidbody rb = objToAttach.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        //in Unity, gameObject refers to the GameObject that the current script is attached to.
        //in this case is SnapPoint which is where the wheel would go 
        Debug.Log(objToAttach.name + " attached to " + gameObject.name);
    }
}