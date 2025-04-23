using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] private GameObject goalLine;
    [SerializeField] private GameObject goalText;
    [SerializeField] private string nextSceneName = "NextScene"; // Название следующей сцены

    private bool goalReached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (goalReached) return;

        if (other.CompareTag("Player"))
        {
            goalReached = true;

            // Сохраняем очки
            GameData.Score = CollectibleManager.Instance.GetScore();

            // Прячем Goal Line
            if (goalLine != null) goalLine.SetActive(false);

            // Показываем надпись "Goal!"
            if (goalText != null) goalText.gameObject.SetActive(true);

            // Загружаем следующую сцену через 3 секунды
            Invoke(nameof(LoadNextScene), 3f);
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
