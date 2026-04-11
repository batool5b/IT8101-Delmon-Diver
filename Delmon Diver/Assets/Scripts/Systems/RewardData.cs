using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardDefinition", menuName = "Delmon Diver/Rewards/Reward Definition")]
public class RewardData : ScriptableObject
{
    [SerializeField] private string rewardId = "reward_default";
    [SerializeField] private string displayName = "New Reward";
    [SerializeField] private List<RewardEntry> rewards = new List<RewardEntry>();

    public string RewardId => rewardId;
    public string DisplayName => displayName;
    public IReadOnlyList<RewardEntry> Rewards => rewards;
}

[Serializable]
public class RewardEntry
{
    public RewardType type;
    public int amount = 10;
}

public enum RewardType
{
    XP,
    Coins
}
