using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameObject resultPanel;
    public Text resultText;
    public GameObject buttonPause;

    public FightingController[] fightingControllers;
    public OpponentAI[] opponentAI;


    private void Update() {
        foreach(FightingController fightingController in fightingControllers){
            if(fightingController.gameObject.activeSelf && fightingController.currentHealth <= 0){
                SetResult("You Lose");
                return;
            }
        }

        foreach(OpponentAI opponentAI in opponentAI){
            if(opponentAI.gameObject.activeSelf && opponentAI.currentHealth <= 0){
                SetResult("You Win");
                return;
            }
        }
    }
    void SetResult(string result){
        resultText.text = result;
        resultPanel.SetActive(true);
        buttonPause.SetActive(false);
        Time.timeScale = 0f;
    }

    public void LoadMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
