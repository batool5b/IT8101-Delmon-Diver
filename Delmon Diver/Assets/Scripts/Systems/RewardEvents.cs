using UnityEngine;

public class RewardEvents : MonoBehaviour
{
    [SerializeField] private RewardManager rewardManager;
    [SerializeField] private RewardData levelReward;
    [SerializeField] private RewardData missionReward;
    [SerializeField] private RewardData enemyReward;
    [SerializeField] private RewardData bossReward;

    private void Awake()
    {
        if (rewardManager == null)
        {
            rewardManager = FindFirstObjectByType<RewardManager>();
        }
    }

    public void HandleLevelCompleted()
    {
        rewardManager?.GrantLevelReward(levelReward);
    }

    public void HandleMissionCompleted()
    {
        rewardManager?.GrantMissionReward(missionReward);
    }

    public void HandleEnemyDefeated()
    {
        rewardManager?.GrantEnemyReward(enemyReward);
    }

    public void HandleBossDefeated()
    {
        rewardManager?.GrantBossReward(bossReward);
    }
}
