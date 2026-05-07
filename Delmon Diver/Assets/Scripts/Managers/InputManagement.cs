using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Reads all player input from the Input System (PlayerInputActions asset)
/// and stores it as simple floats and bools that other scripts can read.
/// Other scripts never touch the Input System directly — they read from here.
/// </summary>
public class InputManagement : MonoBehaviour
{
    // ── OUTPUT: read by PlayerLocomotion and AnimatorManager ──────────
    [HideInInspector] public float verticalInput;    // W/S: -1 to 1
    [HideInInspector] public float horizontalInput;  // A/D: -1 to 1
    [HideInInspector] public float moveAmount;       // 0=idle, 0.5=walk, 1=run
    [HideInInspector] public bool  isRunning;        // Left Shift held
    [HideInInspector] public bool  jumpSwimUpInput;  // Space held
    [HideInInspector] public bool  diveInput;        // Left Ctrl held

    // ── INTERNAL ──────────────────────────────────────────────────────
    private PlayerInputActions playerInputActions;  // auto-generated from .inputactions file
    private Vector2            movementInput;        // raw WASD vector

    private void Awake()
    {
        // Create the input actions object (generated from PlayerInputActions.inputactions)
        playerInputActions = new PlayerInputActions();
    }

    // OnEnable/OnDisable: subscribe/unsubscribe to input events
    // This is the correct pattern — never subscribe in Awake or Start
    private void OnEnable()
    {
        playerInputActions.PlayerMovement.Enable();

        // performed = key pressed or axis moved
        // canceled  = key released or axis returned to zero
        playerInputActions.PlayerMovement.Move.performed       += OnMove;
        playerInputActions.PlayerMovement.Move.canceled        += OnMove;
        playerInputActions.PlayerMovement.Run.performed        += OnRun;
        playerInputActions.PlayerMovement.Run.canceled         += OnRun;
        playerInputActions.PlayerMovement.JumpSwimUp.performed += OnJumpSwimUp;
        playerInputActions.PlayerMovement.JumpSwimUp.canceled  += OnJumpSwimUp;
        playerInputActions.PlayerMovement.Dive.performed       += OnDive;
        playerInputActions.PlayerMovement.Dive.canceled        += OnDive;
    }

    private void OnDisable()
    {
        // Always unsubscribe to avoid memory leaks
        playerInputActions.PlayerMovement.Move.performed       -= OnMove;
        playerInputActions.PlayerMovement.Move.canceled        -= OnMove;
        playerInputActions.PlayerMovement.Run.performed        -= OnRun;
        playerInputActions.PlayerMovement.Run.canceled         -= OnRun;
        playerInputActions.PlayerMovement.JumpSwimUp.performed -= OnJumpSwimUp;
        playerInputActions.PlayerMovement.JumpSwimUp.canceled  -= OnJumpSwimUp;
        playerInputActions.PlayerMovement.Dive.performed       -= OnDive;
        playerInputActions.PlayerMovement.Dive.canceled        -= OnDive;
        playerInputActions.PlayerMovement.Disable();
    }

    // These are called automatically by the Input System when keys change
    // => is shorthand for a one-line function (lambda expression)
    private void OnMove(InputAction.CallbackContext ctx)
        => movementInput = ctx.ReadValue<Vector2>();  // reads WASD as X/Y vector

    private void OnRun(InputAction.CallbackContext ctx)
        => isRunning = ctx.ReadValueAsButton();        // true while Shift is held

    private void OnJumpSwimUp(InputAction.CallbackContext ctx)
        => jumpSwimUpInput = ctx.ReadValueAsButton();  // true while Space is held

    private void OnDive(InputAction.CallbackContext ctx)
        => diveInput = ctx.ReadValueAsButton();        // true while LCtrl is held

    /// <summary>
    /// Called every Update by PlayerManager.
    /// Converts raw input into the simple values other scripts use.
    /// </summary>
    public void HandleAllInputs()
    {
        horizontalInput = movementInput.x;  // A = -1, D = +1
        verticalInput   = movementInput.y;  // S = -1, W = +1

        bool hasInput = movementInput.sqrMagnitude > 0.01f;  // any key pressed?

        // Build moveAmount: single number representing movement state
        // 0.0 = Idle (no input)
        // 0.5 = Walk (input but no shift)
        // 1.0 = Run  (input + shift)
        if (!hasInput)       moveAmount = 0f;
        else if (isRunning)  moveAmount = 1f;
        else                 moveAmount = 0.5f;
    }
}