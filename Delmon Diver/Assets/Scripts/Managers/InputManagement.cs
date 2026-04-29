using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagement : MonoBehaviour
{
    // Raw input values read from PlayerInputActions
    [HideInInspector] public float verticalInput;
    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float moveAmount;   // 0 = idle, 0.5 = walk, 1 = run
    [HideInInspector] public bool isRunning;

    private PlayerInputActions playerInputActions;
    private Vector2 movementInput;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.PlayerMovement.Enable();

        // Subscribe to Move
        playerInputActions.PlayerMovement.Move.performed += OnMove;
        playerInputActions.PlayerMovement.Move.canceled  += OnMove;

        // Subscribe to Run
        playerInputActions.PlayerMovement.Run.performed += OnRun;
        playerInputActions.PlayerMovement.Run.canceled  += OnRun;
    }

    private void OnDisable()
    {
        playerInputActions.PlayerMovement.Move.performed -= OnMove;
        playerInputActions.PlayerMovement.Move.canceled  -= OnMove;
        playerInputActions.PlayerMovement.Run.performed  -= OnRun;
        playerInputActions.PlayerMovement.Run.canceled   -= OnRun;

        playerInputActions.PlayerMovement.Disable();
    }

    // Called automatically by the Input System
    private void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    private void OnRun(InputAction.CallbackContext ctx)
    {
        isRunning = ctx.ReadValueAsButton();
    }

    // Called every frame by PlayerManager
    public void HandleAllInputs()
    {
        horizontalInput = movementInput.x;
        verticalInput   = movementInput.y;

        // Build a single 0-1 float the animator and locomotion both use
        bool hasInput = movementInput.sqrMagnitude > 0.01f;

        if (!hasInput)
            moveAmount = 0f;                     // Idle  → parameter = 0
        else if (isRunning)
            moveAmount = 1f;                     // Run   → parameter = 1
        else
            moveAmount = 0.5f;                   // Walk  → parameter = 0.5
    }
}