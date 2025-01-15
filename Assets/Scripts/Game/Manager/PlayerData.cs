using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerData : MonoBehaviour
{
    public GameObject lockLevel;
    public Text levelText;

    public Transform[] buttonLevels;
    private void Start()
    {
        LoadData();
        
        GameManager.OnGameStateChanged += UpdateGameState;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= UpdateGameState;
    }

    void UpdateGameState(GameState state)
    {
        if (state == GameState.GameWin)
        {
            SaveData(LevelManager.Instance.level);
        }
    }

    public void LoadData()
    {
        int level = PlayerPrefs.GetInt("level", 0);

        foreach (Transform buttonLevel in buttonLevels)
        {
            buttonLevel.GetComponent<Button>().interactable = false;
            foreach (Transform child in buttonLevel)
            {
                Destroy(child.gameObject);
            }
        }
        
        
        for (int i = 0; i < buttonLevels.Length; i++)
        {
            if (i <= level)
            {
                buttonLevels[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                Instantiate(lockLevel, buttonLevels[i]);
            }
            
            Text txtLevel = Instantiate(levelText, buttonLevels[i]);
            txtLevel.text = (i + 1).ToString();

        }
    }

    public void SaveData(int level)
    {
        int levelMax = PlayerPrefs.GetInt("level", 0);

        if (level > levelMax)
        {
            PlayerPrefs.SetInt("level", level);
            PlayerPrefs.Save();
            
            LoadData();
            
            Debug.Log("Save level " + level );
        }
    }
}
