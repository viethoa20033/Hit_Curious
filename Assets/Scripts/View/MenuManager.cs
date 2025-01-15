using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void ButtonPlaying()
    {
        GameManager.Instance.UpdateGameState(GameState.Playing);
    }

    public void ButtonChooseLevel()
    {
        GameManager.Instance.UpdateGameState(GameState.ChooseLevel);
    }

    public void ButtonSetting()
    {
        GameManager.Instance.UpdateGameState(GameState.Setting);
    }
    
}
