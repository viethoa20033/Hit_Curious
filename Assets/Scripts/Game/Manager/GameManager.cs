using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public static UnityAction<GameState> OnGameStateChanged;

    public GameState gameState;

    public bool isPlaying;
    public bool isWin;

    public ParticleSystem[] pars;
    protected override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case GameState.ChooseLevel:
                HandleChooseLevel();
                break;
            case GameState.Playing:
                isPlaying = true;
                break;
            case GameState.Setting:
                isPlaying = false;
                break;
            case GameState.GameWin:
                HandleGameWin();
                break;
        }
        OnGameStateChanged?.Invoke(gameState);
    }

    void HandleChooseLevel()
    {
    }
    void HandleGameWin()
    {
        isWin = true;
        isPlaying = false;

        StopAllCoroutines();
        StartCoroutine(WaitingTimeNextLevel());
    }

    IEnumerator WaitingTimeNextLevel()
    {
        yield return new WaitForSeconds(.5f);
        foreach (var par in pars)
        {
            par.Play();
        }
        yield return new WaitForSeconds(3);
        
        
        UpdateGameState(GameState.Playing);
    }
}

public enum GameState
{
    ChooseLevel,
    Playing,
    Setting,
    GameWin
}
