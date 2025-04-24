using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        if (scoreText != null)
        {
            int finalScore = GameData.Score;
            scoreText.text = $"Your Score: {finalScore.ToString("D5")}";
        }
    }

    public void OnAgainButton()
    {
        GameData.Score = 0;
        SceneManager.LoadScene("1-1 Simple");
    }

    public void OnMenuButton()
    {
        GameData.Score = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
