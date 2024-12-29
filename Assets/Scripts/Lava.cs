using UnityEngine;

public class Lava : MonoBehaviour
{
    [SerializeField] float lavaImpulse;
    private float timer = 0;
    private float cooldown = 2;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Time.time > timer)
        {
            timer = Time.time + cooldown;
            other.gameObject.GetComponent<PlayerState>().TakeDamage();
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * lavaImpulse, ForceMode2D.Impulse);
        }
    }
}
