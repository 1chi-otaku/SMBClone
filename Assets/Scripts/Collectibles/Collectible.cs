using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Событие, которое будет вызываться при подборе
    public static event System.Action<int> OnCollectiblePicked;

    [SerializeField] private int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCollectiblePicked?.Invoke(value);
            Destroy(gameObject);
        }
    }
}
