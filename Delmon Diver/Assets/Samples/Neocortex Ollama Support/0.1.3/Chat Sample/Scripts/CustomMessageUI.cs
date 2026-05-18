using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Neocortex.Samples
{
    public class CustomMessageUI : MonoBehaviour
    {
        // USER = Dark Brown
        [SerializeField]
        private Color userMessageColor =
            new Color(0.478f, 0.306f, 0.157f); // #7A4E28

        // AGENT = Parchment
        [SerializeField]
        private Color agentMessageColor =
            new Color(0.910f, 0.835f, 0.710f); // #E8D5B5

        private Text textComponent;
        private TextMeshProUGUI tmpTextComponent;
        private Image backgroundImage;
        private LayoutElement layoutElement;
        private HorizontalLayoutGroup horizontalLayoutGroup;

        private void Awake()
        {
            // Get text components
            textComponent = GetComponentInChildren<Text>(true);
            tmpTextComponent = GetComponentInChildren<TextMeshProUGUI>(true);

            // Get background image
            backgroundImage = transform.parent?.GetComponent<Image>();

            // Get layout components
            layoutElement = GetComponent<LayoutElement>();
            horizontalLayoutGroup = transform.parent?.GetComponent<HorizontalLayoutGroup>();

            // Remove background visibility
            if (backgroundImage != null)
            {
                backgroundImage.color = Color.clear;
            }
        }

        /// <summary>
        /// Style the message based on sender
        /// </summary>
        public void StyleMessage(string message, bool isUser)
        {
            // Remove background color
            if (backgroundImage != null)
            {
                Color bgColor = backgroundImage.color;
                bgColor.a = 0f;
                backgroundImage.color = bgColor;
            }

            // Add sender prefix
            string displayText = isUser
                ? "[AlHaetham] " + message
                : "[Balbol] " + message;

            // Apply to legacy Text
            if (textComponent != null)
            {
                textComponent.text = displayText;

                textComponent.color = isUser
                    ? userMessageColor
                    : agentMessageColor;
            }

            // Apply to TextMeshPro
            if (tmpTextComponent != null)
            {
                tmpTextComponent.text = displayText;

                tmpTextComponent.color = isUser
                    ? userMessageColor
                    : agentMessageColor;

                // Force alignment
                tmpTextComponent.alignment = TextAlignmentOptions.TopLeft;

                // Better readability
                tmpTextComponent.enableWordWrapping = true;
            }

            // Align layout left
            if (horizontalLayoutGroup != null)
            {
                horizontalLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
            }

            // Flexible width
            if (layoutElement != null)
            {
                layoutElement.flexibleWidth = 1;
            }
        }
    }
}