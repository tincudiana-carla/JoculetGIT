using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void OnEnable()
    {
        Health.OnPlayerDeath += EnableGameOverMenu;
    }
    private void OnDisable()
    {
        Health.OnPlayerDeath -= EnableGameOverMenu;
    }
    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }
    public void RestartMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    /*private void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
  
}
