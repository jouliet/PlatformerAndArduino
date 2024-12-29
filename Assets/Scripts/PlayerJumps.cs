using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerJumps : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer render;

    Color defaultColor;
    Color targetColor;
    bool activateGradient = false;
    float gradient = 0;

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
        render = gameObject.GetComponent<SpriteRenderer>();
        defaultColor = render.color;
        targetColor = new Color(1, 0.05786445f, 1, 1);
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
        if (activateGradient)
        {
            gradient += Time.deltaTime * 1.2f;
            render.color = Color32.Lerp(defaultColor, targetColor, gradient);
        }
    }
    public void OnJump(InputAction.CallbackContext context) {
        if (IsGrounded() || (canDoubleJump && !wallJump.IsOnWall()))
        {
            if (context.performed && context.duration < 0.3f)
            {
                Jump();
            }
            if (context.interaction is HoldInteraction)
            {
                activateGradient = true;
            }
            if (context.canceled && context.interaction is HoldInteraction)
            {
                ChargedJump((float)context.duration);
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
        if (jumpsLeft > 0)
        {
            Vector2 velocity = rb.linearVelocity;
            velocity.y = 0;
            rb.linearVelocity = velocity;
            rb.AddForce(jumpImpulse, ForceMode2D.Impulse);
            StartCoroutine(JumpEffect());
            jumpsLeft--;
        }
    }

    void ChargedJump(float duration)
    {
        if (jumpsLeft > 0)
        {
            float charge = Mathf.Clamp(1.7f * duration, 1, 2);
            rb.AddForce(jumpImpulse * charge, ForceMode2D.Impulse);
            StartCoroutine(JumpEffect());
            jumpsLeft = 0;
            activateGradient = false;
            render.color = defaultColor;
            gradient = 0;
        }
    }

    IEnumerator JumpEffect()
    {
        jumpEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        jumpEffect.SetActive(false);
    }
}
