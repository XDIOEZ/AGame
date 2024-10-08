using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void Update()
    {
        InputSetting();
    }
    public void InputSetting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("¿Õ¸ñ¼ü°´ÏÂ");
        }
    }
}
