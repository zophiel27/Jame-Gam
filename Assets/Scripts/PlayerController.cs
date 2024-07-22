using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject existingArrow; // Reference to the existing arrow in the scene
    public Transform shootingPoint;
    public float arrowSpeed = 10f;
    private bool arrowInAir = false; // Flag to track if the arrow is in the air
    float time_to_dest = 500;
    float curr_time = 0;
    bool another_arrow_exists = false;
    GameObject arrow;

    void Update()
    {

        if (another_arrow_exists)
            curr_time++;
        if (time_to_dest <= curr_time)
        {
            curr_time = 0;
            another_arrow_exists = false;
            Destroy(arrow);
            arrowInAir = false;
        }
        // Shoots an arrow when screen is clicked
        if (Input.GetMouseButtonDown(0) && !arrowInAir)
        {
            if (Input.GetMouseButtonDown(0)) // Check for left mouse button click or screen tap
            {
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convert screen to world position
                clickPosition.z = 0; // Set z to 0 since we're working in 2D

                Vector3 direction = clickPosition - existingArrow.transform.position; // Calculate direction from arrow to click position
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate angle in degrees

                existingArrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-48)); // Rotate the arrow to face the click position
            }
            ShootArrow();
            arrowInAir = true; // Set the flag to true when an arrow is shot

        }
    }

    void ShootArrow()
    {
        if (existingArrow && shootingPoint)
        {

            //instantiate

            arrow = Instantiate(existingArrow, shootingPoint.position, shootingPoint.rotation);


            another_arrow_exists = true; 
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            //delete existing arrow
            //Destroy(existingArrow);
            if (rb)
            {
                rb.velocity = transform.right * arrowSpeed; // Apply velocity
            }
        }
    }
    public void ResetArrowInAir()
    {
        arrowInAir = false; // Reset the flag when called
    }
}
