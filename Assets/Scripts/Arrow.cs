using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;
    void Update()
    {
        if (true) // Check for left mouse button click or screen tap
            {
                Vector2 pos=transform.position;
                Vector2 tapPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction= tapPos-pos;
                transform.right=direction;
            }
    }

    void Start()
    {
        startPosition = transform.position; // Store the original position
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
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
    }
}