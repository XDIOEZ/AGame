// using UnityEngine;

// public class RockTrigger : MonoBehaviour
// {
//     private MovableRock movableRock;

//     void Awake()
//     {
//         // 获取 MovableRock 组件
//         movableRock = GetComponentInParent<MovableRock>();
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             // 当玩家进入触发范围时，设置玩家对象
//             movableRock.SetPlayer(other.gameObject, Vector3.zero);
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             // 当玩家离开触发范围时，停止交互
//             movableRock.ToggleInteraction();
//             movableRock.SetPlayer(null, Vector3.zero); // 清空玩家对象
//         }
//     }

//     private void Update()
//     {
//         if (movableRock != null && movableRock.player != null)
//         {
//             // 检测交互键
//             if (Input.GetKeyDown(KeyCode.F))
//             {
//                 movableRock.ToggleInteraction();
//             }
//         }
//     }
// }
