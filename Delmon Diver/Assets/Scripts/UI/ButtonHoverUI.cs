using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text buttonText;

    public int normalSize = 36;
    public int hoverSize = 46;

    void Start()
    {
        buttonText.fontSize = normalSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontSize = hoverSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontSize = normalSize;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonText.fontSize = normalSize;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void OnDisable()
    {
        buttonText.fontSize = normalSize;
    }
}