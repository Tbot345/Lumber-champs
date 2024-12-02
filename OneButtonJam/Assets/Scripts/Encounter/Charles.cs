using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charles : MonoBehaviour
{
    public float lifetime = 15f;
    public float speed = 2f;                  // Movement speed
    public float viewRadius = 5f;            // Radius of field of view
    public float viewAngle = 90f;            // Angle of field of view
    public float rotationSpeed = 2f;         // Speed of turning Charles toward target

    public LayerMask targetMask;             // Layer for the player
    public LayerMask obstacleMask;           // Layer for obstacles

    private Vector2 movePosition;            // Current target patrol position
    private float stopTime = 2f;             // Time to stop at a target before moving
    private float stopTimeCounter;

    public float minX = -5f, maxX = 5f, minY = -5f, maxY = 5f;

    private float currentFOVRotation;        // Current FOV direction
    private float targetFOVRotation;         // Target FOV direction
    private bool isTurning = true;           // Whether Charles is currently turning to face the target

    private Mesh viewMesh;
    public MeshFilter viewMeshFilter;
    public MeshRenderer viewMeshRenderer;    // Reference to the MeshRenderer for controlling transparency
    public int meshResolution = 10;

    private Material viewMaterial;           // Material used for the FOV visualization

    private TreeChopping player;         // Reference to the player's controller

    void Start()
    {
        // Initialize movement and FOV rotation
        movePosition = GetRandomPatrolPoint();
        currentFOVRotation = 0f;
        targetFOVRotation = currentFOVRotation;

        // Setup view mesh for FOV visualization
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        // Get the material from the MeshRenderer
        viewMaterial = viewMeshRenderer.material;
    }

    void Update()
    {
        // Rotate Charles toward the target before moving
        if (isTurning)
        {
            TurnTowardTarget();
        }
        else
        {
            MoveTowardTarget();
        }

        lifetime -= Time.deltaTime;
        
        if(lifetime <= 0f)
        {
            Destroy(gameObject);
        }

        // Smoothly update FOV rotation to match Charles's facing direction
        currentFOVRotation = Mathf.LerpAngle(currentFOVRotation, transform.eulerAngles.z, rotationSpeed * Time.deltaTime);

        // Draw the updated FOV visualization
        DrawFieldOfView();

        // Check if the player is within the FOV
        DetectPlayer();
    }

    private void TurnTowardTarget()
    {
        Vector2 direction = (movePosition - (Vector2)transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, currentAngle);

        if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle)) < 1f)
        {
            isTurning = false;
        }
    }

    private void MoveTowardTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePosition) < 0.2f)
        {
            if (stopTimeCounter <= 0)
            {
                movePosition = GetRandomPatrolPoint();
                targetFOVRotation = Random.Range(0f, 360f);
                stopTimeCounter = stopTime;
                isTurning = true;
            }
            else
            {
                stopTimeCounter -= Time.deltaTime;
            }
        }
    }

    private Vector2 GetRandomPatrolPoint()
    {
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    private void DrawFieldOfView()
    {
        int rayCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / rayCount;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = currentFOVRotation - viewAngle / 2 + stepAngleSize * i;
            Vector3 dir = DirFromAngle(angle, false);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);

            if (hit.collider)
            {
                viewPoints.Add(hit.point);
            }
            else
            {
                viewPoints.Add(transform.position + dir * viewRadius);
            }
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    private void DetectPlayer()
    {
        // Find all targets in the view radius
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach (var target in targetsInViewRadius)
        {
            Vector2 dirToTarget = (target.transform.position - transform.position).normalized;
            float angleToTarget = Vector2.Angle(DirFromAngle(currentFOVRotation, true), dirToTarget);

            if (angleToTarget < viewAngle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);
                if (!Physics2D.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                {
                    if(target.GetComponent<TreeChopping>().isChopping)
                    {
                        target.GetComponent<Stunnable>().ApplyStun();
                        target.GetComponent<TreeChopping>().playerPoints -= 10;
                        Debug.Log(target.name + "is stunned by Charles");
                        target.GetComponent<TreeChopping>().isChopping = false;
                    }
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += currentFOVRotation;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
