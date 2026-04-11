using UnityEngine;

public class RewardTrigger : MonoBehaviour
{
    [SerializeField] private RewardEvents rewardEvents;
    [SerializeField] private RewardTriggerType triggerType;
    [SerializeField] private bool triggerOnlyOnce = true;

    private bool hasTriggered;

    private void Awake()
    {
        if (rewardEvents == null)
        {
            rewardEvents = FindFirstObjectByType<RewardEvents>();
        }
    }

    public void TriggerRewardEvent()
    {
        if (triggerOnlyOnce && hasTriggered)
        {
            return;
        }

        switch (triggerType)
        {
            case RewardTriggerType.LevelDone:
                rewardEvents?.HandleLevelCompleted();
                break;
            case RewardTriggerType.MissionDone:
                rewardEvents?.HandleMissionCompleted();
                break;
            case RewardTriggerType.EnemyDown:
                rewardEvents?.HandleEnemyDefeated();
                break;
            case RewardTriggerType.BossDown:
                rewardEvents?.HandleBossDefeated();
                break;
        }

        hasTriggered = true;
    }
}

public enum RewardTriggerType
{
    LevelDone,
    MissionDone,
    EnemyDown,
    BossDown
}
