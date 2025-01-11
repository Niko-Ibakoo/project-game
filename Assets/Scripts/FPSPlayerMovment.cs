
using UnityEngine;

public class FPSPlayerMovment : MonoBehaviour
{
    public PlayerJump playerJump;
    public CharacterController controller;
    //gravity for the fall 
    public float speed = 12f;
    public float sprintSpeed = 35f;
    public Vector3 velocity;
    public float gravity = -9.81f;
    //end of gravity for the fall
    
    void Start(){
    playerJump = GetComponent<PlayerJump>();
    }
    // Update is called once per frame
    void Update()
    {

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            
        }

        // Movement logic
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;

        //move is a variable that stores both the horizontal and forward inputs
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        //controller.Move has 3 params the direction, the speed and the time(so that it will move at the same speed regardless of the frame rate)
        controller.Move(move * moveSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        
        // Jump logic
        if (Input.GetKeyDown("space") && controller.isGrounded)
        {
            playerJump.Jump();
        }
     

    }
}
