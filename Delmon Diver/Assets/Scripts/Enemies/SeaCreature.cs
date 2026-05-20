using UnityEngine;

public class SeaCreatureAroundBoat : MonoBehaviour
{
    [Header("Boat")]
    public Transform boat;

    [Header("UI")]
    public DangerUI dangerUI; 

    [Header("Detection")]
    public float appearDistance = 60f;
    public float stopDistance = 90f;

    [Header("Circle In Front Of Boat")]
    public float circleRadius = 10f;
    public float circleSpeed = 2f;
    public float moveSpeed = 8f;
    public float distanceInFront = 15f; 

    [Header("Height")]
    public bool useStartY = true;
    public float customY = 1.2f;

    [Header("Rotation")]
    public bool rotateWithMovement = false;
    public bool yAxisOnly = true;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private float circleY;
    private Vector3 circleCenter;
    private float angle = 0f;
    private bool isCircling = false;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        circleY = useStartY ? startPosition.y : customY;
        Debug.Log("Sea creature ready to circle in front of boat!");
    }

    void Update()
    {
        if (boat == null || dangerUI == null)
            return;

        float distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(boat.position.x, 0, boat.position.z)
        );

        if (!isCircling && distance <= appearDistance)
            StartCircling();

        if (isCircling && distance >= stopDistance)
            StopCircling();

        if (isCircling)
            CircleInFrontOfBoat();
    }

    void StartCircling()
    {
        isCircling = true;
        Vector3 boatForward = boat.forward;
        circleCenter = boat.position + boatForward * distanceInFront;
        circleCenter.y = circleY;
        angle = 0f;
        dangerUI.ShowDanger();
    }

    void StopCircling()
    {
        isCircling = false;
        dangerUI.HideDanger();
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    void CircleInFrontOfBoat()
    {
        angle += circleSpeed * Time.deltaTime;

        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;

        Vector3 targetPosition = new Vector3(
            circleCenter.x + x,
            circleY,
            circleCenter.z + z
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (!rotateWithMovement)
        {
            transform.rotation = startRotation;
            return;
        }

        if (yAxisOnly)
        {
            Vector3 direction = targetPosition - transform.position;
            if (direction != Vector3.zero)
            {
                float targetY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(startRotation.eulerAngles.x, targetY, startRotation.eulerAngles.z);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
            }
        }
    }
}