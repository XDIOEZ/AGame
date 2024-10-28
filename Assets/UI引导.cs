using UnityEngine;
using UnityEngine.UI;

public class ShowTextOnEnter : MonoBehaviour
{
    public GameObject player;         // 主角对象
    public Text displayText;          // 要显示的文字

    private void Start()
    {
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false);  // 开始时隐藏文字
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检测到主角进入
        if (other.gameObject == player)
        {
            displayText.gameObject.SetActive(true);   // 显示文字
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 检测到主角离开
        if (other.gameObject == player)
        {
            displayText.gameObject.SetActive(false);  // 隐藏文字
        }
    }

}
