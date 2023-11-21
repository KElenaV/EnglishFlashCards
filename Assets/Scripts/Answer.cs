using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public bool IsCorrect = false;
    public Game Game;

    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        Game.ButtonClicked += OnButtonClicked;
    }

    private void OnDisable()
    {
        Game.ButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }

    public void ShowAnswer()
    {
        if (IsCorrect)
            Game.Correct();
        else
            Game.Wrong();
    }
}
