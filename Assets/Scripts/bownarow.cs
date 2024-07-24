using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bownarow : MonoBehaviour
{

    public GameObject arrow;
    public float arrowSpeed = 40f;
    public Transform shootingPoint;
    float time_to_dest = 500;
    float curr_time = 0;
    // Update is called once per frame
    void Update()
    {
        Vector2 pos=transform.position;
        Vector2 tapPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction= tapPos-pos;
        transform.right=direction;

        
        curr_time++;
        if (time_to_dest <= curr_time)
        {
            curr_time = 0;
            Destroy(arrow);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ShootArrow();
        }
    }

    void ShootArrow()
    {
        if (shootingPoint)
        {
            GameObject newArrow = Instantiate(arrow, shootingPoint.position, shootingPoint.rotation);
            Rigidbody2D rb = newArrow.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            if (rb)
            {
                rb.velocity = transform.right * arrowSpeed; // Apply velocity
            }
        }
    }
    
}
