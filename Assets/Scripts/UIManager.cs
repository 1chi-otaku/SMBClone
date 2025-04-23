using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI bananaText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI speedText;

    [Header("Timer Settings")]
    [SerializeField] private int startTime = 60;
    private float timeRemaining;
    private bool isTimerRunning = true;

    private int maxBananas;
    private int collectedBananas;
    private Rigidbody playerRb;

    void Start()
    {
        timeRemaining = startTime;
        levelText.text = SceneManager.GetActiveScene().name;

        var collectibleManager = FindObjectOfType<CollectibleManager>();
        if (collectibleManager != null)
        {
            maxBananas = collectibleManager.TotalCount;
        }

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
        }

        UpdateBananaText();
    }

    void Update()
    {
        UpdateTimer();
        UpdateSpeedText();
    }

    public void AddBanana()
    {
        collectedBananas++;
        UpdateBananaText();
    }

    private void UpdateBananaText()
    {
        bananaText.text = $"{collectedBananas.ToString("D3")}/{maxBananas.ToString("D3")}";
    }

    private void UpdateTimer()
    {
        if (!isTimerRunning) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0f, timeRemaining);
        timeText.text = Mathf.CeilToInt(timeRemaining).ToString("D3");

        // if (timeRemaining <= 0) { // TODO: что-то сделать }
    }

    private void UpdateSpeedText()
    {
        if (playerRb == null) return;

        float speed = playerRb.linearVelocity.magnitude * 3.6f; // м/с → км/ч
        speedText.text = $"{speed:F0} km/h";
    }
}
