using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

/// <summary>
/// ThirdPersonCameraController  — Delmon Diver project
///
/// What this script does:
///   • Reads ONLY the Look action (mouse / right-stick) to rotate the camera.
///     Movement keys (WASD / left-stick) are completely ignored here.
///   • No zoom: scroll-wheel input is intentionally never read.
///   • Underwater mode: when the player is submerged the pivot rolls 90 °
///     (horizontal tilt) so the camera feels like it is swimming.
///   • UI mode (press Escape): frees the cursor and pauses camera rotation.
///
/// How it works with Cinemachine:
///   The script rotates a small empty GameObject called "CameraFollow"
///   (the cameraFollowTarget). The Cinemachine Camera's Tracking Target is
///   set to that same GameObject, and Rotation Control is set to
///   "Rotate With Follow Target".  Cinemachine therefore always copies
///   whatever rotation this script applies — no Input Provider needed.
///
/// Required scene setup:
///   See the companion User Guide: CameraSystem_UserGuide.md
/// </summary>
public class ThirdPersonCameraController : MonoBehaviour
{
    // ── Singleton ─────────────────────────────────────────────────────────
    /// <summary>Lets UI buttons call EnterUIMode / ExitUIMode without a
    /// direct Inspector reference.</summary>
    public static ThirdPersonCameraController Instance { get; private set; }

    // ── Inspector fields ──────────────────────────────────────────────────
    [Header("References")]
    [Tooltip("The empty GameObject that Cinemachine tracks (CameraFollow).")]
    public Transform cameraFollowTarget;

    [Tooltip("InputManagement on the player — used for look inputs.")]
    public InputManagement inputManager;

    [Header("Mouse Sensitivity")]
    [Tooltip("How fast the camera turns left/right with the mouse.")]
    public float horizontalSensitivity = 200f;

    [Tooltip("How fast the camera tilts up/down with the mouse.")]
    public float verticalSensitivity   = 150f;

    [Header("Gamepad Sensitivity")]
    [Tooltip("How fast the camera turns left/right with the right stick.")]
    public float gamepadHorizontalSensitivity = 100f;

    [Tooltip("How fast the camera tilts up/down with the right stick.")]
    public float gamepadVerticalSensitivity   = 80f;

    [Header("Pitch (Up / Down) Clamp")]
    [Tooltip("Lowest angle the camera can look up from (negative = look up).")]
    public float minVerticalAngle = -30f;

    [Tooltip("Highest angle the camera can look down to.")]
    public float maxVerticalAngle = 60f;

    [Header("Underwater Roll")]
    [Tooltip("Target roll angle when fully submerged (90 = sideways swim view).")]
    public float underwaterRollAngle = 90f;

    [Tooltip("How fast the camera rolls into / out of the underwater angle.")]
    public float rollLerpSpeed = 5f;

    [Header("Options")]
    [Tooltip("Invert the vertical (up/down) look axis.")]
    public bool invertY = false;

    // ── Private state ─────────────────────────────────────────────────────
    private float _pitch;       // current up/down angle
    private float _yaw;         // current left/right angle
    private float _roll;        // current roll (0 on land, underwaterRollAngle underwater)
    private bool  _uiMode;      // true = cursor free, camera frozen

    // ─────────────────────────────────────────────────────────────────────
    private void Awake()
    {
        Instance = this;

        // Auto-find InputManagement if not assigned in Inspector
        if (inputManager == null)
            inputManager = FindFirstObjectByType<InputManagement>();

        if (cameraFollowTarget == null)
            Debug.LogError("[ThirdPersonCameraController] cameraFollowTarget is not assigned!");
    }

    private void Start()
    {
        LockCursor();

        // Initialise yaw so the camera starts facing the same way as the target
        if (cameraFollowTarget != null)
        {
            _yaw   = cameraFollowTarget.eulerAngles.y;
            _pitch = 0f;
            _roll  = 0f;
        }
    }

    private void LateUpdate()
    {
        HandleCursorToggle();

        if (!_uiMode)
        {
            HandleCameraRotation();   // look left/right/up/down
            HandleUnderwaterRoll();   // tilt when submerged
        }
    }

    // ── Camera rotation ───────────────────────────────────────────────────

    private void HandleCameraRotation()
    {
        // Hard guard: if cursor is free (e.g. notebook UI) don't rotate
        if (Cursor.lockState != CursorLockMode.Locked) return;

        Vector2 look = inputManager != null ? inputManager.GetLookInput() : Vector2.zero;
        bool usingMouse = inputManager != null ? inputManager.isLookInputFromMouse : true;

        float yawDelta;

        if (usingMouse)
        {
            // Mouse delta is a frame-based displacement. Do NOT multiply by Time.deltaTime.
            // Scale by 0.005f to keep rotation speed smooth at default sensitivity values.
            float mouseX = look.x * 0.005f;

            yawDelta = mouseX * horizontalSensitivity;

            if (inputManager != null)
            {
                inputManager.ResetLookInput();
            }
        }
        else
        {
            // Gamepad right-stick: values range -1…1, needs deltaTime
            float stickX = look.x;

            yawDelta = stickX * gamepadHorizontalSensitivity * Time.deltaTime;
        }

        _yaw  += yawDelta;
        _pitch = 0f; // Force vertical angle to stay flat (left/right only)

        // Write rotation to the follow target
        // Roll is handled separately in HandleUnderwaterRoll()
        if (cameraFollowTarget != null)
            cameraFollowTarget.rotation = Quaternion.Euler(_pitch, _yaw, _roll);
    }

    // ── Underwater roll ───────────────────────────────────────────────────

    private void HandleUnderwaterRoll()
    {
        // Decide target roll based on whether the player is submerged
        bool submerged = false;
        float targetRoll = submerged ? underwaterRollAngle : 0f;

        // Smoothly interpolate toward the target roll angle
        _roll = Mathf.LerpAngle(_roll, targetRoll, rollLerpSpeed * Time.deltaTime);

        // Apply — pitch and yaw stay the same, only roll changes
        if (cameraFollowTarget != null)
            cameraFollowTarget.rotation = Quaternion.Euler(_pitch, _yaw, _roll);
    }

    // ── Cursor toggle (Escape) ────────────────────────────────────────────

    private void HandleCursorToggle()
    {
        if (Keyboard.current?.escapeKey.wasPressedThisFrame == true)
        {
            if (_uiMode) ExitUIMode();
            else         EnterUIMode();
        }
    }

    // ── Public API (call from UI buttons) ─────────────────────────────────

    /// <summary>Free the cursor and stop camera from rotating (open menus).</summary>
    public void EnterUIMode()
    {
        _uiMode = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;
    }

    /// <summary>Lock the cursor back and resume camera control (close menus).</summary>
    public void ExitUIMode()
    {
        _uiMode = false;
        LockCursor();
    }

    // ── Helpers ───────────────────────────────────────────────────────────

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }
}