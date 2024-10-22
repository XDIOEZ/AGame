using UnityEngine;

public class PlayerLittleState : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ManSpriteRenderer; // 大人状态的 SpriteRenderer
    [SerializeField] private CapsuleCollider2D ManCollider; // 大人状态的 CapsuleCollider2D
    [SerializeField] private bool isLittle = false; // 跟踪当前状态，默认为大人状态

    [SerializeField] private float initialGravityScale; // 初始重力缩放值
    [SerializeField] private float initialDrag; // 初始摩擦力值

    void Start()
    {
        // 记录初始的重力缩放和摩擦力值
        var rb2D = GetComponentInParent<Rigidbody2D>();
        initialGravityScale = rb2D.gravityScale;
        initialDrag = rb2D.drag;
    }

    // 玩家切换到小人状态
    public void ChangeToLittle()
{
    if (!isLittle) // 只有在当前不是小人状态时才切换
    {
        // 获取 Rigidbody2D 组件
        Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
        
        // 设置 Rigidbody2D 为静止状态
        rb.velocity = Vector2.zero; // 将速度设置为零
        //rb.isKinematic = true; // 设置为动态刚体，使其不受物理影响

        ManSpriteRenderer.enabled = false; // 关闭大人状态的渲染
        ManCollider.enabled = false; // 关闭大人状态的碰撞体
        // 启用小人状态的组件
        GetComponent<SpriteRenderer>().enabled = true; // 启用小人状态的SpriteRenderer
        GetComponent<CapsuleCollider2D>().enabled = true; // 启用小人状态的Collider
        GetComponentInParent<PlayerMovement_Temp>().enabled = false; // 禁用大人状态的移动组件
        GetComponent<LittleStateMovement>().enabled = true; // 启用小人状态的移动组件
        isLittle = true; // 更新状态为小人
        Debug.Log("切换到小人状态"); // 调试信息
        ChangeToLittleAboutGravity(); // 禁用父对象的重力
    }
}


    // 玩家切换为大人状态
    public void ChangeToBig()
    {
        if (isLittle) // 只有在当前是小人状态时才切换
        {
            ManSpriteRenderer.enabled = true; // 重新启用大人状态的渲染
            ManCollider.enabled = true; // 重新启用大人状态的碰撞体
            // 关闭小人状态的组件
            GetComponent<SpriteRenderer>().enabled = false; // 关闭小人状态的SpriteRenderer
            GetComponent<CapsuleCollider2D>().enabled = false; // 关闭小人状态的Collider
            GetComponentInParent<PlayerMovement_Temp>().enabled = true; // 启用大人状态的移动组件
            GetComponent<LittleStateMovement>().enabled = false; // 关闭小人状态的移动组件
            isLittle = false; // 更新状态为大人
            Debug.Log("切换到大人状态"); // 调试信息
            ChangeToBigAboutGravity(); // 恢复父对象的重力
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C)) // 切换状态
        {
            SwitchPlayerState();
        }
    }

    public void ChangeToBigAboutGravity()
    {
        // 恢复父对象的重力和摩擦力
        Rigidbody2D rb2D = GetComponentInParent<Rigidbody2D>();
        Debug.Log("恢复父对象的重力");
        rb2D.gravityScale = initialGravityScale;
        rb2D.drag = initialDrag;
    }

    public void ChangeToLittleAboutGravity()
    {
        // 禁用父对象的重力
        var rb2D = GetComponentInParent<Rigidbody2D>(); // 获取父对象的 Rigidbody2D 组件
        rb2D.gravityScale = 0; // 设置父对象的重力为0
        rb2D.drag = 0; // 设置摩擦力为0，确保速度恒定
    }

    public void SwitchPlayerState()
    {

        if (isLittle)
        {
            ChangeToBig(); // 当前是小人，切换到大人
        }
        else
        {
            ChangeToLittle(); // 当前是大人，切换到小人
        }
    }
}
