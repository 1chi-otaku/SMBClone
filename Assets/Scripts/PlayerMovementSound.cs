using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementSound : MonoBehaviour
{
    [SerializeField] private AudioClip movementClip;
    [SerializeField] private AudioClip fallClip;          // Звук падения
    [SerializeField] private float minSpeedKmh = 1f;
    [SerializeField] private float maxSpeedKmh = 40f;
    [SerializeField] private float maxInterval = 1.0f;
    [SerializeField] private float minInterval = 0.1f;

    [SerializeField] private float fallSoundThreshold = 5f; // Порог силы удара для звука падения

    private AudioSource audioSource;
    private Rigidbody rb;
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.7f;
    }

    void Update()
    {
        float speedKmh = rb.linearVelocity.magnitude * 3.6f;

        if (speedKmh < minSpeedKmh)
        {
            timer = 0f;
            return;
        }

        float t = Mathf.InverseLerp(minSpeedKmh, maxSpeedKmh, speedKmh);
        float interval = Mathf.Lerp(maxInterval, minInterval, t);

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            audioSource.PlayOneShot(movementClip);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем силу удара
        float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime;

        if (impactForce > fallSoundThreshold)
        {
            audioSource.PlayOneShot(fallClip);
        }
    }
}
