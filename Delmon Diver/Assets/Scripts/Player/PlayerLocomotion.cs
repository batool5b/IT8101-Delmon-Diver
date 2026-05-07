using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    // ── References (found automatically on same GameObject) ───────────
    private InputManagement     inputManager;
    private EnvironmentDetector env;
    private AnimatorManager     animatorManager;
    private Transform           cameraObject;
    private Rigidbody           playerRigidbody;

    [Header("Land Movement")]
    public float walkSpeed     = 4f;    // metres/sec while walking
    public float runSpeed      = 8f;    // metres/sec while running
    public float rotationSpeed = 15f;   // how fast character turns

    [Header("Water Movement")]
    public float swimSpeed    = 3f;     // slow swim
    public float swimRunSpeed = 5f;     // fast swim (hold Run)
    public float waterDrag    = 3f;     // slows player naturally in water
    public float normalDrag   = 0f;     // no drag on land

    [Header("Dive (run off edge)")]
    public float diveForce    = 6f;     // impulse when diving

    [Header("Jump / SwimUp")]
    public float jumpForce   = 5f;      // land jump impulse
    public float swimUpForce = 3f;      // rise-to-surface impulse

    // ── Private state ─────────────────────────────────────────────────
    private bool hasDived = false;      // prevents dive firing every frame

    // ─────────────────────────────────────────────────────────────────
    private void Awake()
    {
        // GetComponent looks on the SAME GameObject for each script
        inputManager    = GetComponent<InputManagement>();
        env             = GetComponent<EnvironmentDetector>();
        animatorManager = GetComponent<AnimatorManager>();
        playerRigidbody = GetComponent<Rigidbody>();

        // Camera.main finds the camera tagged "MainCamera" in the scene
        cameraObject = Camera.main.transform;

        if (playerRigidbody == null) Debug.LogError("No Rigidbody on " + gameObject.name);
        if (inputManager    == null) Debug.LogError("No InputManagement on " + gameObject.name);
        if (env             == null) Debug.LogError("No EnvironmentDetector on " + gameObject.name);
        if (cameraObject    == null) Debug.LogError("No MainCamera in scene!");
    }

    // Called by PlayerManager every FixedUpdate (physics step, ~50/sec)
    public void HandleAllMovement()
    {
        if (env == null) return;

        // Switch drag so water feels like water
        UpdateDrag();

        if (env.isInWater)
        {
            HandleSwimMovement();
            HandleSwimUp();
        }
        else
        {
            HandleLandMovement();
            HandleJump();
            HandleRunOffEdge();
        }

        HandleRotation();
    }

    // ── LAND ──────────────────────────────────────────────────────────
    private void HandleLandMovement()
    {
        hasDived = false; // reset dive flag when back on land

        // IMPORTANT: always keep the current Y velocity
        // so that gravity continues to work (character falls off edges)
        float yVel = playerRigidbody.linearVelocity.y;

        if (inputManager.moveAmount <= 0f)
        {
            // No input → stop X/Z immediately, keep Y (gravity)
            playerRigidbody.linearVelocity = new Vector3(0f, yVel, 0f);
            return;
        }

        // GetFlatCameraDirection() gives a normalised horizontal direction
        // based on which way the camera is facing + which keys are pressed
        Vector3 dir = GetFlatCameraDirection();
        float   spd = inputManager.isRunning ? runSpeed : walkSpeed;

        // Set X/Z from input, preserve Y from physics
        playerRigidbody.linearVelocity = new Vector3(dir.x * spd, yVel, dir.z * spd);
    }

    private void HandleJump()
    {
        // jumpSwimUpInput = Space key (from InputManagement)
        if (!inputManager.jumpSwimUpInput) return;
        if (!env.isGrounded) return;  // can only jump from ground
        // ForceMode.Impulse = instant kick, mass is factored in
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // ── WATER ─────────────────────────────────────────────────────────
    private void HandleSwimMovement()
    {
        if (inputManager.moveAmount <= 0f) return;  // let drag slow us

        Vector3 dir = GetFlatCameraDirection();
        float   spd = inputManager.isRunning ? swimRunSpeed : swimSpeed;

        float yVel = playerRigidbody.linearVelocity.y;

        // At the water surface, prevent sinking unless player holds Dive (LCtrl)
        if (!env.isSubmerged && !inputManager.diveInput)
            yVel = Mathf.Max(yVel, 0f);

        playerRigidbody.linearVelocity = new Vector3(dir.x * spd, yVel, dir.z * spd);
    }

    private void HandleSwimUp()
    {
        // Space in water = push upward toward surface
        if (!inputManager.jumpSwimUpInput) return;
        playerRigidbody.AddForce(Vector3.up * swimUpForce, ForceMode.Impulse);
    }

    // ── RUN OFF EDGE → DIVE ───────────────────────────────────────────
    private void HandleRunOffEdge()
    {
        // Conditions: on edge + running + haven't dived yet this approach
        if (!env.isAtEdge || !inputManager.isRunning || hasDived) return;

        hasDived = true;

        // Push forward and slightly down for a dive arc
        Vector3 diveDir = (transform.forward + Vector3.down * 0.4f).normalized;
        playerRigidbody.AddForce(diveDir * diveForce, ForceMode.Impulse);

        // Tell the Animator to play "Run To Dive" clip
        animatorManager.TriggerDive();
    }

    // ── SHARED HELPERS ────────────────────────────────────────────────

    // Converts WASD input + camera facing into a flat world direction
    // Example: pressing W while camera faces East → moves East
    private Vector3 GetFlatCameraDirection()
    {
        // verticalInput = W/S (1 or -1), horizontalInput = A/D (1 or -1)
        Vector3 dir  = cameraObject.forward * inputManager.verticalInput;
        dir         += cameraObject.right   * inputManager.horizontalInput;
        dir.Normalize();  // makes diagonal movement same speed as straight
        dir.y = 0f;       // flatten: never move up/down from this calculation
        return dir;
    }

    // Smoothly rotate character to face movement direction
    private void HandleRotation()
    {
        Vector3 dir = GetFlatCameraDirection();
        if (dir == Vector3.zero) return;  // no input = don't rotate

        Quaternion target = Quaternion.LookRotation(dir);
        // Slerp = smooth spherical interpolation between current and target angle
        transform.rotation = Quaternion.Slerp(
            transform.rotation, target, rotationSpeed * Time.deltaTime);
    }

    // Switch Rigidbody drag: high in water (resistance), zero on land
    private void UpdateDrag()
    {
        playerRigidbody.linearDamping = env.isInWater ? waterDrag : normalDrag;
    }
}