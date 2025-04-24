using UnityEngine;
using UnityEngine.SceneManagement;

public class FallZoneTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip fallSound;
    [SerializeField] private float delayBeforeRestart = 4f;

    private bool hasFallen = false;

    private void OnTriggerEnter(Collider other)
    {

        if (hasFallen) return;

        if (other.CompareTag("Player"))
        {
            hasFallen = true;

 
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

  
            UIManager.Instance?.StopTimer();

  
            UIManager.Instance?.ShowFallText();

            if (fallSound != null)
                AudioSource.PlayClipAtPoint(fallSound, transform.position, 1.0f);


            GameData.Score = 0;

            Invoke(nameof(ReloadScene), delayBeforeRestart);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
