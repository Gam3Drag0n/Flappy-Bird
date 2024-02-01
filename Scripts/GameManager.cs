using System;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private int _score;
    private int _highscore;
    private bool _isGameOver;


    public static Action<bool> OnPause;
    public static Action<int> OnScore;
    public static Action OnStartGame;
    public static Action<bool, int, int> OnGameOver;



    private void OnEnable()
    {
        PlayerController.OnAddScore += ChangeScore;
        PlayerController.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        PlayerController.OnAddScore -= ChangeScore;
        PlayerController.OnGameOver -= GameOver;
    }


    private void Awake() =>
        _highscore = PlayerPrefs.GetInt("Highscore");


    private void ChangeScore()
    {
        _score++;
        if(_score >= _highscore )
        {
            _highscore = _score;
            PlayerPrefs.SetInt("Highscore", _highscore);
        }

        OnScore?.Invoke(_score);
    }

    public void Pause(bool isPause)
    {
        if (_isGameOver) return;
        OnPause?.Invoke(isPause);
    }


    public void GameOver(bool isGameOver)
    {
        _isGameOver = isGameOver;
        OnGameOver?.Invoke(isGameOver, _score, _highscore);
        OnPause?.Invoke(isGameOver);
    }

    public void StartGame() =>
        OnStartGame?.Invoke();
}
