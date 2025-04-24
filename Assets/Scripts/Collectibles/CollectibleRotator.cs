using UnityEngine;

public class CollectibleRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; 

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
