using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Game elements")]
    [SerializeField] private TextMeshProUGUI _gameScoreText;

    [Header("Game over panel")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highscoreText;

    [Header("Menu elements")]
    [SerializeField] private TextMeshProUGUI _menuHighscoreText;



    private void OnEnable()
    {
        GameManager.OnScore += ChangeScore;
        GameManager.OnGameOver += GameOver;
        MenuManager.OnUpdateHighscore += ChangeHighscore;
    }

    private void OnDisable()
    {
        GameManager.OnScore -= ChangeScore;
        GameManager.OnGameOver -= GameOver;
        MenuManager.OnUpdateHighscore -= ChangeHighscore;
    }


    private void ChangeScore(int score) =>
        _gameScoreText.text = score.ToString();

    private void ChangeHighscore(int score) =>
        _menuHighscoreText.text = score.ToString();

    private void GameOver(bool isGameOver, int score, int highScore)
    {
        _scoreText.text = score.ToString();
        _highscoreText.text = highScore.ToString();
        _gameOverPanel.SetActive(isGameOver);
    }
}
