using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformInteraction : MonoBehaviour
{
    private GameObject currentPlatform;

    [SerializeField] private BoxCollider2D playerCollider;

    private bool crouching;

    private void Update()
    {
        if (crouching)
        {
            Drop();
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        if (value.y < -0.5f)
        {
            crouching = true;
        }
        else
        {
            crouching = false;
        }
    }

    private void Drop()
    {
        if (currentPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            currentPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
