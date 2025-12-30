using UnityEngine;

public class CameraAngled : MonoBehaviour
{
    [SerializeField] private float angleMax = 5f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Transform mainCamera;

    private float targetAngle;

    private float _forceAngle;

    private void Start()
    {
        CheckValueAngledCamera();
    }

    public void CheckValueAngledCamera()
    {
        _forceAngle = PlayerPrefs.GetFloat("AngledCamera");
    }

    public void SetCameraAngle(Vector2 dir)
    {
        targetAngle = angleMax * dir.x * _forceAngle;
    }

    private void Update()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        mainCamera.rotation = Quaternion.Lerp(
            mainCamera.rotation,
            targetRotation,
            smoothSpeed * Time.deltaTime
        );
    }
}
