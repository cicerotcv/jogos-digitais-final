using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // private GameManager gm;

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }

    // public void Start() {
    //     gm = GameManager.GetInstance();
    // }

    // public void Update() {
    //     gm._gameState = GameManager.GameState.MENU;
    //     Cursor.lockState = CursorLockMode.None;
    // }
}