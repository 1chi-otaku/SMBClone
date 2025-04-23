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

    // Вращение камеры по горизонтали (правый стик)
    [SerializeField] private float cameraRotateSpeed = 100f;

    private float horizontalInput;
    private float verticalInput;

    private float camYaw = 0f;   // Вращение камеры вокруг Y

    void Update()
    {
        // Ввод движения (левый стик)
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Ввод вращения камеры по горизонтали (правый стик)
        float camHorizontal = Input.GetAxis("RightStickHorizontal");
        // float camVertical = Input.GetAxis("RightStickVertical"); // НЕ используем вертикальный ввод с правого стика

        // Обновляем угол поворота камеры только по горизонтали
        camYaw += camHorizontal * cameraRotateSpeed * Time.deltaTime;

        // Применяем вращение камеры (без вертикального наклона)
        transform.rotation = Quaternion.Euler(0f, camYaw, 0f);

        // Визуальный наклон камеры (типа tilt), только по движению игрока (левый стик)
        cameraTransform.localRotation = Quaternion.Lerp(
            cameraTransform.localRotation,
            Quaternion.Euler(-verticalInput * maxTiltAngleVertical, 0f, -horizontalInput * maxTiltAngleHorizontal),
            tiltSpeed * Time.deltaTime);

        FollowPlayer();
    }

    void FixedUpdate()
    {
        // Рассчитываем движение игрока относительно направления камеры (с учётом yaw)
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
