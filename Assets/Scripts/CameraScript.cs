using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxTiltAngle = 30f;
    [SerializeField] private float tiltSpeed = 5f;
    [SerializeField] private float followDistance = 10f;
    [SerializeField] private float followHeight = 5f;
    [SerializeField] private float followSmoothness = 5f;


    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        TiltCamera();
    }

    void FixedUpdate()
    {
        Vector3 forward = Vector3.Cross(cameraTransform.right, Vector3.up).normalized;
        Vector3 right = cameraTransform.right;
        Vector3 moveDir = (forward * verticalInput + right * horizontalInput).normalized;

        player.Move(moveDir);
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

    void TiltCamera()
    {
        float targetX = -verticalInput * maxTiltAngle;
        float targetZ = -horizontalInput * maxTiltAngle;

        Quaternion targetRotation = Quaternion.Euler(targetX, transform.eulerAngles.y, targetZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = player.transform.position
                               - transform.forward * followDistance
                               + Vector3.up * followHeight;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSmoothness * Time.deltaTime);
    }
}
