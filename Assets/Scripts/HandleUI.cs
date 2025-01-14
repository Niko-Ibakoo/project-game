using UnityEngine;
using TMPro;
public class HandleUI : MonoBehaviour
{
    public static void HandleUIText(TextMeshProUGUI itemName, float pickUpRange)
    {
        if (itemName != null)
        {
            itemName.text = ""; // Clear the item name by default
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, pickUpRange))
            {

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Pickable") || hit.collider.CompareTag("canGrab"))
                {
                    itemName.text = hit.collider.gameObject.name;

                }

            }
        }
        else
        {
            Debug.Log("Item name is null whic is :" + itemName);
        }

    }
}
