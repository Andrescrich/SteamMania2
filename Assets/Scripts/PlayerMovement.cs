using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
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
    public float recoilForce = 1000f;
    public int bullets;
    public bool canDoubleJump;

    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int Speed = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        Physics2D.IgnoreLayerCollision(8, 9);
        _anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sR = GetComponentInChildren<SpriteRenderer>();
        _pS = GetComponent<PlayerStates>();
        gameObject.name = "Player";
    }

    private void Start()
    {
        width = _sR.bounds.extents.x;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 groundPos = transform.position + -transform.up * width;
        Vector2 vec2 = -transform.up * 0.2f;
        _pS.isGrounded = Physics2D.Linecast(groundPos, groundPos + vec2, isGround);
        Debug.DrawLine(groundPos, groundPos + vec2, Color.red);

        if (!_pS.recoiling)
            dirMove = Input.GetAxisRaw("Horizontal");
        else
            dirMove = 0;

        if (!_pS.isJumping && dirMove != 0)
        {
            _pS.isWalking = true;
        }
        else if (_pS.isJumping || dirMove == 0)
        {
            _pS.isWalking = false;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && bullets > 0)
        {
            _pS.isShoothing = true;
            _anim.SetTrigger(Shoot);
            //  AudioManager.Play(uiSound);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            bullets = 3;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _pS.isJumping == false && _pS.isGrounded)
        {
            _pS.isJumping = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump && _pS.isDoubleJumping == false && !_pS.isGrounded)
        {
            _pS.isDoubleJumping = true;
        }

        _anim.SetFloat(Speed, Mathf.Abs(dirMove));
    }

    private void FixedUpdate()
    {
        Move(dirMove, velocity);

        if(_pS.isJumping)
              Jump();
        if (_pS.isDoubleJumping)
            DoubleJump();
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
        if (!_pS.isGrounded)
        {
            _pS.isJumping = false;
            canDoubleJump = true;
            return;
        }

        var vector = new Vector2(_rb.velocity.x, jumpForce * Time.fixedDeltaTime);
        _rb.velocity = vector;
        Debug.Log("Jump");
    }

    private void DoubleJump()
    {
        if (!canDoubleJump)
        {
            _pS.isDoubleJumping = false;
            return;
        }

        var vector = new Vector2(_rb.velocity.x, jumpForce * Time.fixedDeltaTime);
        _rb.velocity = vector;
        canDoubleJump = false;
        Debug.Log("DoubleJump");
    }

    public void ShootRecoil()
    {
        if (!_pS.recoiling) 
            return;
        Vector2 vector;
        _rb.AddForce(_sR.flipX ? new Vector2(-recoilForce, 0f): new Vector2(recoilForce, 0f));
        _pS.recoiling = false;
    }
}