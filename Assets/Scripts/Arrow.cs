using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;
    public BombScript bombScript;
    void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Start()
    {
        startPosition = transform.position; // Store the original position
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) // Zophiel alr had a function, dont have to use bren when u have Zophiel carrying u :3
    {
        if (collision.gameObject.CompareTag("Floor")) // Assuming the floor has a tag "Floor"
        {
            FindObjectOfType<PlayerController>().ResetArrowInAir(); // Reset the flag in PlayerController
            rb.velocity = Vector2.zero; // Reset velocity
            rb.angularVelocity = 0f; // Reset angular velocity if any
            transform.position = startPosition; // Reset position
            transform.rotation = Quaternion.identity; // Reset rotation if needed
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (collision.gameObject.CompareTag("Bomb"))
            bombScript.Explode();
    }
}