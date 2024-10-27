using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFadeOut : MonoBehaviour
{
    public Image uiElement;          // 要渐变消失的UI元素
    public float fadeDuration = 1f;  // 淡出持续时间

    public void OnButtonClick()
    {
        StartCoroutine(FadeOutUI());
    }

    IEnumerator FadeOutUI()
    {
        Color originalColor = uiElement.color;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            uiElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // 确保最终Alpha为0
        uiElement.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        uiElement.gameObject.SetActive(false); // 可选：淡出后隐藏UI元素
    }
}
