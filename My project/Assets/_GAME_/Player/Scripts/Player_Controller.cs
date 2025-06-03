using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
[SelectionBase]

public class TopDownPlayerMovement : MonoBehaviour
{
    private enum Directions { UP, DOWN, LEFT, RIGHT }
    public float _moveSpeed = 50f;

    public BoxCollider areaBounds; // defines the player's playable area

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject bulletPrefab;
    private float bulletMoveSpeed = 10;
    private float bulletRange = 13;
    private float restTime = 0.5f;

    public PlayerHealthData healthData;

    private Vector2 _moveDir = Vector2.zero;
    private Directions _facingDirection = Directions.RIGHT;
    private bool takingDamage = false;
    private readonly int _animMoveRight = Animator.StringToHash("Anim_Player_Move_Right");
    private readonly int _animIdleRight = Animator.StringToHash("Anim_Player_Idle_Right");
    private bool isShooting = false;

    private bool _dashing = false;
    private bool _dashed = false;


    void Start()
    {
        Debug.Log("Reset health");
        healthData.ResetHealth();
    }
    private void Update()
    {
        GatherInput();
        CalculateFacingDirection();
        UpdateAnimation();
        if (Input.GetMouseButton(0) && !isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    private void FixedUpdate()
    {
        MovementUpdate();
        BindPosition();
    }

    private void GatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DashOnce());
        }
    }

    private void MovementUpdate()
    {
        if (_dashing && !_dashed)
        {
            _rb.linearVelocity = _moveDir.normalized * 80f;
            _dashed = true;
        }
        else
        {
            _rb.linearVelocity = _moveDir.normalized * _moveSpeed * Time.fixedDeltaTime;
        }
    }

    // only allows player to move within playable area
    private void BindPosition()
    {
        Vector3 boundPosition = _rb.position;
        Bounds bounds = areaBounds.bounds;
        boundPosition.x = Mathf.Clamp(boundPosition.x, bounds.min.x, bounds.max.x);
        boundPosition.y = Mathf.Clamp(boundPosition.y, bounds.min.y, bounds.max.y);
        _rb.position = boundPosition;
    }

    private void CalculateFacingDirection()
    {
        // moving L/R
        if (_moveDir.x != 0)
        {
            if (_moveDir.x > 0) // move right
            {
                _facingDirection = Directions.RIGHT;
            }
            else if (_moveDir.x < 0) // move left
            {
                _facingDirection = Directions.LEFT;
            }
        }
    }

    private void UpdateAnimation()
    {
        // player flipping direction
        if (_facingDirection == Directions.LEFT)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_facingDirection == Directions.RIGHT)
        {
            _spriteRenderer.flipX = false;
        }

        // player moving
        if (_moveDir.SqrMagnitude() != 0)
        {
            _animator.CrossFade(_animMoveRight, 0);
        }
        // player returning to idle
        else
        {
            _animator.CrossFade(_animIdleRight, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Bullet") == true)
        {
            Debug.Log("Hit bullet");
            if (takingDamage == false)
            {
                Debug.Log("Taking dammage");
                StartCoroutine(TakeDamage());
            }
        }
    }


    private IEnumerator TakeDamage()
    {
        takingDamage = true;
        healthData.TakeDamage(1);
        Debug.Log(healthData.currentHealth);
        yield return new WaitForSeconds(1);
        takingDamage = false;
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        // Vector3 target = Input.mousePosition;
        // target.z = 0.0f;
        // target = Camera.main.ScreenToWorldPoint(target);
        // target = target - transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 target = (Vector2)((mousePos - transform.position));
        target.Normalize();

        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.right = target;
        if (newBullet.TryGetComponent(out Bullet bullet))
        {
            bullet.UpdateMoveSpeed(bulletMoveSpeed);
            bullet.UpdateProjectileRange(bulletRange);
            Debug.Log("shot at " + target);
        }
        yield return new WaitForSeconds(restTime);
        isShooting = false;

    }


    private IEnumerator DashOnce()
    {
        if (_dashing) yield break;
        _dashing = true;
        Debug.Log("dashing");
        yield return new WaitForSeconds(0.4f);
        _dashing = false;
        _dashed = false;
    }
}