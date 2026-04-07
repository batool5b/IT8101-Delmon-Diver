using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TypewriterText : MonoBehaviour
{
    public Text uiText;
    public float letterDelay = 0.05f;
    public float startDelay = 1.2f;

    private string fullText;

    void Start()
    {
        // Take the text already written in UI
        fullText = uiText.text;

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        yield return new WaitForSeconds(startDelay);

        uiText.text = "";

        foreach (char letter in fullText)
        {
            uiText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }
}