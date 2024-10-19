using UnityEngine;

[System.Serializable]
public enum RainbowColorType
{
    Red,
    Orange,
    Yellow,
    Green,
    Cyan,
    Blue,
    Purple,
}

public class RainbowColor : MonoBehaviour
{
    private Renderer objectRenderer;

    #region 颜色属性
    public RainbowColorType CurrentColorType
    {
        get { return currentColorType; }
        set { ChangeColor(value); }
    }

    [SerializeField]
    private RainbowColorType currentColorType = RainbowColorType.Red;
    #endregion

    #region 音效属性
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip colorChangeClip; // 颜色变化时的音效
    #endregion
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>(); // 获取 AudioSource 组件
        ChangeColor(currentColorType);
    }

    // 当属性在检查器中变化时调用
    private void OnValidate()
    {
        ChangeColor(currentColorType);
    }

    public void ChangeColor(RainbowColorType colorType)
    {
        Color color = Color.white; // 默认颜色

        switch (colorType)
        {
            case RainbowColorType.Red:
                color = new Color(1.0f, 0.7f, 0.7f); // #FFB3B3
                SetPitch(1.2f); // 设置音调
                break;
            case RainbowColorType.Orange:
                color = new Color(1.0f, 0.8f, 0.6f); // #FFCC99
                SetPitch(1.1f); // 设置音调
                break;
            case RainbowColorType.Yellow:
                color = new Color(1.0f, 1.0f, 0.6f); // #FFFF99
                SetPitch(1.0f); // 设置音调
                break;
            case RainbowColorType.Green:
                color = new Color(0.8f, 1.0f, 0.8f); // #CCFFCC
                SetPitch(0.9f); // 设置音调
                break;
            case RainbowColorType.Cyan:
                color = new Color(0.6f, 1.0f, 1.0f); // #99FFFF
                SetPitch(0.8f); // 设置音调
                break;
            case RainbowColorType.Blue:
                color = new Color(0.7f, 0.8f, 1.0f); // #B3CFFF
                SetPitch(0.7f); // 设置音调
                break;
            case RainbowColorType.Purple:
                color = new Color(0.9f, 0.7f, 0.9f); // #E6B3E6
                SetPitch(0.6f); // 设置音调
                break;
        }

        if (objectRenderer != null)
        {
            objectRenderer.material.color = color; // 改变物体颜色
            currentColorType = colorType; // 记录当前颜色
        }

        PlayColorChangeSound(); // 播放颜色变化音效
    }

    private void SetPitch(float pitch)
    {
        if (audioSource != null)
        {
            audioSource.pitch = pitch; // 设置音调
        }
    }

    private void PlayColorChangeSound()
    {
        if (audioSource != null && colorChangeClip != null)
        {
            audioSource.PlayOneShot(colorChangeClip); // 播放音效
        }
    }
}
