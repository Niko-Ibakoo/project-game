using UnityEngine;

public class SnapPointGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.green;
    public float gizmoSize = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoSize);
    }
}
