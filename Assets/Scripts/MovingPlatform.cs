using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;
    float speed = 7;
    Vector2 target;
    float maxTimer = 4;
    float timer;
    bool isMoving;

    private float previousDistance;
    private float currentDistance;
    void Start()
    {
        target = end.transform.position;
        timer = maxTimer;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (GetComponent<Rigidbody2D>().linearVelocity == Vector2.zero)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    ChangeTarget();
                }
            }
        }*/

        if (isMoving)
        {
            float delta = currentDistance - previousDistance;
            Debug.Log(delta);
            if (delta >= 2 && delta <= 20)
            {
                transform.Translate(speed * Vector3.right * Time.deltaTime);
            }
            if (delta <= -2 && delta >= -20)
            {
                transform.Translate(speed * Vector3.left * Time.deltaTime);
            }
        }

    }

    void ChangeTarget()
    {
        if (transform.position == start.transform.position)
        {
            target = end.transform.position;
        }
        if (transform.position == end.transform.position)
        {
            target = start.transform.position;
        }
        timer = maxTimer;
    }

    public void UpdateDistance(float distance)
    {
        previousDistance = currentDistance;
        currentDistance = distance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
            other.gameObject.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null);
        }
        isMoving = false;
    }
}
