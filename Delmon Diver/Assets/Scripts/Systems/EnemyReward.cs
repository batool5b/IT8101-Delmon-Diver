using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    [SerializeField] private RewardTrigger rewardTrigger;

    private void Awake()
    {
        if (rewardTrigger == null)
        {
            rewardTrigger = GetComponent<RewardTrigger>();
        }
    }

    public void HandleDefeat()
    {
        rewardTrigger?.TriggerRewardEvent();
    }
}
