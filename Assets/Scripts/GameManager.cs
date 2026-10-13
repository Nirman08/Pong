using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameUI gameUI;
    public GameAudio gameAudio;
    public Shake screenShake;
    public BallMovement ballMovement;
    public int scorePlayer1, scorePlayer2;
    
    
    public System.Action onReset; // action is located in system itself

    public int maxScore = 4;

    public PlayMode playMode;

    public enum PlayMode
    {
        PlayerVsPlayer,
        PlayerVsAi
    }

    public void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            gameUI.onStartGame += OnStartGame;
        }
    }

    public void onScoreZoneReached(int id)
    {

        if(id == 1)
        {
            scorePlayer1++;
        }

        if(id == 2)
        {
            scorePlayer2++;
        }

        gameUI.updateScores(scorePlayer1,  scorePlayer2);
        gameUI.HighlightScore(id);
        CheckWin();
    }

    public void CheckWin()
    {
        int winnerId = scorePlayer1 == maxScore? 1 : scorePlayer2==maxScore ? 2 : 0;
        if (winnerId != 0)
        {
            gameUI.OnGameEnds(winnerId);
            gameAudio.PlayWinSound();
        }
        else
        {
            onReset?.Invoke();
            gameAudio.PlayScoreSound();

        }
    }

    private void OnStartGame()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
        gameUI.updateScores(scorePlayer1, scorePlayer2 );
    }


    public void SwitchPlayMode()
    {
        switch (playMode)
        {
            case PlayMode.PlayerVsPlayer:
                playMode = PlayMode.PlayerVsAi;
                break;

            case PlayMode.PlayerVsAi:
                playMode = PlayMode.PlayerVsPlayer;
                break;

        }
    }

    public bool IsPlayer2Ai()
    {
        return playMode == PlayMode.PlayerVsAi;
    }
   
}
