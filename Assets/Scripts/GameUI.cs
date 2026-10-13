using System;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public ScoreText ScoreTextPlayer1, ScoreTextPlayer2;
    public GameObject menuObject;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI playModeButtonText;
    public TextMeshProUGUI volumeValueText;

    public Action onStartGame;

    public void Start()
    {
        AdjustPlayModeButtonText();
    }

    public void updateScores(int scorePlayer1, int scorePlayer2)
    {
        ScoreTextPlayer1.setScore(scorePlayer1);
        ScoreTextPlayer2.setScore(scorePlayer2);
    }

    public void HighlightScore(int id)
    {
        if (id == 1)
        {
            ScoreTextPlayer1.Highlight();

        }
        else
        {
            ScoreTextPlayer2.Highlight();
        }
    }

    public void OnStartGameButtonClicked()
    {
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winnerId)
    {
        menuObject.SetActive(true);
        winText.text = $"Player {winnerId} wins!";
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        volumeValueText.text = $"{Mathf.RoundToInt (value * 100)} %";
    }

    public void OnSwitchPlayModeButtonClicked()
    {
        GameManager.instance.SwitchPlayMode();
        AdjustPlayModeButtonText();
    }

    private void AdjustPlayModeButtonText()
    {
        switch (GameManager.instance.playMode)
        {
            case GameManager.PlayMode.PlayerVsPlayer:
                playModeButtonText.text = "2 Players";
                break;


            case GameManager.PlayMode.PlayerVsAi:
                playModeButtonText.text = "Player Vs AI";
                break;
        }


    }
}
