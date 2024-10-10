// 单例父类
//
// 使用场景:(可挂载--可访问场景节点的函数方法，可全局访问)
// 比如一个场景中有多个MonoBehaviour组件，需要实现单例模式，那么可以直接继承这个类，然后到场景中挂载这个脚本，最后在脚本中调用GetInstance方法即可。
// 
// 
// 使用案例:
// 
// 假设有个需要作为单例的组件叫`MyMono`，那么可以这样写：
// public class MyMono : SingletonAutoMono<MyMono>
// {
//     //此处添加自己的函数逻辑, 假设这里有一个方法叫`DoSomething()`
// }
// 
// 然后在需要的地方调用 `MyMono.GetInstance()` 即可获得单例的`MyMono`对象
// public class Test : MonoBehaviour
// {
//     void Start()
//     {
//         MyMono.GetInstance().DoSomething();  // 通过静态方法访问
//         MyMono.Instacne.DoSomething();       // 通过静态属性访问
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//C#中 泛型知识点
//设计模式 单例模式的知识点
//继承了 MonoBehaviour 的 单例模式对象 需要我们自己保证它的位移性
public class SingletonMono<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T GetInstance()
    {
        //继承了Mono的脚本 不能够直接new
        //只能通过拖动到对象上 或者 通过 加脚本的api AddComponent去加脚本
        //U3D内部帮助我们实例化它
        return instance;
    }

    protected virtual void Awake()
    {
        instance = this as T;
    }

    public static T Instacne
    {
        get { return GetInstance(); }
    }
}
