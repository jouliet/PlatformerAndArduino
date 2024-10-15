using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumps : MonoBehaviour
{
    // Start is called before the first frame update
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
        if (!IsGrounded())
        {
            canDoubleJump = true;
        }
    }
    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed && (IsGrounded() || (canDoubleJump && jumpsLeft > 0 && !wallJump.IsOnWall())))
        {
            StartCoroutine(JumpEffect());
            Vector2 velocity = rb.velocity;
            velocity.y = 0;
            rb.velocity = velocity;
            rb.AddForce(jumpImpulse, ForceMode2D.Impulse);
            jumpsLeft--;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(isJumping.transform.position, Vector2.down, distance);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator JumpEffect()
    {
        jumpEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        jumpEffect.SetActive(false);
    }
}
