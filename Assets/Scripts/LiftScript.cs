using UnityEngine;
using UnityEngine.UIElements;

public class LiftScript : MonoBehaviour


{

  public GameObject liftObject;

  void Start()
  {
    liftObject = GameObject.Find("Lift");
  }
  void Update()

  {
    Lift();
  }
  public void Lift()
  {


    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out RaycastHit target, 20f))
    {

      if (target.transform.name == "upButton" && Input.GetMouseButtonDown(0))
      {
        liftObject.transform.localScale = new Vector3(liftObject.transform.localScale.x, liftObject.transform.localScale.y + 0.5f, liftObject.transform.localScale.z);
        // Update the collider size
        BoxCollider collider = liftObject.GetComponent<BoxCollider>();
        collider.size = liftObject.transform.localScale;
      }
      else if (target.transform.name == "downButton" && Input.GetMouseButtonDown(0))
      {
        liftObject.transform.localScale = new Vector3(liftObject.transform.localScale.x, liftObject.transform.localScale.y - 0.5f, liftObject.transform.localScale.z);
        /// Update the collider size
        BoxCollider collider = liftObject.GetComponent<BoxCollider>();
        collider.size = liftObject.transform.localScale;
      }

    }
  }

}
