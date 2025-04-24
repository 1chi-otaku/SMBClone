using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI bananaText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject fallText;

    [Header("Timer Settings")]
    [SerializeField] private int startTime = 60;
    private float timeRemaining;
    private bool isTimerRunning = true;

    private int maxBananas;
    private int collectedBananas;
    private Rigidbody playerRb;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        timeRemaining = startTime;
        levelText.text = SceneManager.GetActiveScene().name;

        var collectibleManager = FindFirstObjectByType<CollectibleManager>();
        if (collectibleManager != null)
        {
            maxBananas = collectibleManager.TotalCount;
        }

        var player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
        }

        UpdateBananaText();
        UpdateScore(GameData.Score);
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

    public void UpdateBananaText()
    {
        int collected = CollectibleManager.Instance.GetCollectedCount();
        int total = CollectibleManager.Instance.TotalCount;
        bananaText.text = $"{collected.ToString("D3")}/{total.ToString("D3")}";
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString("D5");
    }

    public void ShowFallText()
    {
        if (fallText != null)
            fallText.SetActive(true);
    }

    private void UpdateTimer()
    {
        if (!isTimerRunning) return;

        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Max(0f, timeRemaining);
        timeText.text = Mathf.CeilToInt(timeRemaining).ToString("D3");

        if (timeRemaining <= 0) {

            GameData.Score = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   
        
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    private void UpdateSpeedText()
    {
        if (playerRb == null) return;

        float speed = playerRb.linearVelocity.magnitude * 3.6f; 
        speedText.text = $"{speed:F0} km/h";
    }
}
