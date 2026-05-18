using UnityEngine;

public class SeaCreatureAroundBoat : MonoBehaviour
{
    [Header("Boat")]
    public Transform boat; //drag the boat here

    [Header("UI")]
    public GameObject dangerText; //drag danger text here

    [Header("Detection")]
    public float appearDistance = 60f; //boat close enough to activate
    public float stopDistance = 90f; //boat far enough to stop

    [Header("Circle In Same Area")]
    public float circleRadius = 10f; //circle size around original position
    public float circleSpeed = 2f; //circle speed
    public float moveSpeed = 8f; //movement smoothness

    [Header("Height")]
    public bool useStartY = true;
    public float customY = 1.2f; //use this if creature is too high/low

    private Vector3 startPosition; //original creature position
    private Vector3 circleCenter;  //center of the circle
    private float circleY;
    private float angle = 0f;
    private bool isCircling = false;

    void Start()
    {
        //save original creature position
        startPosition = transform.position;

        //circle will stay around this same original position
        circleY = useStartY ? startPosition.y : customY;

        circleCenter = new Vector3(
            startPosition.x,
            circleY,
            startPosition.z
        );

        if (dangerText != null)
            dangerText.SetActive(false);

        Debug.Log("Sea creature circle center: " + circleCenter);
    }

    void Update()
    {
        if (boat == null)
        {
            Debug.LogWarning("Boat is not assigned!");
            return;
        }

        //check distance between boat and creature area using X/Z only
        float distance = Vector2.Distance(
            new Vector2(circleCenter.x, circleCenter.z),
            new Vector2(boat.position.x, boat.position.z)
        );

        //boat near creature area
        if (!isCircling && distance <= appearDistance)
        {
            StartCircling();
        }

        //boat far from creature area
        if (isCircling && distance >= stopDistance)
        {
            StopCircling();
        }

        if (isCircling)
        {
            CircleInSamePlace();
        }
    }

    void StartCircling()
    {
        isCircling = true;

        if (dangerText != null)
            dangerText.SetActive(true);

        Debug.Log("DANGER! Creature is circling in its area.");
    }

    void StopCircling()
    {
        isCircling = false;

        if (dangerText != null)
            dangerText.SetActive(false);

        //return to original position
        transform.position = startPosition;

        Debug.Log("Creature stopped circling.");
    }

    void CircleInSamePlace()
    {
        angle += circleSpeed * Time.deltaTime;

        float x = Mathf.Cos(angle) * circleRadius;
        float z = Mathf.Sin(angle) * circleRadius;

        //circle around original position, NOT around the boat
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

        //face movement direction
        Vector3 direction = targetPosition - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                5f * Time.deltaTime
            );
        }
    }
}