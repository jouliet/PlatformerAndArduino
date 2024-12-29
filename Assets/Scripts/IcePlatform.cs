using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatform : MonoBehaviour
{
    Vector2 slipperyness = new Vector2(30, 0);
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            Debug.Log("slip");
            if (other.attachedRigidbody.linearVelocity.x > 0) {
                other.attachedRigidbody.AddForce(slipperyness);
            }
            else
            {
                other.attachedRigidbody.AddForce(-slipperyness);
            }
        }
    }
}
