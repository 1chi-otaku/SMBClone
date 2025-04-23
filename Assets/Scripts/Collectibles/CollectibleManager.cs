using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    private int totalCollected = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
        Debug.Log("Collected: " + totalCollected);
        // Здесь можно добавить обновление UI, если оно уже есть
    }

    public int GetCollectedCount()
    {
        return totalCollected;
    }
}
