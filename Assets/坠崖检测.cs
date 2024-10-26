using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerFallReset : MonoBehaviour
{
    public float fallThreshold = -10f;  // 坠崖高度
    public float resetDelay = 1.5f;     // 重置延迟时间
    public Image fadeImage;             // 用于淡出的UI Image

    private bool isFalling = false;     // 标识是否已经开始坠崖重置流程

    void Update()
    {
        if (!isFalling && transform.position.y < fallThreshold)
        {
            StartCoroutine(ResetLevelWithFade());  // 开始坠崖处理
        }
    }

    IEnumerator ResetLevelWithFade()
    {
        isFalling = true;

        // 淡出效果
        for (float t = 0; t < 1; t += Time.deltaTime / resetDelay)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(0, 1, t);
            fadeImage.color = color;
            yield return null;
        }

        // 延迟结束后重新加载场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
