// 使用场景:
// 比如一个场景中有多个MonoBehaviour，需要实现单例模式，但是又不想去引擎手动挂载，那么可以直接继承这个类，然后在脚本中调用GetInstance方法即可。
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
// 然后在需要的地方调用 `MyMono.GetInstance()` 即可获得单例的MyMono对象
// public class Test : MonoBehaviour
// {
//     void Start()
//     {
//         MyMono.GetInstance().DoSomething();
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
    private static T instance;

    public static T GetInstance()
    {
        if( instance == null )
        {
            GameObject obj = new GameObject();
            //设置对象的名字为脚本名
            obj.name = typeof(T).ToString();
            //让这个单例模式对象 过场景 不移除
            //因为 单例模式对象 往往 是存在整个程序生命周期中的
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }
    public static T Instacne
    {
        get { return GetInstance(); }
    }

}
