// using UnityEngine;
// using DevionGames.InventorySystem;

// /// <summary>
// /// WorldItem  — Delmon Diver project
// ///
// /// Attach to any pickup GameObject in the world (coral, artifact, fish, etc.)
// /// This component stores which Item this object represents and how many of it.
// ///
// /// Devion Games has its own WorldItem component; if that is available in your
// /// project use it instead.  This lightweight version is provided so the project
// /// compiles and works even before the Devion Games asset is fully configured.
// ///
// /// Inspector setup:
// ///   observedItem        — drag the DevionGames Item ScriptableObject here
// ///   observedItemAmount  — how many of the item this world object represents
// ///   autoPickup          — if true the item is collected when player enters
// ///                         the trigger collider (no key press needed)
// ///   floatAmplitude      — how much the object bobs up and down (0 = static)
// ///   floatFrequency      — speed of the bobbing animation
// ///   rotationSpeed       — how fast the object spins (0 = no spin)
// ///   glowColor           — outline / emission colour shown when in range
// /// </summary>
// [RequireComponent(typeof(Collider))]
// public class WorldItem : MonoBehaviour
// {
//     // ── Inspector ──────────────────────────────────────────────────────────
//     [Header("Item Data")]
//     [Tooltip("The DevionGames Item ScriptableObject this world-object represents.")]
//     public Item observedItem;

//     [Tooltip("How many copies of the item this world-object gives.")]
//     [Min(1)]
//     public int observedItemAmount = 1;

//     [Tooltip("Pick up automatically when player walks over it (trigger).")]
//     public bool autoPickup = false;

//     [Header("Visual Behaviour")]
//     [Tooltip("How far the object bobs up and down in world units. 0 = static.")]
//     [Range(0f, 0.5f)]
//     public float floatAmplitude = 0.15f;

//     [Tooltip("Speed of the bob animation.")]
//     [Range(0f, 5f)]
//     public float floatFrequency = 1.2f;

//     [Tooltip("Degrees per second the object spins. 0 = no spin.")]
//     [Range(0f, 360f)]
//     public float rotationSpeed = 45f;

//     [Header("Glow")]
//     [Tooltip("Emission colour while the player is near. Uses Emission on the Renderer.")]
//     public Color glowColor = Color.cyan;

//     // ── Public accessors (used by ItemPickupHandler) ───────────────────────
//     public Item ObservedItem       => observedItem;
//     public int  ObservedItemAmount => observedItemAmount;

//     // ── Private ────────────────────────────────────────────────────────────
//     private Vector3     _startPos;
//     private Renderer[]  _renderers;
//     private bool        _glowing;
//     private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");

//     // ─────────────────────────────────────────────────────────────────────
//     private void Awake()
//     {
//         _startPos  = transform.position;
//         _renderers = GetComponentsInChildren<Renderer>();

//         // Ensure the collider is a trigger for auto-pickup
//         Collider col = GetComponent<Collider>();
//         if (autoPickup) col.isTrigger = true;
//     }

//     private void Update()
//     {
//         // Bob up and down
//         if (floatAmplitude > 0f)
//         {
//             float y = _startPos.y + Mathf.Sin(Time.time * floatFrequency * Mathf.PI * 2f) * floatAmplitude;
//             transform.position = new Vector3(transform.position.x, y, transform.position.z);
//         }

//         // Spin
//         if (rotationSpeed > 0f)
//             transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
//     }

//     // Auto-pickup: fires when player enters trigger collider
//     private void OnTriggerEnter(Collider other)
//     {
//         if (!autoPickup) return;
//         if (!other.CompareTag("Player")) return;

//         ItemPickupHandler picker = other.GetComponentInParent<ItemPickupHandler>();
//         if (picker != null)
//             picker.ForcePickup(this);
//     }

//     // ── Glow helpers (called by ItemPickupHandler) ─────────────────────────

//     /// <summary>Enable emission glow when player is in range.</summary>
//     public void SetGlow(bool on)
//     {
//         if (_glowing == on) return;
//         _glowing = on;

//         foreach (Renderer r in _renderers)
//         {
//             // Material must have emission enabled in the shader
//             if (on)
//             {
//                 r.material.EnableKeyword("_EMISSION");
//                 r.material.SetColor(EmissionColorID, glowColor * 2f);
//             }
//             else
//             {
//                 r.material.DisableKeyword("_EMISSION");
//                 r.material.SetColor(EmissionColorID, Color.black);
//             }
//         }
//     }
// }
