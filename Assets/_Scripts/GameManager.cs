using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

    private static GameManager _instance;
    
    public enum GameState {MENU, PAUSE, PLAY, ENDGAME};
    public GameState _gameState;

    private GameManager() {
        _gameState = GameState.MENU;
    }

    public static GameManager GetInstance() {
        if (_instance == null) {
            _instance = new GameManager();
        }
        return _instance;
    }
}
