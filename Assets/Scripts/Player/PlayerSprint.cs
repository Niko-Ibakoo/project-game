using UnityEngine;

public class PlayerSprint : MonoBehaviour
{
    public FPSPlayerMovment playerMovment;
    public float sprintSpeed = 25f;
    public float walkingSpeed = 10f;


    public void StartSprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Sprinting");
            playerMovment.movmentSpeed = sprintSpeed;
        }
        else
        {
            playerMovment.movmentSpeed = walkingSpeed;
        }

    }
}
