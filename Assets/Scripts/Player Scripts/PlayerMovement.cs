using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;
    private Vector3 moveDirection;
    public float speed = 5.0f;
    private float gravity = 20.0f;
    public float jumpForce = 10.0f;
    private float verticalVelocity;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    } 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Vector3 holds x, y, z. We're taking input forward and back, left and right. (x and z).
        // We're not moving up and down, so we're setting y to 0.
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL));

        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= speed * Time.deltaTime;

        ApplyGravity();

        characterController.Move(moveDirection);
    }

    void ApplyGravity()
    {
        verticalVelocity = verticalVelocity - gravity * Time.deltaTime;
        // jump
        PlayerJump();

        moveDirection.y = verticalVelocity * Time.deltaTime;
    } 

    void PlayerJump()
    {
        if(characterController.isGrounded && Input.GetKey(KeyCode.Space))
        {
            verticalVelocity = jumpForce;

            

        }
    }
}