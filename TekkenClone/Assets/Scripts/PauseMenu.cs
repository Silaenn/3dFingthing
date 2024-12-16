using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject buttonPause;
    private bool isPaused = false;


    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Pause(){
        isPaused = true;
        buttonPause.SetActive(false);
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    public void Resume(){
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        buttonPause.SetActive(true);
    }

     public void LoadMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void QuitGame(){
        Application.Quit();
    }

}
