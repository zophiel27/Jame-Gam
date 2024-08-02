using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;
    private GameScript gameScript;
    public bool is_active = true;
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
        gameScript= FindObjectOfType<GameScript>();
        is_active = true;
    }

    void OnCollisionEnter2D(Collision2D collision) // Zophiel alr had a function, dont have to use bren when u have Zophiel carrying u :3
    {
        if (collision.gameObject.CompareTag("Floor")) // Assuming the floor has a tag "Floor"
        {
            FindObjectOfType<bow>().ResetArrowInAir(); // Reset the flag in PlayerController
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            is_active = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            // old method
            // float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // new method
            // Get the normal of the surface we collided with
            Vector2 normal = collision.contacts[0].normal;
            Debug.Log("Normal: " + normal);
            // Reflect the velocity vector off the surface normal
            Vector2 incomingVelocity = rb.velocity;
            Vector2 reflectedVelocity = Vector2.Reflect(incomingVelocity, normal);

            // Assign the reflected velocity to the Rigidbody
            rb.velocity = reflectedVelocity;
        }
        //else if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    FindObjectOfType<bow>().ResetArrowInAir(); // Reset the flag in PlayerController
        //    rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        //    gameScript.EnemyDied();
        //}
    }

    void OnTriggerEnter2D(Collider2D collider) // Uncomment if u still need this code
    {
        if (collider.CompareTag("Enemy"))
        {
            //FindObjectOfType<bow>().ResetArrowInAir();
            //rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            //gameScript.EnemyDied();
        }
    }
}