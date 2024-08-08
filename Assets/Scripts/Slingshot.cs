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
    private bow bow; // Reference to the bow script
    public float power;
    public Vector2 minPower;
    public Vector2 maxPower;
    private Vector2 force;
    public float maxPullDistance; // Maximum distance the string can be pulled

    // Trajectory related variables
    public GameObject pointPrefab;
    public GameObject[] trajectoryPoints;
    public int numberOfTrajectoryPoints = 10;
    public float pointSpacing = 0.03f;
    public Transform shootingPoint;
    void Start()
    {
        LineRenderers[0].positionCount = 2; 
        LineRenderers[1].positionCount = 2;
        UpdateStringPositions();  
        bow = FindObjectOfType<bow>(); // Finding the script in the scene
        // Initialize the trajectory points
        trajectoryPoints = new GameObject[numberOfTrajectoryPoints];
        for (int i = 0; i < numberOfTrajectoryPoints; i++)
        {
            trajectoryPoints[i] = Instantiate(pointPrefab, shootingPoint.position, Quaternion.identity);
            trajectoryPoints[i].SetActive(false); // Initially hide trajectory points
        }
    }
    void Update()
    {
        UpdateStringPositions();
        if(isMouseDown && bow.checkArrow())
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            currentPosition = Camera.main.ScreenToWorldPoint(mousePos);
            currentPosition = ClampToMaxDistance(currentPosition);
            SetStrings(currentPosition);
            // Calculate the force
            force = new Vector2(
                Mathf.Clamp(Center.position.x - currentPosition.x, minPower.x, maxPower.x),
                Mathf.Clamp(Center.position.y - currentPosition.y, minPower.y, maxPower.y)
            );
            // Update the trajectory points
            UpdateTrajectoryPoints(shootingPoint.position, force * power);
        }
        else
        {
            ResetStrings();
            HideTrajectoryPoints();
        }
    }
    private void OnMouseDown()
    {
        isMouseDown = true;
        if (bow != null)
        {
            bow.SetMouseDown(true);
        }
    }
    private void OnMouseUp()
    {
        isMouseDown = false;
        if (bow != null)
        {
            bow.SetMouseDown(false);
            Vector3 arrowForce = power * force;
            Debug.Log("force: " + force + "\nforce magnitude: " + force.magnitude + " & arrowForce: " + arrowForce);
            bow.ShootArrow(arrowForce);
            HideTrajectoryPoints();
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
    void UpdateTrajectoryPoints(Vector2 startPosition, Vector2 velocity)
    {
        for (int i = 0; i < numberOfTrajectoryPoints; i++)
        {
            float t = i * pointSpacing;
            Vector2 pointPosition = startPosition + velocity * t;
            trajectoryPoints[i].transform.position = pointPosition;
            trajectoryPoints[i].SetActive(true); // Show the trajectory points
        }
    }
    void HideTrajectoryPoints()
    {
        foreach (GameObject point in trajectoryPoints)
        {
            point.SetActive(false);
        }
    }
}
