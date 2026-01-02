using UnityEngine;
using UnityEngine.U2D;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Shaker shakeOnPlatform;
    [SerializeField] private ModifyMaterialAsteroid modifyerMaterial;
    [SerializeField] private Collider2D colliderAsteroid;
    [SerializeField] private Collider2D colliderPlatform;
    [SerializeField] private bool transformIntoPlatform = true;
    [SerializeField] private bool randomRotation = true;
    public LineAsteroid lineAsteroid;
    [SerializeField] private RandomPitch platformAudio;

    private Vector2 _directionToGo = Vector2.zero;
    private bool _canMove = true;
    private Transform _parent;
    private LineAsteroid _lineAsteroid;

    private void Awake()
    {
        // Set rotation
        if(randomRotation)
            transform.Rotate(Vector3.forward * Random.Range(0f,360f));

        // set colliders
        SetColliders(false);
    }

    private void FixedUpdate()
    {
        if(_canMove)
            Move();
    }

    public void SetDirection(Vector2 dir)
    {
        _directionToGo = dir;
        if (!randomRotation)
        {
            transform.right = dir;
        }
    }

    public void SetParent(Transform parent)
    {
        _parent = parent;
    } 

    public void SetLine(LineAsteroid line)
    {
        _lineAsteroid = line;
    }

    private void Move()
    {
        transform.Translate(_directionToGo * speed * Time.fixedDeltaTime, Space.World);
    }

    private void DestroyLineAndAsteroid()
    {
        if(_lineAsteroid != null)
            Destroy(_lineAsteroid.gameObject);
        Destroy(gameObject, 2);
    }

    private void TransformAsteroidToPlatform()
    {
        if (_lineAsteroid != null)
            Destroy(_lineAsteroid.gameObject);

        platformAudio.PlaySound();
        _canMove = false;
        SetColliders(true);
        transform.SetParent(_parent);
        gameObject.layer = LayerMask.NameToLayer("Platform");

        CameraShake.Instance.Shake(shakeOnPlatform);
        modifyerMaterial.PlayTransition();

        this.enabled = false;
    }

    private void SetColliders(bool value)
    {
        colliderPlatform.enabled = value;
        colliderAsteroid.enabled = !value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.enabled && transformIntoPlatform && collision.CompareTag("Platform"))
        {
            TransformAsteroidToPlatform();
        }

        if (collision.CompareTag("Death")){
            DestroyLineAndAsteroid();
        }
    }
}
