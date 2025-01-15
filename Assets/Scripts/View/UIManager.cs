using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    public GameObject lockButton;

    [Header("Choose level")] public GameObject chooseLevel;
    public CanvasGroup bg;
    public RectTransform head;
    public RectTransform levelButtonRec;
    public Button[] levelButtons;

    [Header("Game Play")] 
    public RectTransform gamePlay;
    public RectTransform gameSetting;
    public CanvasGroup blackPanel;

    [Header("Text")] 
    public Text levelText;
    public Text completeText;
    public string stringComplete;

    [Header("Button Anim Click")] 
    public Button[] buttons;

    [Header("Change Music")] public bool isMusic;
    public Image[] imageMusics;
    public Sprite[] spriteMusics;
    private void Start()
    {
        GameManager.OnGameStateChanged += UpdateGameState;

        LevelManager.OnLevelChanged += UpdateLevel;
        
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i;
            levelButtons[i].onClick.AddListener(() => ButtonLevelClick(index));
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(()=> ButtonAnimClick(index));
        }
        
        //UI Start game
        head.anchoredPosition = new Vector2(0, Screen.height);
        head.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.OutBack);

        StartCoroutine(LevelButtonAnimation());

        isMusic = PlayerPrefs.GetInt("isMusic", 1) == 1;
        if (isMusic)
        {
            AudioListener.volume = 1;
            foreach (var image in imageMusics)
            {
                image.sprite = spriteMusics[1];
            }
        }
        else
        {
            AudioListener.volume = 0;
            foreach (var image in imageMusics)
            {
                image.sprite = spriteMusics[0];
            }
            
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= UpdateGameState;
        
        LevelManager.OnLevelChanged -= UpdateLevel;
    }

    void UpdateLevel(int level)
    {
        levelText.text = "LEVEL " + level;
    }
    void UpdateGameState(GameState state)
    {
        switch (state)
        {
            case GameState.ChooseLevel:
                HandleChooseLevel();
                break;
            case GameState.Playing:
                HandlePlaying();
                break;
            case GameState.Setting:
                HandleGameSetting();
                break;
            case GameState.GameWin:
                HandleGameWin();
                break;
        }
    }

    void HandleChooseLevel()
    {
        lockButton.SetActive(true);
        
        Time.timeScale = 1;
        gameSetting.DOAnchorPos(new Vector2(0, Screen.height), 1f).SetEase(Ease.InBack).OnComplete(() =>
        {
            lockButton.SetActive(false);
            
            gameSetting.gameObject.SetActive(false);
            gamePlay.gameObject.SetActive(false);
            
            chooseLevel.SetActive(true);

            bg.alpha = 0;
            bg.DOFade(1, 1);
            
            head.gameObject.SetActive(true);
            head.anchoredPosition = new Vector2(0, Screen.height);
            head.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.OutBack);
            
            levelButtonRec.gameObject.SetActive(true);
            levelButtonRec.anchoredPosition = new Vector2(0, -Screen.height);
            levelButtonRec.DOAnchorPos(new Vector2(0, -100), 1f).SetEase(Ease.OutBack);

            StartCoroutine(LevelButtonAnimation());

        });
    }

    void HandlePlaying()
    {
        lockButton.SetActive(true);
        
        if (chooseLevel.activeInHierarchy)
        {
            
            bg.alpha = 1;
            bg.DOFade(0, .75f);
            
            head.DOAnchorPos(new Vector2(0, Screen.height), .75f).SetEase(Ease.InBack);
            
            levelButtonRec.DOAnchorPos(new Vector2(0, -Screen.height), .75f).SetEase(Ease.InBack).OnComplete(() =>
            {
                lockButton.SetActive(false);
                
                chooseLevel.SetActive(false);

                gamePlay.gameObject.SetActive(true);
                gamePlay.anchoredPosition = new Vector2(0, Screen.height);
                gamePlay.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.OutBack);
            });
        }

        if (gameSetting.gameObject.activeInHierarchy)
        {
            gameSetting.DOAnchorPos(new Vector2(0, Screen.height), .75f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
            {
                lockButton.SetActive(false);
                
                Time.timeScale = 1;
                gameSetting.gameObject.SetActive(false);
            });
        }
    }

    void HandleGameSetting()
    {
        lockButton.SetActive(true);
        
        Time.timeScale = 0;
        gameSetting.gameObject.SetActive(true);
        gameSetting.anchoredPosition = new Vector2(0, Screen.height);
        gameSetting.DOAnchorPos(Vector2.zero, .75f).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() =>
        {
            lockButton.SetActive(false);
        });
    }

    void HandleGameWin()
    {
        lockButton.SetActive(true);
        
        StopAllCoroutines();

        StartCoroutine(RevealTextComplete());
        StartCoroutine(GameWinAnimation());
    }

    void ButtonLevelClick(int index)
    {
        LevelManager.Instance.SetLevel(index + 1);
    }

    void ButtonAnimClick(int index)
    {
        buttons[index].transform.DOScale(Vector3.one * .8f, .1f).SetEase(Ease.InOutQuad).SetUpdate(true).OnComplete(() =>
        {
            buttons[index].transform.DOScale(Vector3.one, .1f).SetEase(Ease.InOutQuad).SetUpdate(true);
        });
    }

    public void ChangeMusic()
    {
        isMusic = !isMusic;

        if (isMusic)
        {
            AudioListener.volume = 1;
            foreach (var image in imageMusics)
            {
                image.sprite = spriteMusics[1];
            }
        }
        else
        {
            AudioListener.volume = 0;
            foreach (var image in imageMusics)
            {
                image.sprite = spriteMusics[0];
            }
            
        }
        PlayerPrefs.SetInt("isMusic", isMusic ? 1 : 0);
        PlayerPrefs.Save();
    }

    IEnumerator GameWinAnimation()
    {
        yield return new WaitForSeconds(3f);
        blackPanel.gameObject.SetActive(true);

        //in black
        blackPanel.alpha = 0;
        blackPanel.DOFade(1, .5f);
        
        //-------//
        yield return new WaitForSeconds(1f);
        
        //out black
        blackPanel.DOFade(0, 1);
        yield return new WaitForSeconds(1f);
        
        //-------//
        blackPanel.gameObject.SetActive(false);
        
        
        lockButton.SetActive(false);
    }
    
    IEnumerator RevealTextComplete()
    {
        completeText.gameObject.SetActive(true);

        
        for (int i = 0; i <= stringComplete.Length; i++)
        {
           string currentText = stringComplete.Substring(0, i);
            completeText.text = currentText;
            yield return new WaitForSeconds(.05f);
        }

        yield return new WaitForSeconds(1.5f);
        completeText.gameObject.SetActive(false);
    }

    IEnumerator LevelButtonAnimation()
    {
        foreach (var level in levelButtons)
        {
            level.transform.localScale = Vector3.zero;
        }

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.2f);
        }
    }
}
