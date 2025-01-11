using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{

    public FPSPlayerMovment playerMovment;
    public float jumpHeight = 5f;

    public void Jump()
    {
        playerMovment.velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerMovment.gravity);
     
    }

}