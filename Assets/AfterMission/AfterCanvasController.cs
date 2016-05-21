using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AfterCanvasController : MonoBehaviour
{
    public static int currentScore = 0;
    public InputField playerName;
    public Button saveScoreButton;

    public GameObject field;
    public GameObject button;

    public Text scoreText;

    void Start()
    {
        if (SaveLoad.scoresSaves.Count < 3 || currentScore <= SaveLoad.scoresSaves[2].scorePoints)
        {
            scoreText.text = "Your score:\n" + currentScore;
            field.SetActive(true);
            button.SetActive(true);
        }
        else
        {
            scoreText.text = "Your score:\n" + currentScore + "\nEnter your name";
            field.SetActive(false);
            button.SetActive(false);
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SaveScore()
    {
        SaveLoad.AddScore(new Score { scorePoints = currentScore, playerName = playerName.text });
        button.SetActive(false);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene("City1");
    }
}
