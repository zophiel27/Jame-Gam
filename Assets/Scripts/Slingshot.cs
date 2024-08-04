using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] LineRenderers;
    public Transform[] stringPositions;
    public Transform Center;
    public Transform IdlePosition;
    bool isMouseDown;
    private Vector3 currentPosition;
    private bow Bow; //reference to the bow script
    public float power;
    public Vector2 minPower;
    public Vector2 maxPower;
    private Vector2 force;
    public float maxPullDistance; // maximum distance the string can be pulled

    void Start()
    {
        LineRenderers[0].positionCount = 2; 
        LineRenderers[1].positionCount = 2;
        UpdateStringPositions();  
        Bow = FindObjectOfType<bow>(); //finding the script
    }
    
    void Update()
    {
        UpdateStringPositions();
        if(isMouseDown && Bow.checkArrow())
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePos);
            //currentPosition = Center.position + Vector3.ClampMagnitude(currentPosition - Center.position, maxLength);
            currentPosition = ClampToMaxDistance(currentPosition);

            SetStrings(currentPosition);
        }
        else
        {
            ResetStrings();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
        if (Bow != null)
        {
            Bow.SetMouseDown(true);
        }
    }
    private void OnMouseUp()
    {
        isMouseDown = false;
        if (Bow != null)
        {
            Bow.SetMouseDown(false);
            // Vector3 arrowForce = -1 * arrowSpeed * (currentPosition - Center.position);
            // Bow.ShootArrow(arrowForce); 
            force = new Vector2(Mathf.Clamp(Center.position.x - currentPosition.x, minPower.x, maxPower.x), Mathf.Clamp(Center.position.y - currentPosition.y, minPower.y, maxPower.y));
            Vector3 arrowForce = power * force;
            Debug.Log("force: " + force + "\nforce magnitude: " + force.magnitude + " & arrowForce: " + arrowForce);
            Bow.ShootArrow(arrowForce);

        }
    }
    void ResetStrings()
    {
        currentPosition = IdlePosition.position;
        SetStrings(currentPosition);
    }
    void SetStrings(Vector3 position)
    {
        LineRenderers[0].SetPosition(1, position);
        LineRenderers[1].SetPosition(1, position);

    }
    void UpdateStringPositions()
    {
        LineRenderers[0].SetPosition(0, stringPositions[0].position);
        LineRenderers[1].SetPosition(0, stringPositions[1].position);
        // Update the second position of the LineRenderers to the current position of the slingshot
        LineRenderers[0].SetPosition(1, Center.position);
        LineRenderers[1].SetPosition(1, Center.position);
    }
    Vector3 ClampToMaxDistance(Vector3 position)
    {
        Vector3 direction = position - Center.position;
        if (direction.magnitude > maxPullDistance)
        {
            direction = direction.normalized * maxPullDistance;
        }
        return Center.position + direction;
    }
}
