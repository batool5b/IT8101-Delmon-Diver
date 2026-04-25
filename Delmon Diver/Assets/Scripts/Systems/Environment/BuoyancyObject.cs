using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyObject : MonoBehaviour
{
    public float objectVolume = 1.0f;
    public float depthBeforeSubmerged = 1f;
    public float displacementMultiplier = 3f;
    public Transform[] floaters;
    
    [HideInInspector] public bool inWater = false;
    [HideInInspector] public WaterVolume waterVolume;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (floaters.Length == 0)
        {
            // fallback if no floaters assigned
            floaters = new Transform[1];
            floaters[0] = transform;
        }
    }

    void FixedUpdate()
    {
        if (inWater && waterVolume != null)
        {
            float forceMultiplier = 1f / floaters.Length;

            for (int i = 0; i < floaters.Length; i++)
            {
                Transform floater = floaters[i];
                float waterVolY = waterVolume.GetWaterLevelAtPos(floater.position);

                // Check if floater is actually underwater
                if (floater.position.y < waterVolY)
                {
                    float displacementOffset = Mathf.Clamp01((waterVolY - floater.position.y) / depthBeforeSubmerged) * displacementMultiplier;
                    
                    // Archimedes principle: Upward force = density * gravity * volume displaced
                    Vector3 upLift = new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementOffset * waterVolume.fluidDensity * objectVolume, 0f);
                    
                    rb.AddForceAtPosition(upLift * forceMultiplier, floater.position, ForceMode.Force);
                }
            }
        }
    }
}
