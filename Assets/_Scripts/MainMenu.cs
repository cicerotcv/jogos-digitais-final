using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gm._gameState = GameManager.GameState.PLAY;
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