using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputManagement inputManager;
    private PlayerLocomotion playerLocomotion;
    private AnimatorManager animatorManager;

    private void Awake()
    {
        inputManager    = GetComponent<InputManagement>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animatorManager  = GetComponent<AnimatorManager>();
    }

    private void Update()
    {
        // 1. Read all inputs
        inputManager.HandleAllInputs();

        // 2. Drive the Animator every frame
        animatorManager.UpdateAnimations(inputManager.moveAmount);
    }

    private void FixedUpdate()
    {
        // 3. Move the Rigidbody in FixedUpdate for physics stability
        playerLocomotion.HandleAllMovement();
    }
}