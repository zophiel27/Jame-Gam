using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;
    private GameScript gameScript;
    public bool is_active = true;
    private int bounceCount = 0;
    public float speed = 25f;
    public float fallThreshold = 0f; // defining a threshold for fallen chains
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
        rb.velocity = transform.right * speed;
        gameScript= FindObjectOfType<GameScript>();
        is_active = true;
    }

    void OnCollisionEnter2D(Collision2D collision) // Zophiel alr had a function, dont have to use bren when u have Zophiel carrying u :3
    {
        if (collision.gameObject.CompareTag("Floor")) // Assuming the floor has a tag "Floor"
        {
            stopArrow();
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Boulder"))
        {
            if (bounceCount >= 5)
            {
                stopArrow();
                bounceCount = 0;
            }
            else
            {
                Vector2 normal = collision.contacts[0].normal;
                Debug.Log("Normal: " + normal);
                Vector2 reflectDir = Vector2.Reflect(rb.velocity, normal);
                rb.velocity = reflectDir;
                bounceCount++;
            }    
        }
        else if (collision.gameObject.CompareTag("Chain"))
        {
            Debug.Log("Chain hit");
            Destroy(gameObject);
            HingeJoint2D hinge = collision.gameObject.GetComponent<HingeJoint2D>();
            if (hinge != null)
            {
                Destroy(hinge);
        
                GameObject chainParent = collision.gameObject.transform.parent.gameObject;
                Destroy(collision.gameObject, 1f);
                Destroy(chainParent, 1f);

            }
            
        }
    }
private void stopArrow(){
    
    FindObjectOfType<bow>().ResetArrowInAir(); // Reset the flag in PlayerController
    rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    is_active = false;
}
}