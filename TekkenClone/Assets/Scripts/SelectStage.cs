using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour
{
    public void OnYesButtonClick(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
}
