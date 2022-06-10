using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameMenus : MonoBehaviour {

    public GameObject pauseMenuUI, optionsMenuUI;
    private GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gm._gameState == GameManager.GameState.PLAY) {
                Pause();
            } else if (gm._gameState == GameManager.GameState.PAUSE) {
                Resume();
            }
        }

        if (gm._gameState == GameManager.GameState.ENDGAME) {
            GameOver();
        }
    }

    public void Pause() {
        gm._gameState = GameManager.GameState.PAUSE;
        Time.timeScale = 0f;

        pauseMenuUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume() {
        gm._gameState = GameManager.GameState.PLAY;
        Time.timeScale = 1f;

        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    public void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

    public void Menu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        gm.QuitGame();
    }
}
