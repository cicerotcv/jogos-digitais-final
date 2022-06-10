using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    public GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart() {
        gm._gameState = GameManager.GameState.PLAY;
        SceneManager.LoadScene(1);
    }

    public void Menu() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        gm.QuitGame();
    }


}
