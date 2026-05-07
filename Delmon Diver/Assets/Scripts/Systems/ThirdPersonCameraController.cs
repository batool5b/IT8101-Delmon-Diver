using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

/// <summary>
/// Third Person Camera Controller - Works with Cinemachine Virtual Camera
/// Rotates the CameraPivot based on mouse/gamepad input
/// Yaw (left/right) rotates the pivot around Y axis
/// Pitch (up/down) rotates the pivot around X axis locally
/// </summary>
public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    public Transform cameraPivot;

    [Header("Mouse Settings")]
    public float mouseSensitivity = 1.5f;

    [Header("Gamepad Settings")]
    public float gamepadSensitivity = 100f;

    [Header("Pitch Constraints")]
    public float minPitch = -40f;
    public float maxPitch = 75f;

    [Header("Options")]
    public bool invertY = false;
    public bool lockCursor = true;

    private float pitch;
    private InputManagement inputManager;

    private void Awake()
    {
        // Get reference to InputManagement (should be on same GameObject as PlayerManager)
        inputManager = GetComponent<InputManagement>();
        if (inputManager == null)
            Debug.LogError("InputManagement not found on this GameObject!");

        // Find cameraPivot if not assigned
        if (cameraPivot == null)
        {
            cameraPivot = transform.Find("CameraPivot");
            if (cameraPivot == null)
                Debug.LogError("CameraPivot not found! Assign it in Inspector or create child named 'CameraPivot'");
        }

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        Debug.Log("ThirdPersonCameraController initialized");
    }

    private void Update()
    {
        if (inputManager == null)
            return;

        // Read separate LookX and LookY from InputManagement
        Vector2 lookInput = inputManager.GetLookInput();
        float lookX = lookInput.x;
        float lookY = lookInput.y;

        // Debug to see if values are being read
        if (lookX != 0 || lookY != 0)
        {
            Debug.Log($"LookX: {lookX}, LookY: {lookY}");
        }

        // Detect if using mouse (large delta values) or gamepad (small -1 to 1 values)
        bool usingMouse = Mathf.Abs(lookX) > 2f;

        // Apply sensitivity based on input device
        float yawInput, pitchInput;

        if (usingMouse)
        {
            // Mouse delta: don't multiply by deltaTime, it's already per-frame
            yawInput = lookX * mouseSensitivity;
            pitchInput = lookY * mouseSensitivity;
        }
        else
        {
            // Gamepad stick: -1 to 1, needs deltaTime scaling
            yawInput = lookX * gamepadSensitivity * Time.deltaTime;
            pitchInput = lookY * gamepadSensitivity * Time.deltaTime;
        }

        // Apply pitch (up/down rotation around X axis locally)
        pitch += invertY ? pitchInput : -pitchInput;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Apply rotation to CameraPivot
        // Yaw around Y axis (world), Pitch around X axis (local)
        if (cameraPivot != null)
        {
            // Yaw: rotate around world Y axis
            cameraPivot.RotateAround(cameraPivot.position, Vector3.up, yawInput);

            // Pitch: apply locally around X axis
            cameraPivot.localRotation = Quaternion.Euler(pitch, cameraPivot.localEulerAngles.y, 0);
        }
    }
}