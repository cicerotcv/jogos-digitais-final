using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager {

    private static GameManager _instance;
    
    public enum GameState {PLAY, PAUSE, MENU, ENDGAME};
    public GameState _gameState;

    public int _startTime { get; private set; }
    public int _runningTime;

    private GameManager() {
        _gameState = GameState.MENU;
        _startTime = 120;
    }

    public static GameManager GetInstance() {
        if (_instance == null) {
            _instance = new GameManager();
        }
        return _instance;
    }


    // Menu Funcitions
    public void QuitGame() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
