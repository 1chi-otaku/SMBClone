using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event System.Action<int> OnCollectiblePicked;

    [SerializeField] private int value = 1;
    [SerializeField] private AudioClip pickupSound;

    private AudioSource audioSource;
    private bool isCollected = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {
            isCollected = true;
            OnCollectiblePicked?.Invoke(value);

            // Отключаем визуал и физику
            foreach (var renderer in GetComponentsInChildren<Renderer>())
                renderer.enabled = false;

            foreach (var collider in GetComponents<Collider>())
                collider.enabled = false;

            // Проигрываем звук
            if (pickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
                Destroy(gameObject, pickupSound.length);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
