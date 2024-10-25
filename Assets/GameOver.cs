using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Include this for scene management

public class GameOver : MonoBehaviour
{
    [SerializeField] private float delayBeforeGameOver = 2f; // Delay time before transitioning

    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.AddEventListener("BossDead", OnGameOver);
    }

    void OnGameOver()
    {
        Debug.Log("Game Over");
        StartCoroutine(DelayedGameOver());
    }

    // Coroutine for delayed transition to the game over scene
    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(delayBeforeGameOver);
        SceneManager.LoadScene("GameOverScene"); // Change to your actual game over scene name
    }

    void Update()
    {
        // No need to use Update for this functionality
    }
}
