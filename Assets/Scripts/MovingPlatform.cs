using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] GameObject start;
    [SerializeField] GameObject end;
    [SerializeField] float speed;
    Vector2 target;
    float maxTimer = 4;
    float timer;
    bool isMoving;
    void Start()
    {
        target = end.transform.position;
        timer = maxTimer;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (GetComponent<Rigidbody2D>().velocity == Vector2.zero)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    ChangeTarget();
                }
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
        other.gameObject.transform.SetParent(null);
    }
}
