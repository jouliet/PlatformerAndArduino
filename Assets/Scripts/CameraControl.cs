using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject player;
    [SerializeField] float cameraDrag;
    [SerializeField] float cameraDistance;
    Rigidbody2D rb;
    Vector2 target;
    Vector2 position;
    Vector2 velocity;
    Vector2 offSet;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform.position;
        target += offSet;
        position = transform.position;
        float distance = (position - target).magnitude;
        velocity = rb.linearVelocity;

        rb.AddForce(target - position);
        velocity = velocity / (1f + cameraDrag / distance);
        rb.linearVelocity = velocity;
    }

    public void OnMoveCamera(InputAction.CallbackContext context)
    {
        offSet = context.ReadValue<Vector2>() * cameraDistance;
    }
}
