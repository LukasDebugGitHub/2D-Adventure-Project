using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    [Header("Key Codes")]
    [SerializeField] KeyCode jumpKey = KeyCode.W;

    [Header("Ground Check")]
    [SerializeField] float playerHeight;
    [SerializeField] float groundDrag;
    [SerializeField] LayerMask whatIsGround;
    bool grounded;

    float horizontalInput;

    Rigidbody player_rb;

    // Start is called before the first frame update
    void Start()
    {
        player_rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();

        if (grounded)
            player_rb.drag = groundDrag;
        else
            player_rb.drag = 0f;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(jumpKey) && grounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        // Move the player around the x Axis
        if (grounded)
            player_rb.AddForce(transform.right * horizontalInput * moveSpeed * 10, ForceMode.Force);
    }

    private void Jump()
    {
        // Make the player jumping in the y direction
        player_rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
