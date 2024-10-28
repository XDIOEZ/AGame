using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGamePanel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ShowPanel<GameMenuPanel>("GameMenuPanel");
        }
    }
}
