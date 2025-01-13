using UnityEngine;

public class AttachPoint : MonoBehaviour
{
    public bool CanAttach(GameObject objToAttach)
    {
        return objToAttach.CompareTag("Attachable"); //this should be dynamic
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