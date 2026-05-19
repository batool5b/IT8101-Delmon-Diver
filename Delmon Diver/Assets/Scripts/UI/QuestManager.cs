using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [System.Serializable]
    public class Quest
    {
        public string questId;
        public string description;
        public int currentProgress;
        public int targetAmount;
        public bool isCompleted;

        [HideInInspector]
        public QuestItemUI uiRow;
    }

    [Header("Prefab & Container")]
    [Tooltip("Drag the QuestItemRow prefab here")]
    public GameObject questRowPrefab;
    [Tooltip("Drag the parent panel (with Vertical Layout Group) here")]
    public Transform questListContainer;

    [Header("Quest List")]
    public List<Quest> activeQuests = new List<Quest>();

    [Header("Global Style Settings")]
    public Sprite uncheckedSprite; // Default empty box sprite
    public Sprite checkedSprite;   // Default checked box sprite
    public Color activeColor = Color.white;
    public Color completedColor = new Color(0.5f, 1f, 0.5f); // Light green

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SpawnAndRefreshUI();
    }

    /// <summary>
    /// Clears old rows, spawns one row per quest, and fills them with data.
    /// </summary>
    [ContextMenu("Refresh UI")]
    public void SpawnAndRefreshUI()
    {
        if (questRowPrefab == null)
        {
            Debug.LogError("[QuestManager] 'Quest Row Prefab' is not assigned in the Inspector! Please drag your prefab here.", this);
        }
        if (questListContainer == null)
        {
            Debug.LogError("[QuestManager] 'Quest List Container' is not assigned in the Inspector! Please drag the parent panel here.", this);
        }

        if (questListContainer == null || questRowPrefab == null) return;

        // Clear old rows
        for (int i = questListContainer.childCount - 1; i >= 0; i--)
        {
            GameObject child = questListContainer.GetChild(i).gameObject;
            if (Application.isPlaying)
                Destroy(child);
            else
                DestroyImmediate(child);
        }

        // Spawn one row per quest and fill it with data
        foreach (Quest q in activeQuests)
        {
            GameObject row = Instantiate(questRowPrefab, questListContainer);
            
            // Fix Unity UI scale and position issues when instantiating prefabs
            row.transform.localScale = Vector3.one;
            RectTransform rect = row.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition3D = Vector3.zero;
                rect.localRotation = Quaternion.identity;
            }

            // Prevent VerticalLayoutGroup from flattening height to 0
            LayoutElement layoutElement = row.GetComponent<LayoutElement>();
            if (layoutElement == null)
            {
                layoutElement = row.AddComponent<LayoutElement>();
            }
            RectTransform prefabRect = questRowPrefab.GetComponent<RectTransform>();
            if (prefabRect != null && layoutElement != null)
            {
                layoutElement.preferredHeight = prefabRect.rect.height;
                layoutElement.preferredWidth = prefabRect.rect.width;
            }

            q.uiRow = row.GetComponent<QuestItemUI>();

            if (q.uiRow != null)
            {
                q.uiRow.Refresh(
                    q.description,
                    q.currentProgress,
                    q.targetAmount,
                    q.isCompleted,
                    uncheckedSprite,
                    checkedSprite,
                    activeColor,
                    completedColor
                );
            }
            else
            {
                Debug.LogError($"[QuestManager] The spawned prefab '{row.name}' does not have the 'QuestItemUI' component attached to its root!", this);
            }
        }
    }

    /// <summary>
    /// Adds progress to a quest and updates its UI row.
    /// Example: QuestManager.Instance.AddProgress("collect_pearls");
    /// </summary>
    public void AddProgress(string questId, int amount = 1)
    {
        Quest q = activeQuests.Find(x => x.questId == questId);
        if (q == null)
        {
            Debug.LogWarning($"[QuestManager] AddProgress failed: No quest with ID '{questId}' was found in the Active Quests list!", this);
            return;
        }

        if (q.isCompleted)
        {
            Debug.Log($"[QuestManager] Quest '{questId}' is already completed. Ignoring progress addition.", this);
            return;
        }

        q.currentProgress = Mathf.Min(q.currentProgress + amount, q.targetAmount);
        Debug.Log($"[QuestManager] Added +{amount} progress to Quest '{questId}'. New Progress: {q.currentProgress}/{q.targetAmount}", this);

        if (q.currentProgress >= q.targetAmount)
        {
            q.isCompleted = true;
            Debug.Log($"Quest '{q.questId}' Completed!");
        }

        if (q.uiRow != null)
        {
            q.uiRow.Refresh(
                q.description,
                q.currentProgress,
                q.targetAmount,
                q.isCompleted,
                uncheckedSprite,
                checkedSprite,
                activeColor,
                completedColor
            );
        }
        else
        {
            Debug.LogWarning($"[QuestManager] Quest '{questId}' has no UI Row assigned! Cannot update visual display.", this);
        }
    }
}
