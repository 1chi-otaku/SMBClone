using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float maxTiltAngleVertical = 15f;
    [SerializeField] private float maxTiltAngleHorizontal = 30f;
    [SerializeField] private float tiltSpeed = 5f;
    [SerializeField] private float followDistance = 10f;
    [SerializeField] private float followHeight = 5f;
    [SerializeField] private float followSmoothness = 5f;

    // Camera rotation speed
    [SerializeField] private float cameraRotateSpeed = 100f;

    private float horizontalInput;
    private float verticalInput;

    private float camYaw = 0f;   // Camera spin Y

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // horizontal
        float camHorizontal = Input.GetAxis("RightStickHorizontal");

        camYaw += camHorizontal * cameraRotateSpeed * Time.deltaTime;

        // enable camera rotation 
        transform.rotation = Quaternion.Euler(0f, camYaw, 0f);

        // tilt
        cameraTransform.localRotation = Quaternion.Lerp(
            cameraTransform.localRotation,
            Quaternion.Euler(-verticalInput * maxTiltAngleVertical, 0f, -horizontalInput * maxTiltAngleHorizontal),
            tiltSpeed * Time.deltaTime);

        FollowPlayer();
    }

    void FixedUpdate()
    {
        // Ensure the player moves when the camera faces
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        Vector3 moveDir = (forward * verticalInput + right * horizontalInput).normalized;

        player.Move(moveDir);
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = player.transform.position
                               - cameraTransform.forward * followDistance
                               + Vector3.up * followHeight;

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSmoothness * Time.deltaTime);
    }
}
