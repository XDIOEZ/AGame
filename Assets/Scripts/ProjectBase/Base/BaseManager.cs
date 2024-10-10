// 使用场景:(可全局访问，仅包含自定义方法--不包含Unity内容)
// 比如想在Unity外单独写一个自己的日志管理器，那就可以继承BaseManager<T>类，然后在自己的类中实现自己的日志记录方法即可。
// 
// 
// 使用案例:
// 
// 假设做一个日志管理器`LogManager`，那么可以这样写：
// public class LogManager : BaseManager<LogManager>
// {
//     // 此处添加自己的函数逻辑, 假设这里有一个方法叫`DoSomething()`
//     // 注意这里无法直接访问Unity的API，比如:无法直接获取某个场景节点的名称
//     // 间接获取的方法涉及其他脚本的引用  ————  海阔任鱼游_天高任鸟飞   ————
// }
// 
// 然后在需要的地方调用 `LogManager.GetInstance()` 即可获得单例的`LogManager`对象
// public class Test : MonoBehaviour
// {
//     void Start()
//     {
//         LogManager.GetInstance().DoSomething();      // 通过静态方法访问
//         LogManager.Instacne.DoSomething();           // 通过静态属性访问
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1.C#中 泛型的知识
//2.设计模式中 单例模式的知识
public class BaseManager<T> where T:new()
{
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }
    public static T Instacne
    {
        get { return GetInstance(); }
    }
}
