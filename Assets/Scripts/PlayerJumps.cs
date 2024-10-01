using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    public void OnJump() {
        if (IsGrounded())
        {
            rb.AddForce(jumpImpulse, ForceMode2D.Impulse);
            jumpsLeft--;
        }
        else if (canDoubleJump && jumpsLeft > 0)
        {
            rb.AddForce(1.5f * jumpImpulse, ForceMode2D.Impulse);
            jumpsLeft--;
        }
    }

    bool IsGrounded()
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
}
