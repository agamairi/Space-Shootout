using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    [SerializeField]
    private Text PauseText;
    public bool isCoopmmode = false;
    private bool isGamePause = false;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && _isGameOver == true)
        {
            SceneManager.LoadScene(0); //main menu scene
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Backspace)){
            SceneManager.LoadScene(0);
        }
        if(isGamePause == false && Input.GetKeyDown(KeyCode.P)){
            isGamePause = true;
            Time.timeScale = 0f;
            PauseText.gameObject.SetActive(true);
        }
        if(isGamePause == true && Input.GetKeyDown(KeyCode.R)){
            Time.timeScale = 1f;
            PauseText.gameObject.SetActive(false);
            isGamePause = false;
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
