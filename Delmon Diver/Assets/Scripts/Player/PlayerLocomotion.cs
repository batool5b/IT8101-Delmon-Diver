using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    private InputManagement inputManager;
    private Transform cameraObject;
    private Rigidbody playerRigidbody;

    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float rotationSpeed = 15f;

    private Vector3 moveDirection;

    private void Awake()
    {
        inputManager = GetComponent<InputManagement>();
        cameraObject = Camera.main.transform;
        playerRigidbody = GetComponent<Rigidbody>();

        if (playerRigidbody == null) Debug.LogError("NO RIGIDBODY on " + gameObject.name);
        if (inputManager == null) Debug.LogError("NO InputManagement on " + gameObject.name);
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (inputManager.moveAmount <= 0f)
            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0f;

        float speed = inputManager.isRunning ? runSpeed : walkSpeed;

        Vector3 movement = moveDirection * speed * Time.fixedDeltaTime;

        playerRigidbody.MovePosition(playerRigidbody.position + movement);
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection += cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0f;   // rotate only on the horizontal plane

        if (targetDirection == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(
            transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }
}