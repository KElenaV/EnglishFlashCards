using UnityEngine;

public class Answer : MonoBehaviour
{
    public bool IsCorrect = false;
    public Game Game;

    public void ShowAnswer()
    {
        if (IsCorrect)
            Game.Correct();
        else
            Game.Wrong();
    }
}
