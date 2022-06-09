using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {

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
    }

    public void Resume() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gm._gameState = GameManager.GameState.PLAY;
    }

    public void Pause() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gm._gameState = GameManager.GameState.PAUSE;
    }

    public void Menu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
