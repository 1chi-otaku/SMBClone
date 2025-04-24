using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementSound : MonoBehaviour
{
    [SerializeField] private AudioClip movementClip;
    [SerializeField] private float minSpeedKmh = 1f;
    [SerializeField] private float maxSpeedKmh = 40f;
    [SerializeField] private float maxInterval = 1.0f;  // на низкой скорости — медленно
    [SerializeField] private float minInterval = 0.1f;  // на высокой скорости — часто

    private AudioSource audioSource;
    private Rigidbody rb;
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = movementClip;
        audioSource.volume = 0.3f;
    }

    void Update()
    {
        float speedKmh = rb.linearVelocity.magnitude * 3.6f;

        if (speedKmh < minSpeedKmh)
        {
            timer = 0f;
            return;
        }

        // t увеличивается с ростом скорости
        float t = Mathf.InverseLerp(minSpeedKmh, maxSpeedKmh, speedKmh);
        float interval = Mathf.Lerp(maxInterval, minInterval, t);

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            audioSource.PlayOneShot(movementClip);
        }
    }
}
