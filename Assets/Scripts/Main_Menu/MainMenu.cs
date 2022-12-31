using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start(){
    GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void LoadSinglePlayer()
    {
        SceneManager.LoadScene(1); //1 is the Single player mode scene
    }
    public void LoadCoOpMode(){
        SceneManager.LoadScene(2); //2 is the Co-Op mode scene
    }
}
