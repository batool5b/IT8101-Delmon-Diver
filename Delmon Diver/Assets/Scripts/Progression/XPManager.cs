using UnityEngine;

public class XPManager : MonoBehaviour
{
    [SerializeField] private LevelProgression levelProgression;

    public int CurrentXP { get; private set; }

    public event System.Action<int> OnXPChanged;

    private void Awake()
    {
        if (levelProgression == null)
        {
            levelProgression = GetComponent<LevelProgression>();
        }
    }

    public void Initialize(int savedXP)
    {
        CurrentXP = Mathf.Max(0, savedXP);
        OnXPChanged?.Invoke(CurrentXP);

        if (levelProgression != null)
        {
            levelProgression.UpdateLevelFromXP(CurrentXP);
        }
    }

    public void AddXP(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        CurrentXP += amount;
        OnXPChanged?.Invoke(CurrentXP);

        if (levelProgression != null)
        {
            levelProgression.UpdateLevelFromXP(CurrentXP);
        }
    }
}
