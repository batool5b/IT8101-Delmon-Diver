using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private const string RewardSaveKey = "RewardSystem.SaveData";

    [System.Serializable]
    public class RewardSystemSaveData
    {
        public int xp;
        public int level = 1;
        public int coins;
    }

    public RewardSystemSaveData LoadRewardData()
    {
        if (!PlayerPrefs.HasKey(RewardSaveKey))
        {
            return new RewardSystemSaveData();
        }

        string json = PlayerPrefs.GetString(RewardSaveKey);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new RewardSystemSaveData();
        }

        RewardSystemSaveData saveData = JsonUtility.FromJson<RewardSystemSaveData>(json);
        return saveData ?? new RewardSystemSaveData();
    }

    public void SaveRewardData(int xp, int level, int coins)
    {
        RewardSystemSaveData saveData = new RewardSystemSaveData
        {
            xp = Mathf.Max(0, xp),
            level = Mathf.Max(1, level),
            coins = Mathf.Max(0, coins)
        };

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(RewardSaveKey, json);
        PlayerPrefs.Save();
    }
}
