using UnityEngine;

public class simpleplayercontroller : MonoBehaviour
{
public float moveSpeed = 10f;
    public float rotationSpeed = 100f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get WASD / Arrow Key Input
        float moveForward = Input.GetAxis("Vertical"); 
        float turn = Input.GetAxis("Horizontal");

        // Calculate Movement
        Vector3 movement = transform.forward * moveForward * moveSpeed * Time.fixedDeltaTime;
        
        // Apply Movement to Rigidbody
        rb.MovePosition(rb.position + movement);

        // Handle Rotation (Turning left/right)
        Quaternion turnRotation = Quaternion.Euler(0f, turn * rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
