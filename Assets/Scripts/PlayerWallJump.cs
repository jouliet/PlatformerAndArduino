using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallJump : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    [SerializeField] Vector2 jumpImpulse;
    int direction;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    [SerializeField] float delay;
    float distance = 0.1f;
    float timeSinceLast;
    [SerializeField] int maxJumps;
    int jumpsLeft;
    bool canWallJump = false;

    PlayerJumps jumps;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = maxJumps;
        jumps = GetComponent<PlayerJumps>();
        timeSinceLast = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnWall())
        {
            canWallJump = true;
            jumpsLeft = maxJumps;
        }
        else
        {
            canWallJump = false;
        }

        timeSinceLast += Time.deltaTime;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canWallJump && !jumps.IsGrounded() && timeSinceLast > delay)
        {
            rb.AddForce(jumpImpulse + (direction * Vector2.right), ForceMode2D.Impulse);
            jumpsLeft--;
            timeSinceLast = 0;
        }
    }

    public bool IsOnWall()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left.transform.position, Vector2.left, distance);
        RaycastHit2D rightHit = Physics2D.Raycast(right.transform.position, Vector2.right, distance);
        if (leftHit.collider != null)
        {
            Debug.Log("left wall");
            direction = 5;
            return true;
        }
        else if (rightHit.collider != null)
        {
            Debug.Log("right wall");
            direction = -5;
            return true;
        }
        else
        {
            Debug.Log("is not on wall");
            return false;
        }
    }
}
