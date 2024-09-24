using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float xAxisDrag;

    private Vector2 direction;

    private Rigidbody2D rb;

    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = velocity;
        }
        else if (Mathf.Abs(rb.velocity.x) > maxSpeed && !isRunning)
        {
            velocity.x = direction.x * maxSpeed;
            rb.velocity = velocity;
        }
        velocity.x = velocity.x / (xAxisDrag + 1f); // zero = no drag. Start with a small value like 0.05
        rb.velocity = velocity;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        Debug.Log("ouais");
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
