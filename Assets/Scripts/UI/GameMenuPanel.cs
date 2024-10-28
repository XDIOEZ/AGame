using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuPanel : MonoBehaviour
{
    private void Start()
    {
        HideMe();
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

    private void HideMe()
    {
        gameObject.SetActive(false);
    }
}
