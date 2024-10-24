using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassLevelCheck : MonoBehaviour
{
    public GameObject[] levelObjects;
    
    bool isPassed = false;
    //����ҽӵ�levelObjectsȫ���Ǽ���״̬�򷵻�true
    public bool CheckPassLevel()
    {
        foreach (GameObject obj in levelObjects)
        {
            if (!obj.activeSelf)
            {
                return false;
            }
            
        }
        EventCenter.GetInstance().EventTrigger("7ɫ������");
        Debug.Log("Pass Level!");
        return true;
    }
    private void Update()
    {
        if(isPassed == false)
            isPassed =  CheckPassLevel();
    }
}
