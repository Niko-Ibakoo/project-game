using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform equipPosition;
    public float distance =10f;
    GameObject currentWeapon;
    RaycastHit hit;
    bool canGrab;
    public float throwForce = 10f;
    // Update is called once per frame

 void Update()
    {
        CheckWeapon();
        if (canGrab && Input.GetKeyDown(KeyCode.E))
        {
         
            GrabWeapon(hit);
            
        }
        if (currentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DropWeapon();
            }
 
        }
        ThrowWeapon();
    }
    //with this function we can check if the player can grab a weapon by shooting a raycast in front of the player
   public void CheckWeapon()
{
    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distance))
    {
        if (hit.transform.CompareTag("canGrab"))
        {
           
            canGrab = true;
        }
        else
        {
            canGrab = false;
        }
    }
}
  public void GrabWeapon(RaycastHit hit)
{
    currentWeapon = hit.transform.gameObject;
    currentWeapon.transform.position = equipPosition.position;
    currentWeapon.transform.parent = equipPosition;
    currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
    
}
    public void DropWeapon()
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon = null;
    }

    public void ThrowWeapon()
    {
        if(currentWeapon != null && Input.GetMouseButtonDown(1))
        {
            //play throw sound
            currentWeapon.transform.parent = null;
            currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
            currentWeapon.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
            currentWeapon = null;
        }
    {
        
    }
        
         
    }
    
}
