using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{

    public void OnPlayButton()
    {
        GameData.Score = 0;
        SceneManager.LoadScene("1-1 Simple");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
