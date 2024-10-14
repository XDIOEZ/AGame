//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerController : MonoBehaviour
//{
//    private Rigidbody2D rb;
//    private Vector2 moveInput;
//    private AGameInputSystem playerInputActions;

//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        playerInputActions = new AGameInputSystem();

//        // 绑定输入事件
//        playerInputActions.PlayerAction.Move.performed += ctx =>
//            moveInput = ctx.ReadValue<Vector2>();
//        playerInputActions.PlayerAction.Move.canceled += ctx => moveInput = Vector2.zero;
//        playerInputActions.PlayerAction.Attack.performed += ctx => Shoot();
//    }

//    private void OnEnable()
//    {
//        playerInputActions.Enable();
//    }

//    private void OnDisable()
//    {
//        playerInputActions.Disable();
//    }

//    private void Update()
//    {
//        // 处理移动
//        Vector2 move = new Vector2(moveInput.x, moveInput.y) * Time.deltaTime * 20;
//        rb.MovePosition(rb.position + move);

//        // 处理朝向
//        if (moveInput.x != 0 && Mathf.Sign(moveInput.x) != Mathf.Sign(transform.localScale.x))
//        {
//            Vector3 newScale = transform.localScale;
//            newScale.x *= -1;
//            transform.localScale = newScale;
//        }
//    }

//    private void Shoot()
//    {
//        //TODO: 实现发射子弹的逻辑
//        Debug.Log("朝当前方向发射子弹");
//        //TODO: 这里可以实例化子弹的逻辑
//    }
//}
