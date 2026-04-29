using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    private Animator animator;

    // These strings must match the parameter names you add in the Animator Controller
    private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
            Debug.LogError("AnimatorManager: No Animator found on " + gameObject.name);
    }

    /// <summary>
    /// Call this every frame from PlayerManager, passing inputManager.moveAmount.
    /// 0 = Idle, 0.5 = Walk, 1 = Run
    /// </summary>
    public void UpdateAnimations(float moveAmount)
    {
        // Smooth damp so transitions feel natural (0.1 s blend)
        float current = animator.GetFloat(MoveSpeedHash);
        float smoothed = Mathf.Lerp(current, moveAmount, Time.deltaTime * 10f);
        animator.SetFloat(MoveSpeedHash, smoothed);
    }
}