using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Neocortex.Samples
{
    /// <summary>
    /// Custom message UI override for styling chat messages
    /// Customizes:
    /// 1. Removes background color
    /// 2. Changes text color based on user/agent
    /// 3. Aligns messages to the left
    /// 4. Adds "[AlHaetham]" prefix for user messages
    /// 5. Adds "[Balbol]" prefix for agent messages
    /// </summary>
    public class CustomMessageUI : MonoBehaviour
    {
        [SerializeField] private Color userTextColor = Color.blue;
        [SerializeField] private Color agentTextColor = Color.green;
        
        private Text textComponent;
        private TextMeshProUGUI tmpTextComponent;
        private Image backgroundImage;
        private LayoutElement layoutElement;
        private HorizontalLayoutGroup horizontalLayoutGroup;

        private void Awake()
        {
            // Get the text component (could be Text or TextMeshPro)
            textComponent = GetComponentInChildren<Text>();
            tmpTextComponent = GetComponentInChildren<TextMeshProUGUI>();
            
            // Get the background image (the parent container)
            backgroundImage = transform.parent?.GetComponent<Image>();
            
            // Get layout components
            layoutElement = GetComponent<LayoutElement>();
            horizontalLayoutGroup = transform.parent?.GetComponent<HorizontalLayoutGroup>();
        }

        /// <summary>
        /// Style the message based on whether it's from user or agent
        /// </summary>
        public void StyleMessage(string message, bool isUser)
        {
            // 1. Remove background color
            if (backgroundImage != null)
            {
                Color bgColor = backgroundImage.color;
                bgColor.a = 0; // Make transparent
                backgroundImage.color = bgColor;
            }

            // 4 & 5. Add prefix based on sender
            string displayText = message;
            if (isUser)
            {
                displayText = "[AlHaetham] " + message;
            }
            else
            {
                displayText = "[Balbol] " + message;
            }

            // Set text and color
            if (textComponent != null)
            {
                textComponent.text = displayText;
                textComponent.color = isUser ? userTextColor : agentTextColor;
            }
            else if (tmpTextComponent != null)
            {
                tmpTextComponent.text = displayText;
                tmpTextComponent.color = isUser ? userTextColor : agentTextColor;
            }

            // 3. Align to left
            if (horizontalLayoutGroup != null)
            {
                horizontalLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
            }
        }
    }
}
