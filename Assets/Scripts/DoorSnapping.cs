using UnityEngine;

public class DoorSnapping : MonoBehaviour
{
    public Rigidbody carFrameRb; // Reference to the car frame's Rigidbody
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))

        {
            Debug.Log("TRYING TO ATTACH DOOR");



            // Align the door to the snap point
            other.transform.position = transform.position;
            other.transform.rotation = transform.rotation;

            // Disable collisions temporarily
            Physics.IgnoreCollision(other.GetComponent<Collider>(), carFrameRb.GetComponent<Collider>(), true);

            // Configure or add HingeJoint
            HingeJoint hinge = other.GetComponent<HingeJoint>();
            if (hinge == null)
            {
                hinge = other.gameObject.AddComponent<HingeJoint>();
            }
            hinge.connectedBody = carFrameRb;
            hinge.anchor = new Vector3(0f, 0f, 0.43f);
            hinge.axis = Vector3.up;

            JointLimits limits = hinge.limits;
            limits.min = 0f;
            limits.max = 90f;
            hinge.limits = limits;
            hinge.useLimits = true;

            // Re-parent the door to the car frame
            other.transform.SetParent(carFrameRb.transform);

            // Re-enable collisions
            Physics.IgnoreCollision(other.GetComponent<Collider>(), carFrameRb.GetComponent<Collider>(), false);


        }
   
    }
}