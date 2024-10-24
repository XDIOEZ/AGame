using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassLevelCheck : MonoBehaviour
{
    public GameObject[] levelObjects;
    
    bool isPassed = false;
    //如果挂接的levelObjects全部是激活状态则返回true
    public bool CheckPassLevel()
    {
        foreach (GameObject obj in levelObjects)
        {
            if (!obj.activeSelf)
            {
                return false;
            }
            
        }
        EventCenter.GetInstance().EventTrigger("7色被点亮");
        Debug.Log("Pass Level!");
        return true;
    }
    private void Update()
    {
        if(isPassed == false)
            isPassed =  CheckPassLevel();
    }
}
