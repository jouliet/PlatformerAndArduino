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
    [SerializeField] float jumpTimer = 0;
    bool canDoubleJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnJump() {
        RaycastHit2D hit = Physics2D.Raycast(isJumping.transform.position, Vector2.down, distance);
        if (hit.collider != null)
        {
            rb.AddForce(jumpImpulse);
            jumpTimer = Time.time + 2;
            canDoubleJump = true;
            //Debug.Log("jump!");
        }
        if (canDoubleJump && Time.time <= jumpTimer)
        {
            rb.AddForce(jumpImpulse);
            canDoubleJump = false;
            //Debug.Log("Double jump!");
        }
    }
}
