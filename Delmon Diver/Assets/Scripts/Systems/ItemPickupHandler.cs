// using UnityEngine;
// using DevionGames.InventorySystem;

// /// <summary>
// /// ItemPickupHandler — Delmon Diver project
// ///
// /// Sits on the same GameObject as PlayerManager.
// /// Every frame it does a sphere-cast around the player looking for nearby
// /// world objects that have a WorldItem component (the Devion Games world-
// /// pickup prefab wrapper).  When the player presses [E] (Interact action)
// /// the nearest object is picked up and added to the "Inventory" container.
// ///
// /// Setup checklist:
// ///   1. Add this script to the player root GameObject (Ch31_nonPBR).
// ///   2. Expose the [E] Interact action in InputManagement (done — see below).
// ///   3. Place the Devion Games "InventoryManager" prefab in the scene.
// ///   4. Tag your item world objects correctly (handled by Devion's WorldItem).
// ///   5. Set pickupRadius in the Inspector.
// ///
// /// Key:
// ///   pickupRadius     — how close the player must be to pick up (metres)
// ///   itemLayer        — set to the layer your world-item objects are on
// ///   interactPromptUI — optional UI text that shows "Press E to pick up …"
// /// </summary>
// public class ItemPickupHandler : MonoBehaviour
// {
//     // ── Inspector ──────────────────────────────────────────────────────────
//     [Header("Pickup Settings")]
//     [Tooltip("Radius of the sphere that scans for nearby items.")]
//     public float pickupRadius = 2.5f;

//     [Tooltip("Layer your world item GameObjects are on (e.g. 'Item').")]
//     public LayerMask itemLayer;

//     [Header("UI (optional)")]
//     [Tooltip("UI text that displays 'Press E to pick up …' — may be null.")]
//     public UnityEngine.UI.Text interactPromptUI;

//     // ── Private ────────────────────────────────────────────────────────────
//     private InputManagement    _input;
//     private WorldItem          _nearestItem;   // DevionGames WorldItem
//     private bool               _wasInteractHeld;

//     // ─────────────────────────────────────────────────────────────────────
//     private void Awake()
//     {
//         _input = GetComponent<InputManagement>();

//         if (_input == null)
//             Debug.LogError("[ItemPickupHandler] InputManagement not found on " + gameObject.name);
//     }

//     private void Update()
//     {
//         ScanForNearbyItems();
//         UpdatePromptUI();
//         HandlePickupInput();
//     }

//     // ── Scan ──────────────────────────────────────────────────────────────

//     /// <summary>
//     /// Finds the closest WorldItem within pickupRadius.
//     /// Stores the result in _nearestItem (null if none found).
//     /// </summary>
//     private WorldItem _previousNearest;

//     private void ScanForNearbyItems()
//     {
//         Collider[] hits = Physics.OverlapSphere(transform.position, pickupRadius, itemLayer);

//         _nearestItem = null;
//         float closest = float.MaxValue;

//         foreach (Collider hit in hits)
//         {
//             WorldItem wi = hit.GetComponentInParent<WorldItem>();
//             if (wi == null) continue;

//             float dist = Vector3.Distance(transform.position, hit.transform.position);
//             if (dist < closest)
//             {
//                 closest      = dist;
//                 _nearestItem = wi;
//             }
//         }

//         // Glow: turn off old, turn on new
//         if (_previousNearest != _nearestItem)
//         {
//             if (_previousNearest != null) _previousNearest.SetGlow(false);
//             if (_nearestItem     != null) _nearestItem.SetGlow(true);
//             _previousNearest = _nearestItem;
//         }
//     }

//     /// <summary>
//     /// Called by WorldItem.OnTriggerEnter for autoPickup objects.
//     /// </summary>
//     public void ForcePickup(WorldItem worldItem)
//     {
//         _nearestItem = worldItem;
//         TryPickup();
//     }

//     // ── Prompt UI ─────────────────────────────────────────────────────────

//     private void UpdatePromptUI()
//     {
//         if (interactPromptUI == null) return;

//         if (_nearestItem != null)
//         {
//             interactPromptUI.text    = $"[E]  Pick up  {_nearestItem.ObservedItem?.DisplayName ?? "item"}";
//             interactPromptUI.enabled = true;
//         }
//         else
//         {
//             interactPromptUI.enabled = false;
//         }
//     }

//     // ── Pickup ────────────────────────────────────────────────────────────

//     /// <summary>
//     /// Reads the Interact flag from InputManagement.
//     /// Uses rising-edge detection so one press = one pickup.
//     /// </summary>
//     private void HandlePickupInput()
//     {
//         bool interactPressed = _input != null && _input.interactInput;

//         // Rising-edge: only act on the frame the key first goes down
//         if (interactPressed && !_wasInteractHeld)
//             TryPickup();

//         _wasInteractHeld = interactPressed;
//     }

//     private void TryPickup()
//     {
//         if (_nearestItem == null) return;

//         // DevionGames: find the player's Inventory container and add the item
//         ItemContainer inventory = ItemContainer.Find("Inventory");

//         if (inventory == null)
//         {
//             Debug.LogWarning("[ItemPickupHandler] Could not find ItemContainer named 'Inventory'. " +
//                              "Make sure the InventoryManager prefab is in the scene and the container " +
//                              "is named exactly 'Inventory'.");
//             return;
//         }

//         Item item   = _nearestItem.ObservedItem;
//         int  amount = _nearestItem.ObservedItemAmount;

//         if (item == null) return;

//         if (inventory.CanAddItem(item, amount))
//         {
//             inventory.AddItem(item, amount);
//             Debug.Log($"[ItemPickupHandler] Picked up {amount}× {item.DisplayName}");
//             _nearestItem.gameObject.SetActive(false); // hide world object
//             Destroy(_nearestItem.gameObject, 0.1f);   // clean up after frame
//             _nearestItem = null;
//         }
//         else
//         {
//             Debug.Log("[ItemPickupHandler] Inventory is full!");
//             // Optionally: show "Inventory full" UI message here
//         }
//     }

//     // ── Gizmos ────────────────────────────────────────────────────────────

//     private void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.yellow;
//         Gizmos.DrawWireSphere(transform.position, pickupRadius);
//     }
// }
