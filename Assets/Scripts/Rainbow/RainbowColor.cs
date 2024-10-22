using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public static class ComponentExtensions
{
    // 尝试获取多个组件的扩展方法
    public static bool TryGetComponents<T>(this GameObject gameObject, out T[] components)
        where T : Component
    {
        components = gameObject.GetComponents<T>();
        return components.Length > 0;
    }
}

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

public class RainbowColor : MonoBehaviour, IToggleable
{
    [Tooltip("多个音色?")]
    public bool isPolyphonic = true;
    private Renderer[] objectRenderers;
    private Light2D light2D;

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

    [SerializeField]
    private AudioClip[] colorChangeClips; // 颜色变化时的音效数组
    #endregion
    private void Start()
    {
        light2D = GetComponent<Light2D>(); // 获取 Light2D 组件
        objectRenderers = GetComponents<Renderer>();
        audioSource = GetComponent<AudioSource>(); // 获取 AudioSource 组件
    }

    private void OnEnable()
    {
        ChangeColor(currentColorType);
    }

#if UNITY_EDITOR
    // 当属性在检查器中变化时调用
    private void OnValidate()
    {
        if (Application.isPlaying && gameObject.activeSelf)
            ChangeColor(currentColorType);
    }
#endif

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

        if (objectRenderers != null)
        {
            foreach (Renderer renderer in objectRenderers)
            {
                renderer.material.color = color; // 改变子物体颜色
            }
        }
        else if (gameObject.TryGetComponents(out objectRenderers))
        {
            foreach (Renderer renderer in objectRenderers)
            {
                renderer.material.color = color; // 改变子物体颜色
            }
        }

        if (light2D != null)
        {
            light2D.color = color; // 改变光源颜色
        }
        else if (TryGetComponent(out light2D))
        {
            light2D.color = color; // 改变光源颜色
        }

        currentColorType = colorType; // 记录当前颜色

        if (isPolyphonic)
        {
            PlayColorChangeSound(colorType); // 播放颜色变化音效
        }
        else
        {
            PlayColorChangeSound(); // 播放颜色变化音效
        }
    }

    private void PlayColorChangeSound()
    {
        if (audioSource != null && colorChangeClip != null)
        {
            audioSource.PlayOneShot(colorChangeClip); // 播放音效
        }
    }

    private void PlayColorChangeSound(RainbowColorType colorType)
    {
        StartCoroutine(CreateAndPlaySound(colorType));
    }

    private IEnumerator CreateAndPlaySound(RainbowColorType colorType)
    {
        // 等待下一帧
        yield return null;

        // 新建一个 GameObject 用于 AudioSource
        GameObject audioObject = new GameObject("AudioSourceObject");
        AudioSource newAudioSource = audioObject.AddComponent<AudioSource>();

        switch (colorType)
        {
            case RainbowColorType.Red:
                newAudioSource.clip = colorChangeClips[0];
                break;
            case RainbowColorType.Orange:
                newAudioSource.clip = colorChangeClips[1];
                break;
            case RainbowColorType.Yellow:
                newAudioSource.clip = colorChangeClips[2];
                break;
            case RainbowColorType.Green:
                newAudioSource.clip = colorChangeClips[3];
                break;
            case RainbowColorType.Cyan:
                newAudioSource.clip = colorChangeClips[4];
                break;
            case RainbowColorType.Blue:
                newAudioSource.clip = colorChangeClips[5];
                break;
            case RainbowColorType.Purple:
                newAudioSource.clip = colorChangeClips[6];
                break;
        }

        // 播放音效
        newAudioSource.Play();

        // 销毁 GameObject，避免内存泄漏
        Destroy(audioObject, newAudioSource.clip.length);
    }

    private void SetPitch(float pitch)
    {
        if (audioSource != null)
        {
            audioSource.pitch = pitch; // 设置音调
        }
    }

    public void Toggle()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }

    public void ToggleOff()
    {
        this.gameObject.SetActive(false);
    }
}
