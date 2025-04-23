using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int totalCollected = 0;
    private int totalOnMap = 0;
    private int score = 0;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        totalOnMap = Object.FindObjectsByType<Collectible>(FindObjectsSortMode.None).Length;
    }

    private void OnEnable()
    {
        Collectible.OnCollectiblePicked += AddCollectible;
    }

    private void OnDisable()
    {
        Collectible.OnCollectiblePicked -= AddCollectible;
    }

    private void AddCollectible(int amount)
    {
        totalCollected += amount;
        score += 100 * amount;

        Debug.Log($"Collected: {totalCollected}/{totalOnMap} | Score: {score}");

        UIManager.Instance?.AddBanana();
        UIManager.Instance?.UpdateScore(score);
    }

    public int GetCollectedCount() => totalCollected;
    public int TotalCount => totalOnMap;
    public int GetScore() => score;

}
