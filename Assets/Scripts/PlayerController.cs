using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject existingArrow; // Reference to the existing arrow in the scene
    public Transform shootingPoint;
    public float arrowSpeed = 10f;
    private bool arrowInAir = false; // Flag to track if the arrow is in the air


    void Update()
    {
        // Shoots an arrow when screen is clicked
        if (Input.GetMouseButtonDown(0) && !arrowInAir)
        {
            ShootArrow();
            arrowInAir = true; // Set the flag to true when an arrow is shot

        }
    }

    void ShootArrow()
    {
        if (existingArrow && shootingPoint)
        {
            
            //instantiate

            GameObject arrow = Instantiate(existingArrow, shootingPoint.position, shootingPoint.rotation);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
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
