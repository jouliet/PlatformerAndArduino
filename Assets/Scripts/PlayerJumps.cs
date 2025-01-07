using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerJumps : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Vector2 jumpImpulse;
    [SerializeField] GameObject isJumping;
    float distance = 0.1f;
    [SerializeField] int maxJumps;
    int jumpsLeft;
    bool canDoubleJump = false;

    [SerializeField] GameObject jumpEffect;

    PlayerWallJump wallJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallJump = GetComponent<PlayerWallJump>();
        jumpsLeft = maxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            canDoubleJump = false;
            jumpsLeft = maxJumps;
        }
        if (!IsGrounded() && jumpsLeft > 0)
        {
            canDoubleJump = true;
        }
    }
    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed && IsGrounded() || context.performed && (canDoubleJump && !wallJump.IsOnWall()))
        {
            if (jumpsLeft > 0)
            {
                jumpsLeft -= 1;
                Jump();
            }
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(isJumping.transform.position, Vector2.down, distance);
        if (hit.collider != null && hit.collider.CompareTag("Platform"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Jump()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = 0;
        rb.linearVelocity = velocity;
        rb.AddForce(jumpImpulse, ForceMode2D.Impulse);
        StartCoroutine(JumpEffect());
    }

    IEnumerator JumpEffect()
    {
        jumpEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        jumpEffect.SetActive(false);
    }
}
