using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 targetPosition;
    public bool cooldown = false;
    GameObject otherPortal;
    void Start()
    {
        Transform parent = transform.parent;
        if (gameObject.CompareTag("Portal1"))
        {
            otherPortal = parent.Find("Portal2").gameObject;
            targetPosition = new Vector2(parent.Find("Portal2").position.x, parent.Find("Portal2").position.y);
        }
        else
        {
            otherPortal = parent.Find("Portal1").gameObject;
            targetPosition = new Vector2(parent.Find("Portal1").position.x, parent.Find("Portal2").position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !cooldown)
        {
            otherPortal.GetComponent<Portal>().cooldown = true;
            other.attachedRigidbody.MovePosition(targetPosition);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            cooldown = false;
        }
    }
}
