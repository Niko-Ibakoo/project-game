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
        liftObject.transform.position = new Vector3(liftObject.transform.position.x, liftObject.transform.position.y + 0.5f, liftObject.transform.position.z);
      }
      else if (target.transform.name == "downButton" && Input.GetMouseButtonDown(0))
      {
        
      }

    }
  }

}
