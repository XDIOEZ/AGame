using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuPanel : BasePanel
{
    private void Start()
    {
        HideMe();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

    public void ContinueButtonClick()
    {
        HideMe();
    }

    public void ExitButtonClick()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("开始场景");
    }
}
