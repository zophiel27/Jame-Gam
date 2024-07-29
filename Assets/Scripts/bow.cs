using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bow : MonoBehaviour
{

    public Transform shootingPoint;
    public float arrowSpeed = 10f;
    private bool arrowInAir = false; // Flag to track if the arrow is in the air
    float time_to_dest = 1000;
    float curr_time = 0;
    bool another_arrow_exists = false;
    public GameObject arrow;
    private bool isMouseDown = false;
    private GameScript gameScript;
    private int arrowCount;
    GameObject NewArrow;
    // Start is called before the first frame update
    void Start()
    {
        gameScript = FindObjectOfType<GameScript>();
        arrowCount = gameScript.arrows;
    }
    // Update is called once per frame
    void Update()
    {
        if(isMouseDown){
            Vector2 pos=transform.position;
            Vector2 tapPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector2 mouseOffset = new Vector2(0, 0);
            tapPos += mouseOffset;
            Vector2 direction= pos-tapPos;
            transform.right=direction;
        }
        

        if (another_arrow_exists)
            curr_time++;
        if (time_to_dest <= curr_time)
        {
            curr_time = 0;
            another_arrow_exists = false;
            Destroy(NewArrow);
            arrowInAir = false;
        }
        // Shoots an arrow when screen is clicked
        // if (Input.GetMouseButtonDown(0) && !another_arrow_exists && !arrowInAir)
        // {
        //     ShootArrow();
        //     arrowInAir = true; // Set the flag to true when an arrow is shot
        // }
    }
    
    public void SetMouseDown(bool isDown)
    {
        isMouseDown = isDown;
    }

    public void ShootArrow()
    {
        if (shootingPoint && !another_arrow_exists && !arrowInAir && arrowCount > 0) 
        {
            arrowCount--;
            arrowInAir = true;
            //instantiate
            NewArrow = Instantiate(arrow, shootingPoint.position, shootingPoint.rotation);
            another_arrow_exists = true;
            Rigidbody2D rb = NewArrow.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            if (rb)
            {
                rb.velocity = transform.right * arrowSpeed; // Apply velocity
            }
        }
        else if (arrowCount <= 0)
        {
            Debug.Log("No arrows left");
        }
        
    }
    public void ResetArrowInAir()
    {
        arrowInAir = false; // Reset the flag when called
    }
}