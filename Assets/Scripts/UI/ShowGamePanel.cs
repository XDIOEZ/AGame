using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGamePanel : MonoBehaviour
{
    public GameObject gamePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            gamePanel.SetActive(!gamePanel.activeSelf);
        }
    }
}
