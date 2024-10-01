using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float xAxisDrag;
    [SerializeField] private float yAxisDrag;
    [SerializeField] private float wallAxisDrag;

    PlayerJumps jumps;
    PlayerWallJump wallJump;

    private Vector2 direction;

    private Rigidbody2D rb;

    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumps = GetComponent<PlayerJumps>();
        wallJump = GetComponent<PlayerWallJump>();
        direction = Vector2.zero;
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(direction.x, 0)*100);
        Vector2 velocity = rb.velocity;
        if (Mathf.Abs(rb.velocity.x) > 2 * maxSpeed && isRunning)
        {
            velocity.x = direction.x * 2 * maxSpeed;
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed && !isRunning)
        {
            velocity.x = direction.x * maxSpeed;
        }
        velocity.x = velocity.x / (xAxisDrag + 1f); // zero = no drag. Start with a small value like 0.05
        velocity.y = velocity.y / (yAxisDrag + 1f);

        if (wallJump.IsOnWall() && velocity.y < 0)
        {
            velocity.y = velocity.y / (wallAxisDrag + 1f);
        }

        rb.velocity = velocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (jumps.IsGrounded())
        {
            direction = context.ReadValue<Vector2>();
        }
        else
        {
            direction = context.ReadValue<Vector2>() / 2;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }
}
