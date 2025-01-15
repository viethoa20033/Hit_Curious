using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SetColor[] setColors;

    private float timeCheckToWin;
    private void Update()
    {
        setColors = FindObjectsOfType<SetColor>();
        
        if (GameManager.Instance.isPlaying && setColors.Length > 0)
        {
            if (CheckingCorrectColor())
            {
                timeCheckToWin += Time.deltaTime;

                if (timeCheckToWin >= .5f)
                {
                    GameManager.Instance.UpdateGameState(GameState.GameWin);
                    timeCheckToWin = 0;
                }
            }
            else
            {
                timeCheckToWin = 0;
            }
        }
    }

    bool CheckingCorrectColor()
    {
        foreach (var setColor in setColors)
        {
            if (!setColor.isCorrect)
            {
                return false;
            }
        }

        return true;
    }
}
