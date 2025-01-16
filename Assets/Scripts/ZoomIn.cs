using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    public int zoom = 20;
    public int normal = 60;
    public float smooth = 5;
    private bool isZoomed = false;
    private bool notZoomed = true;


    void Update()

    {

        if (Input.GetKeyDown(KeyCode.LeftControl))

        {
            isZoomed = !isZoomed;
        }
        if (isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isZoomed = !notZoomed;
        }
        if (notZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }
    }
}
