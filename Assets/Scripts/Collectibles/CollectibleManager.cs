using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int totalCollected = 0;
    private int totalOnMap = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Новый способ найти все Collectible
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

        Debug.Log($"Collected: {totalCollected}/{totalOnMap}");

        // Обновляем UI
        FindObjectOfType<UIManager>()?.AddBanana();
    }

    public int GetCollectedCount()
    {
        return totalCollected;
    }

    public int TotalCount => totalOnMap;
}
