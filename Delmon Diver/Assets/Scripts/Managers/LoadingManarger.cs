using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text loadingText;
    [SerializeField] private TMP_Text tipText;


    [Header("Loading Settings")]
    [SerializeField] private float fakeLoadTime = 3f;
    [SerializeField] private string sceneToLoad = "L1_BrokenBoat";


    [Header("Tips Settings")]
    [SerializeField] private float tipChangeInterval = 2f;

    private string[] tips =
    {
        "Tip: After the shipwreck, search the water for floating resources.",
        "Tip: Wood and materials near the broken boat are your first survival tools.",
        "Tip: You can dive underwater to discover hidden resources and areas.",
        "Tip: Build a small boat first before attempting long sea travel.",
        "Tip: Weather like storms and rain can damage your boat—stay prepared.",
        "Tip: Sea creatures can be dangerous—avoid or learn their patterns.",
        "Tip: Explore islands carefully; some contain rare materials and food.",
        "Tip: Repair your boat often to survive longer journeys.",
        "Tip: The parrot guide gives useful hints—pay attention to it.",
        "Tip: Not all treasures are required—choose your path wisely.",
        "Tip: Better tools help you gather resources faster.",
        "Tip: Caves may hide treasure—but also danger.",
        "Tip: Food is essential—don’t explore too far without it.",
        "Tip: Upgrading your boat is key to reaching the main island.",
        "Tip: Your choices affect the ending—explore or return home?"
    };

    void Start()
    {
        StartCoroutine(LoadScene());
        StartCoroutine(RotateTips());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;
            float fakeProgress = Mathf.Clamp01(timer / fakeLoadTime);
            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);
            float progress = Mathf.Min(fakeProgress, realProgress);

            progressBar.value = progress;
            loadingText.text = $"Loading... {Mathf.FloorToInt(progress * 100)}%";

            if (progress >= 1f && operation.progress >= 0.9f)
            {
                loadingText.text = "Loading... 100%";
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    IEnumerator RotateTips()
    {
        int lastIndex = -1;

        while (true)
        {
            int index;
            do
            {
                index = Random.Range(0, tips.Length);
            } while (index == lastIndex);
            lastIndex = index;
            yield return StartCoroutine(FadeText(1f, 0f, 0.5f));
            tipText.text = tips[index];
            yield return StartCoroutine(FadeText(0f, 1f, 0.5f));
            yield return new WaitForSeconds(tipChangeInterval);
        }
    }

    IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        Color color = tipText.color;
        while (time < duration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            tipText.color = new Color(color.r, color.g, color.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }
        tipText.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}