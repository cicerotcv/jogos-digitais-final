using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    void Update() {
        
        if (gm._gameState == GameManager.GameState.PLAY) {
            gm._runningTime = (int) (gm._startTime - Time.timeSinceLevelLoad);
            timeText.text = "Time: " + gm._runningTime.ToString();

            if (gm._runningTime == 0) {
                gm._gameState = GameManager.GameState.ENDGAME;
            }
        }
    }
}
