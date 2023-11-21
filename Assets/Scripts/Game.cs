using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private List<Card> _cards;
    [SerializeField] private Button[] _answers;
    [SerializeField] private TMP_Text _question;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _scoreText;

    private int _score;
    private int _currentQuestion;
    private int _totalQuestions;

    private void Start()
    {
        _totalQuestions = _cards.Count;
        _gameOverPanel.SetActive(false);
        _gamePanel.SetActive(true);
        TryGenerateQuestion();
    }

    public void Correct()
    {
        _score++;
        GoToNextQuestion();
    }

    public void Wrong()
    {
        GoToNextQuestion();
    }

    private void GoToNextQuestion()
    {
        _cards.RemoveAt(_currentQuestion);
        TryGenerateQuestion();
    }

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
        _gamePanel.SetActive(false);
        _scoreText.text = $"{_score} / {_totalQuestions}";
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TryGenerateQuestion()
    {
        if (_cards.Count > 0)
        {
            _currentQuestion = Random.Range(0, _cards.Count);
            _question.text = _cards[_currentQuestion].EnglishWord;
            SetAnswers();
        }
        else
        {
            GameOver();
        }
    }

    private void SetAnswers()
    {
        _answers[0].GetComponent<Answer>().IsCorrect = true;

        
        for (int i = 0; i < _answers.Length; i++)
        {
            _answers[i].transform.GetChild(0).GetComponent<TMP_Text>().text = _cards[_currentQuestion].Translations[i];
        }
    }
}
