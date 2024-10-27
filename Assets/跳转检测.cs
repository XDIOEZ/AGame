using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public string nextSceneName;         // 下一场景名称
    public Image fadeImage;              // 淡出效果的UI Image
    public float fadeDuration = 1.5f;    // 淡出持续时间

    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否是主角触发
        if (!isTransitioning && other.CompareTag("Player"))
        {
            StartCoroutine(FadeAndSwitchScene());
        }
    }

    IEnumerator FadeAndSwitchScene()
    {
        isTransitioning = true;

        // 淡出效果
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 确保完全黑屏后加载场景
        fadeImage.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene(nextSceneName);
    }
}

