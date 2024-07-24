using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject arrowPrefab;
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
        if (Input.GetMouseButtonDown(0) && !another_arrow_exists && !arrowInAir)
        {
            ShootArrow();
            arrowInAir = true; // Set the flag to true when an arrow is shot
        }
    }

    void ShootArrow()
    {
        if (shootingPoint)
        {
            //instantiate
            arrow = Instantiate(arrowPrefab, shootingPoint.position, shootingPoint.rotation);
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
