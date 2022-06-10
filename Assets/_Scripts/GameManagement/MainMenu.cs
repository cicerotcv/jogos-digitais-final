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
        gm._gameState = GameManager.GameState.PLAY;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        gm.QuitGame();
    }
}