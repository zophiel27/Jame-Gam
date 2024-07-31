using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bow : MonoBehaviour
{
    public Transform shootingPoint;
    private bool arrowInAir = false; // Flag to track if the arrow is in the air
    private float timer;
    bool another_arrow_exists = false;
    public GameObject arrow;
    private bool isMouseDown = false;
    private GameScript gameScript;
    private int arrowCount;
    GameObject NewArrow;
    
    void Start()
    {
        gameScript = FindObjectOfType<GameScript>();
        arrowCount = gameScript.arrows;
    }
    
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
            {
                if (timer > 0)
                    timer -= Time.deltaTime;
                if (timer <= 0)
                   { 
                    another_arrow_exists = false;
                    Destroy(NewArrow);
                    arrowInAir = false;
                    }
            }
    }
    
    public void SetMouseDown(bool isDown)
    {
        isMouseDown = isDown;
    }

    public void ShootArrow(Vector3 force)
    {
        if (shootingPoint && !another_arrow_exists && !arrowInAir && arrowCount > 0) 
        {
            timer=3.5f;
            arrowCount--;
            FindObjectOfType<GameScript>().ArrowFired();
            arrowInAir = true;
            //instantiate
            NewArrow = Instantiate(arrow, shootingPoint.position, shootingPoint.rotation);
            another_arrow_exists = true;
            Rigidbody2D rb = NewArrow.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            if (rb)
            {
                //rb.velocity = transform.right * arrowSpeed; // Apply velocity
                rb.velocity = force;
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
    public bool checkArrow()
    {
        return another_arrow_exists;
    }
}