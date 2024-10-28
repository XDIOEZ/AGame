using UnityEngine;

public class ShowUIOnTrigger : MonoBehaviour
{
    public GameObject uiElement; // 拖拽你的UI元素到这里

    private void Start()
    {
        // 确保UI初始隐藏
        uiElement.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查进入触发器的物体是否是主角
        if (other.CompareTag("Player")) // 确保主角有 "Player" 标签
        {
            uiElement.SetActive(true); // 显示UI
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 退出触发器时隐藏UI
        if (other.CompareTag("Player"))
        {
            uiElement.SetActive(false); // 隐藏UI
        }
    }
}
