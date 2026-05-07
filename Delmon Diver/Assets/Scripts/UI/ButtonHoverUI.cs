using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text textUi;

    public Vector3 normalScale = Vector3.one;
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);

    public void OnPointerEnter(PointerEventData eventData)
    {
        textUi.transform.localScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textUi.transform.localScale = normalScale;
    }
}
