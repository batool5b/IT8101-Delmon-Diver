using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    [SerializeField] private int startingLevel = 1;
    [SerializeField] private int maxLevel = 50;
    [SerializeField] private int baseXPForNextLevel = 100;
    [SerializeField] private float xpGrowthMultiplier = 1.35f;

    public int CurrentLevel { get; private set; }

    public event System.Action<int> OnLevelChanged;

    private void Awake()
    {
        CurrentLevel = Mathf.Max(1, startingLevel);
    }

    public void Initialize(int savedLevel)
    {
        CurrentLevel = Mathf.Clamp(savedLevel, 1, maxLevel);
        OnLevelChanged?.Invoke(CurrentLevel);
    }

    public int GetRequiredXPForLevel(int level)
    {
        if (level <= 1)
        {
            return 0;
        }

        float totalXP = 0f;

        for (int currentLevel = 1; currentLevel < level; currentLevel++)
        {
            totalXP += baseXPForNextLevel * Mathf.Pow(xpGrowthMultiplier, currentLevel - 1);
        }

        return Mathf.RoundToInt(totalXP);
    }

    public void UpdateLevelFromXP(int totalXP)
    {
        int calculatedLevel = 1;

        while (calculatedLevel < maxLevel && totalXP >= GetRequiredXPForLevel(calculatedLevel + 1))
        {
            calculatedLevel++;
        }

        if (calculatedLevel == CurrentLevel)
        {
            return;
        }

        CurrentLevel = calculatedLevel;
        OnLevelChanged?.Invoke(CurrentLevel);
    }
}
