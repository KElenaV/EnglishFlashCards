//using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private AudioClip _wrongSound;
    [SerializeField] private AudioClip _correctSound;

    private int _score;
    private int _currentQuestion;
    private int _totalQuestions;

    public event UnityAction<AudioClip> ButtonClicked;

    private void Start()
    {
        _totalQuestions = _cards.Count;
        _gameOverPanel.SetActive(false);
        _gamePanel.SetActive(true);
        TryGenerateQuestion();
    }

    public void Correct()
    {
        ButtonClicked?.Invoke(_correctSound);
        _score++;
        GoToNextQuestion();
    }

    public void Wrong()
    {
        ButtonClicked?.Invoke(_wrongSound);
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
            _currentQuestion = UnityEngine.Random.Range(0, _cards.Count);
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
        //_answers[0].GetComponent<Answer>().IsCorrect = true;

        //for (int i = 0; i < _answers.Length; i++)
        //{
        //    _answers[i].transform.GetChild(0).GetComponent<TMP_Text>().text = _cards[_currentQuestion].Translations[i];
        //}

        //List<Button> answers = new List<Button>();
        int correctIndex = Random.Range(0, _answers.Length);

        _answers[correctIndex].GetComponent<Answer>().IsCorrect = true;
        _answers[correctIndex].transform.GetChild(0).GetComponent<TMP_Text>().text = _cards[_currentQuestion].CorrectTranslation;


        for (int i = 0, j = 0; i < _answers.Length; i++)
        {
            if (i == correctIndex)
                continue;

            _answers[i].transform.GetChild(0).GetComponent<TMP_Text>().text = _cards[_currentQuestion].Translations[j++];
        }
    }
}
