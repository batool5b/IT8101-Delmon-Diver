using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private XPManager xpManager;
    [SerializeField] private LevelProgression levelProgression;
    [SerializeField] private SaveLoadManager saveLoadManager;
    [SerializeField] private bool loadOnAwake = true;

    public int Coins { get; private set; }

    public event System.Action<RewardData> OnRewardGranted;
    public event System.Action<int> OnCoinsChanged;

    private void Awake()
    {
        if (xpManager == null)
        {
            xpManager = GetComponent<XPManager>();
        }

        if (levelProgression == null)
        {
            levelProgression = GetComponent<LevelProgression>();
        }

        if (saveLoadManager == null)
        {
            saveLoadManager = GetComponent<SaveLoadManager>();
        }

        if (loadOnAwake)
        {
            LoadProgress();
        }
    }

    public void GrantReward(RewardData rewardData)
    {
        if (rewardData == null)
        {
            Debug.LogWarning("RewardManager received a null reward definition.");
            return;
        }

        for (int index = 0; index < rewardData.Rewards.Count; index++)
        {
            RewardEntry entry = rewardData.Rewards[index];
            ApplyReward(entry);
        }

        SaveProgress();
        OnRewardGranted?.Invoke(rewardData);
    }

    public void GrantLevelReward(RewardData rewardData)
    {
        GrantReward(rewardData);
    }

    public void GrantMissionReward(RewardData rewardData)
    {
        GrantReward(rewardData);
    }

    public void GrantEnemyReward(RewardData rewardData)
    {
        GrantReward(rewardData);
    }

    public void GrantBossReward(RewardData rewardData)
    {
        GrantReward(rewardData);
    }

    public void GrantCoins(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        Coins += amount;
        OnCoinsChanged?.Invoke(Coins);
    }

    public bool SpendCoins(int amount)
    {
        if (amount <= 0 || amount > Coins)
        {
            return false;
        }

        Coins -= amount;
        OnCoinsChanged?.Invoke(Coins);
        SaveProgress();
        return true;
    }

    public void LoadProgress()
    {
        if (saveLoadManager == null)
        {
            return;
        }

        SaveLoadManager.RewardSystemSaveData saveData = saveLoadManager.LoadRewardData();

        if (xpManager != null)
        {
            xpManager.Initialize(saveData.xp);
        }

        if (levelProgression != null)
        {
            levelProgression.Initialize(saveData.level);
            levelProgression.UpdateLevelFromXP(saveData.xp);
        }

        Coins = Mathf.Max(0, saveData.coins);
        OnCoinsChanged?.Invoke(Coins);
    }

    public void SaveProgress()
    {
        if (saveLoadManager == null || xpManager == null || levelProgression == null)
        {
            return;
        }

        saveLoadManager.SaveRewardData(xpManager.CurrentXP, levelProgression.CurrentLevel, Coins);
    }

    private void ApplyReward(RewardEntry entry)
    {
        if (entry == null || entry.amount <= 0)
        {
            return;
        }

        switch (entry.type)
        {
            case RewardType.XP:
                if (xpManager != null)
                {
                    xpManager.AddXP(entry.amount);
                }
                break;
            case RewardType.Coins:
                GrantCoins(entry.amount);
                break;
        }
    }
}
