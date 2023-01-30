using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1.5f;
    
    public void EndGame()
    {
        if(gameHasEnded == false)
        { 
            Debug.Log("GAME OVER");
            gameHasEnded = true;
           // Invoke("Restart", restartDelay);
        }
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
