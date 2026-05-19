using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Attach this to the quest row PREFAB.
/// Only holds UI references — QuestManager fills them with data.
/// </summary>
public class QuestItemUI : MonoBehaviour
{
    public TMP_Text descriptionText;
    public TMP_Text progressText;
    public Image    checkImage;

    public void Refresh(string description, int current, int target, bool completed,
                        Sprite uncheckedSprite, Sprite checkedSprite,
                        Color activeColor, Color completedColor)
    {
        if (descriptionText != null)
        {
            descriptionText.text  = description;
            descriptionText.color = completed ? completedColor : activeColor;
        }
        else
        {
            Debug.LogError($"[QuestItemUI] '{gameObject.name}' has no Description Text assigned in the Inspector!", this);
        }

        if (progressText != null)
        {
            progressText.text  = $"{current} / {target}";
            progressText.color = completed ? completedColor : activeColor;
        }
        else
        {
            Debug.LogError($"[QuestItemUI] '{gameObject.name}' has no Progress Text assigned in the Inspector!", this);
        }

        if (checkImage != null)
        {
            if (uncheckedSprite != null && checkedSprite != null)
            {
                checkImage.sprite = completed ? checkedSprite : uncheckedSprite;
            }
        }
        else
        {
            Debug.LogWarning($"[QuestItemUI] '{gameObject.name}' has no Check Image assigned in the Inspector.", this);
        }
    }
}
