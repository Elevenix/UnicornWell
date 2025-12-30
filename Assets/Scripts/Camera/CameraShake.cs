using UnityEngine;
using System.Collections;
using static UnityEngine.Rendering.DebugUI;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private CameraAngled cameraAngled;

    public static CameraShake Instance;

    private Vector3 currentOffset;
    private Vector3 velocity;

    private bool _canShake = true;
    private float _forceScreenShake;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        CheckForceScreenShake();
    }

    public void CheckForceScreenShake()
    {
        _forceScreenShake = PlayerPrefs.GetFloat("ScreenShake");
    }

    public void CheckValueAngledCamera()
    {
        cameraAngled.CheckValueAngledCamera();
    }

    public void SetCameraAngle(Vector2 dir)
    {
        cameraAngled.SetCameraAngle(dir);
    }

    public void SetCanShake(bool value) 
    {
        _canShake = value;
    }

    public void Shake(Shaker shaker)
    {
        Shake(shaker.duration, shaker.magnitude, shaker.smoothTime, shaker.direction);
    }

    public void Shake(float duration, float magnitude, float smoothTime, Vector2 direction)
    {
        if (!_canShake)
            return;
        StopAllCoroutines();
        StartCoroutine(ShakeCoroutine(duration, magnitude * _forceScreenShake, smoothTime, direction));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude, float smoothTime, Vector2 direction)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Atténuation progressive
            float damper = 1f - (elapsed / duration);

            Vector3 targetOffset = new Vector3(
                Random.Range(-1f, 1f) * magnitude * damper * direction.x,
                Random.Range(-1f, 1f) * magnitude * damper * direction.y,
                0f
            );

            // Smooth vers la nouvelle position
            currentOffset = Vector3.SmoothDamp(
                currentOffset,
                targetOffset,
                ref velocity,
                smoothTime
            );

            mainCamera.localPosition = currentOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Retour smooth à la position initiale
        while (currentOffset.magnitude > 0.001f)
        {
            currentOffset = Vector3.SmoothDamp(
                currentOffset,
                Vector3.zero,
                ref velocity,
                smoothTime
            );

            mainCamera.localPosition = currentOffset;
            yield return null;
        }

        mainCamera.localPosition = Vector3.zero;
        currentOffset = Vector3.zero;
    }
}
