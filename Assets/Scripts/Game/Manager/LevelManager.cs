using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public static UnityAction<int> OnLevelChanged;
    
    public int level;

    public GameObject[] levelMaps;

    public GameObject[] levelPreviews;
    public Transform previewContent;


    private void Start()
    {
        GameManager.OnGameStateChanged += UpdateGameState;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= UpdateGameState;
    }

    void UpdateGameState(GameState state)
    {
        if (state == GameState.Playing && GameManager.Instance.isWin)
        {
            NextLevel();
            GameManager.Instance.isWin = false;
        }
    }
    public void SetLevel(int _level)
    {
        level = _level;
        OnLevelChanged?.Invoke(level);

        SpawnMap();
    }

    public void RetryLevel()
    {
        if (!GameManager.Instance.isWin)
        {
            SetLevel(level);
            GameManager.Instance.UpdateGameState(GameState.Playing);
        }
    }

    public void NextLevel()
    {
        if (level < 18)
        {
            level++;
            OnLevelChanged?.Invoke(level);

            SpawnMap();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SpawnMap()
    {
        foreach (Transform child in previewContent)
        {
            Destroy(child.gameObject);
        }
        
        GameObject[] _maps = GameObject.FindGameObjectsWithTag("map");

        foreach (var _map in _maps)
        {
            Destroy(_map.gameObject);
        }
        
        GameObject[] _balls = GameObject.FindGameObjectsWithTag("ball");

        foreach (var ball in _balls)
        {
            Destroy(ball.gameObject);
        }

        Instantiate(levelMaps[level - 1]);

        Instantiate(levelPreviews[level - 1], previewContent);
    }
}
