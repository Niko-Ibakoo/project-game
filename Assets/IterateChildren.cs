using UnityEngine;
public class InspectorButtonExample : MonoBehaviour
{
    public GameObject parentObject;

    [ContextMenu("Create Snap Point")]
    void IterateChildren()
    {
        if (parentObject != null)
        {
            foreach (Transform child in parentObject.transform)
            {
                //create a new GameObject
                GameObject snapObject = new GameObject(child.name + "Snap");

                //set it's position to match the child
                snapObject.transform.position = child.transform.position;

                //set rotation to match the child
                snapObject.transform.rotation = child.transform.rotation;

                //set scale to match the child
                snapObject.transform.localScale = child.transform.localScale;

                //set it's parent to be the same as the child's parnt 
                snapObject.transform.parent = child.transform.parent;
                // ensure it  is at the same hirarchy level as the chid
                snapObject.transform.SetSiblingIndex(child.transform.GetSiblingIndex() + 1);
            }
        }

    }
}

