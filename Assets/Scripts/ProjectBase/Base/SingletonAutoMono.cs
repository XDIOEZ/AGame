// 单例父类
//
// 使用场景:(可挂载--可访问场景节点的函数方法，可全局访问，可自动创建)
// 比如一个场景中有多个MonoBehaviour组件，需要实现单例模式，但是又不想去引擎手动挂载，那么可以直接继承这个类，然后在脚本中调用GetInstance方法即可。
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
//继承这种自动创建的 单例模式基类 不需要我们手动去拖 或者 api去加了
//想用他 直接 GetInstance就行了
public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    // 核心 私有单例对象
    private static T instance;

    // 安全 线程锁
    private static object lockObject = new object();

    #region 访问接口
    public static T GetInstance()
    {
        // 使用双重检查锁定确保线程安全
        if (instance == null)
        {
            lock (lockObject)
            {
                if (instance == null)
                {
                    // 尝试获取已存在的单例对象
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        // 创建新单例对象
                        GameObject singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";
                        DontDestroyOnLoad(singletonObject);
                    }
                }
            }
        }

        return instance;

        // if( instance == null )
        // {
        //     GameObject obj = new GameObject();
        //     //设置对象的名字为脚本名
        //     obj.name = typeof(T).ToString();
        //     //让这个单例模式对象 过场景 不移除
        //     //因为 单例模式对象 往往 是存在整个程序生命周期中的
        //     DontDestroyOnLoad(obj);
        //     instance = obj.AddComponent<T>();
        // }
        // return instance;
    }

    public static T Instacne
    {
        get { return GetInstance(); }
    }
    #endregion

    #region 处理意外单例
    // 用于在加载场景或对象时处理已有此类对象的情况
    protected virtual void Awake()
    {
        // instance没有此类单例对象的记录
        if (instance == null)
        {
            // 设置此类单例对象为自身
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        // instance已有此类单例对象的记录
        else
        {
            // 销毁自身
            Destroy(gameObject);
        }
    }

    // 在销毁对象时处理重新创建单例对象的情况
    protected virtual void OnDestroy()
    {
        // 如果当前销毁的对象是单例对象
        if (instance == this)
        {
            // 将单例对象置空(为创建单例对象留出空间)
            instance = null;
        }
    }
    #endregion
}
