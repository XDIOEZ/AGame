using UnityEngine;

/// <summary>
/// PlayerLittleState类控制玩家在两种状态间的切换：大人状态和小人状态。
/// <para>使用说明：</para>
/// <para>1. 将此脚本挂载到玩家游戏对象上。</para>
/// <para>2. 在Unity编辑器中，确保将大人状态的SpriteRenderer和Collider、
///      小人状态的SpriteRenderer和Collider等通过SerializeField属性进行初始化。</para>
/// <para>3. 调用ChangeToLittle()方法以切换到小人状态，调用ChangeToBig()方法以切换回大人状态。</para>
/// <para>4. 确保在适当的时候调用Update()方法，以监测玩家状态和切换。</para>
/// <para>5. 使用SwitchPlayerState()方法来根据条件自动切换状态。</para>
/// </summary>
public class PlayerLittleState : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer manSpriteRenderer; // 大人状态的 SpriteRenderer

    [SerializeField]
    private BoxCollider2D manCollider; // 大人状态的 CapsuleCollider2D

    [SerializeField]
    private bool isLittle = false; // 跟踪当前状态，默认为大人状态

    [SerializeField]
    private float initialGravityScale; // 初始重力缩放值

    [SerializeField]
    private float initialDrag; // 初始摩擦力值

    [Header("检测参数")]
    public Vector2 offset; // 物差值
    public float detectionRange; // 检测范围
    public LayerMask detectionObjects; // 检测对象

    [Header("状态")]
    public bool isContact; // 接触
    public bool canTransformToLightBall = true; // 满足变成光球条件
    public bool isLightBallState = false; // 光球态

    public void Check()
    {
        isContact = Physics2D.OverlapCircle(
            (Vector2)transform.position + offset,
            detectionRange,
            detectionObjects
        );
    }

    void Start()
    {
        // 记录初始的重力缩放和摩擦力值
        var rb2D = GetComponentInParent<Rigidbody2D>();
        initialGravityScale = rb2D.gravityScale;
        initialDrag = rb2D.drag;

        EventCenter.GetInstance().AddEventListener("主角状态切换", SwitchPlayerState);

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

            manSpriteRenderer.enabled = false; // 关闭大人状态的渲染
            manCollider.enabled = false; // 关闭大人状态的碰撞体
            // 启用小人状态的组件
            GetComponent<SpriteRenderer>().enabled = true; // 启用小人状态的SpriteRenderer
            GetComponent<CapsuleCollider2D>().enabled = true; // 启用小人状态的Collider
            GetComponentInParent<PlayerMove>().isLockingMove = true; // 禁用大人状态的移动组件

            GetComponentInParent<PlayerMove>().isJumping = false; // 禁用大人状态的移动组件
            GetComponentInParent<PlayerMove>().isDashing = false; // 禁用大人状态的移动组件
            GetComponentInParent<PlayerMove>().isMoving = false; // 禁用大人状态的移动组件
            GetComponentInParent<PlayerMove>().moveDirection = Vector2.zero; // 禁用大人状态的移动组件

            GetComponent<LittleStateMovement>().enabled = true; // 启用小人状态的移动组件
            isLittle = true; // 更新状态为小人
            Debug.Log("切换到小人状态"); // 调试信息
            ChangeToLittleAboutGravity(); // 禁用父对象的重力

            isLightBallState = true;
        }
    }
    // 玩家切换为大人状态
    public void ChangeToBig()
    {
        if (isLittle) // 只有在当前是小人状态时才切换
        {
            manSpriteRenderer.enabled = true; // 重新启用大人状态的渲染
            manCollider.enabled = true; // 重新启用大人状态的碰撞体
            // 关闭小人状态的组件
            GetComponent<SpriteRenderer>().enabled = false; // 关闭小人状态的SpriteRenderer
            GetComponent<CapsuleCollider2D>().enabled = false; // 关闭小人状态的Collider
            GetComponentInParent<PlayerMove>().isLockingMove = false; // 启用大人状态的移动组件
            GetComponent<LittleStateMovement>().enabled = false; // 关闭小人状态的移动组件
            isLittle = false; // 更新状态为大人
            Debug.Log("切换到大人状态"); // 调试信息
            ChangeToBigAboutGravity(); // 恢复父对象的重力

            isLightBallState = false;
        }
    }

    void Update()
    {
        Check();
        SwitchPlayerState(); // 切换状
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("切换状态");
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



    /// <summary>
    /// 切换玩家状态函数
    /// </summary>
    public void SwitchPlayerState()
    {
        if (isLittle && isContact)
        {
            ChangeToBig(); // 当前是小人，切换到大人
        }
        else if (canTransformToLightBall)
        {
            ChangeToLittle(); // 当前是大人，切换到小人
            canTransformToLightBall = false;
        }

        if (isLittle && GetComponentInParent<PlayerMove>().dashCount <= 0)
        {
            GetComponentInParent<PlayerMove>().dashCount++;
        }
        if (isLittle && GetComponentInParent<PlayerMove>().jumpCount <= 0)
        {
            GetComponentInParent<PlayerMove>().jumpCount++;
        }
    }
}
