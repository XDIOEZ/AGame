using UnityEngine;

public class ToggleComponent : MonoBehaviour
{
    public MonoBehaviour targetComponent; // 要控制的组件

    // 调用此方法切换组件启用状态
    public void ToggleComponentActive()
    {
        if (targetComponent != null)
        {
            targetComponent.enabled = !targetComponent.enabled;
        }
    }
}
