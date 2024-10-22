using Unity.VisualScripting;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public string toggleTag = "Light_Bullet"; // 触发开关的标签
    public RainbowColor[] lightSource; // 灯光源

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
            foreach (var source in lightSource)
            {
                source.Toggle();
            }
        }
    }
}
