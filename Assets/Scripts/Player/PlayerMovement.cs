using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private float sphereRadius = 1.2f;
    [SerializeField] private LayerMask layerMaskGround;

    [Header("Movement")]
    [SerializeField] private float speed = 1;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float jumpAirForce = 1600;
    [SerializeField] private int numberAirJump = 1;

    [SerializeField] private float jumpHoldForce = 30f;
    [SerializeField] private float maxJumpHoldTime = 0.25f;

    [SerializeField] private ParticleSystem doubleJumpParticles;

    [Header("Wall Slide")]
    [SerializeField] private float wallSlideVelocity = -1;
    [SerializeField] private float slideAngle = 25f;

    [Header("Animator")]
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private Animator animatorWing;
    [SerializeField] private Transform turnVisual;

    private Rigidbody2D _rb;
    private bool _isGrounded = false;
    private Vector2 _dirMove = Vector2.zero;
    private int _actualAirJump = 0;
    private float _slopeAngle = 90;

    // Jump hold
    private bool _isJumping = false;
    private float _jumpTimeCounter = 0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RaycastHit2D cast = Physics2D.CircleCast(
            transform.position,
            sphereRadius,
            Vector2.right,
            0,
            layerMaskGround
        );

        if (cast)
        {
            _actualAirJump = 0;
            _isGrounded = true;

            float dot = Vector3.Dot(cast.normal, Vector3.up);
            _slopeAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            animatorWing.SetBool("Ground", true);
        }
        else
        {
            _isGrounded = false;
            _slopeAngle = 0;
            animatorPlayer.SetBool("Fall", false);
            animatorWing.SetBool("Ground", false);
        }
    }

    private void FixedUpdate()
    {
        // Horizontal movement
        Vector2 move = new Vector2(
            _dirMove.x * Time.fixedDeltaTime * speed,
            _rb.linearVelocityY
        );
        _rb.linearVelocity = move;

        // Jump hold (plus on appuie, plus on monte)
        if (_isJumping && _jumpTimeCounter > 0f)
        {
            _rb.AddForce(Vector2.up * jumpHoldForce);
            _jumpTimeCounter -= Time.fixedDeltaTime;
        }

        // Wall slide
        if (_isGrounded && _rb.linearVelocityY < wallSlideVelocity)
        {
            if (_slopeAngle > slideAngle)
            {
                _rb.linearVelocity = new Vector2(
                    _rb.linearVelocityX,
                    wallSlideVelocity
                );

                animatorPlayer.SetBool("Fall", true);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        float initXValue = _dirMove.x;
        _dirMove = value.ReadValue<Vector2>();

        // set camera angle
        if (initXValue != _dirMove.x)
            CameraShake.Instance.SetCameraAngle(_dirMove);

        if (value.phase == InputActionPhase.Started && _dirMove.x != 0)
        {
            turnVisual.localScale = new Vector3(_dirMove.normalized.x * -1, 1, 1);
            animatorPlayer.SetBool("Run", true);
        }

        if(value.phase == InputActionPhase.Canceled)
        {
            animatorPlayer.SetBool("Run", false);
        }
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (_isGrounded)
            {
                animatorPlayer.SetTrigger("Jump");
                StartJump(jumpForce);
            }
            else if (numberAirJump > _actualAirJump)
            {
                _actualAirJump++;
                animatorPlayer.SetTrigger("DoubleJump");
                animatorWing.SetTrigger("Jump");
                doubleJumpParticles.Play();
                StartJump(jumpAirForce);
            }
        }

        if (value.canceled)
        {
            _isJumping = false;

            // Coupe la montée si on relâche tôt
            if (_rb.linearVelocityY > 0f)
            {
                _rb.linearVelocity = new Vector2(
                    _rb.linearVelocityX,
                    _rb.linearVelocityY * 0.5f
                );
            }
        }
    }

    private void StartJump(float force)
    {
        animatorPlayer.SetBool("Fall", false);
        _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 0f);
        _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        _isJumping = true;
        _jumpTimeCounter = maxJumpHoldTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }
}
