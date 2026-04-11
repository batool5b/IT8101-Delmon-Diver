using UnityEngine;

public class RewardTester : MonoBehaviour
{
    [SerializeField] private RewardManager rewardManager;
    [SerializeField] private RewardEvents rewardEvents;
    [SerializeField] private RewardData testReward;
    [SerializeField] private KeyCode grantRewardKey = KeyCode.R;
    [SerializeField] private KeyCode completeLevelKey = KeyCode.Alpha1;
    [SerializeField] private KeyCode completeMissionKey = KeyCode.Alpha2;
    [SerializeField] private KeyCode defeatEnemyKey = KeyCode.Alpha3;
    [SerializeField] private KeyCode defeatBossKey = KeyCode.Alpha4;

    private void Awake()
    {
        if (rewardManager == null)
        {
            rewardManager = FindFirstObjectByType<RewardManager>();
        }

        if (rewardEvents == null)
        {
            rewardEvents = FindFirstObjectByType<RewardEvents>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(grantRewardKey))
        {
            rewardManager?.GrantReward(testReward);
        }

        if (Input.GetKeyDown(completeLevelKey))
        {
            rewardEvents?.HandleLevelCompleted();
        }

        if (Input.GetKeyDown(completeMissionKey))
        {
            rewardEvents?.HandleMissionCompleted();
        }

        if (Input.GetKeyDown(defeatEnemyKey))
        {
            rewardEvents?.HandleEnemyDefeated();
        }

        if (Input.GetKeyDown(defeatBossKey))
        {
            rewardEvents?.HandleBossDefeated();
        }
    }
}
