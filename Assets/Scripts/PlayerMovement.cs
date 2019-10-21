using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    private Animator _anim;
    private Rigidbody2D _rb;
    private SpriteRenderer _sR;
    private PlayerStates _pS;
    private float width;
    public bool _canFlip = true;
    private float dirMove;
    [SerializeField] private LayerMask isGround;
    [SerializeField] private float groundRaycast;
    [SerializeField] private float velocity = 350f;
    [SerializeField] private float jumpForce = 500f;

    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
        _anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sR = GetComponentInChildren<SpriteRenderer>();
        _pS = GetComponent<PlayerStates>();

    }

    private void Start()
    {
        width = _sR.bounds.extents.x;
    }

    public Audio uiSound;
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.Play(uiSound, gameObject);
        }
        Vector2 groundPos = transform.position + -transform.up * width;
        Vector2 vec2 = -transform.up * 0.2f;
        _pS.isGrounded = Physics2D.Linecast(groundPos, groundPos + vec2, isGround);
        Debug.DrawLine(groundPos, groundPos + vec2, Color.red);

        if (_pS.isGrounded)
            _pS.isJumping = false;
        
        dirMove = Input.GetAxisRaw("Horizontal");

        if (!_pS.isJumping && dirMove != 0)
        {
            _pS.isWalking = true;
        } else if (_pS.isJumping || dirMove == 0)
        {
            _pS.isWalking = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _pS.isShoothing = true;
            _anim.SetTrigger(Shoot);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_pS.isJumping)
        {
            _pS.isJumping = true;
            Jump();
        }


        _anim.SetFloat(Speed, Mathf.Abs(dirMove));
    }

    private void FixedUpdate()
    {
        Move(dirMove, velocity);
        
        if(_pS.isJumping)
            Jump();
    }

    private void Move(float dirMov, float vel)
    {
        var vector = new Vector2(dirMov * vel * Time.fixedDeltaTime, _rb.velocity.y);
        _rb.velocity = vector;
        
        if (!_canFlip) return;
        if (dirMov > 0)
        {
            _sR.flipX = true;
        }
        else if (dirMov < 0)
        {
            _sR.flipX = false;
        }
    }

    private void Jump()
    {
        if (!_pS.isGrounded) return;
        var vector = new Vector2(_rb.velocity.x, jumpForce * Time.fixedDeltaTime);
        _rb.velocity = vector;
    }
}
