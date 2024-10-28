using System.Collections;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public string toggleTag = "Light_Bullet"; // 触发开关的标签
    public RainbowColor[] lightSource; // 灯光源
    public float delayBetweenLights = 0.5f; // 每个光源之间的时间间隔

    private void OnEnable()
    {
        foreach (var source in lightSource)
        {
            source.ToggleOff();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(toggleTag))
        {
            StartCoroutine(ToggleLightsSequentially());
        }
    }

    private IEnumerator ToggleLightsSequentially()
    {
        foreach (var source in lightSource)
        {
            source.Toggle(); // 点亮当前光源
            yield return new WaitForSeconds(delayBetweenLights); // 等待设定的时间
        }
    }
}
