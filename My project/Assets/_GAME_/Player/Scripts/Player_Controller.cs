using UnityEngine;

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
    public PlayerHealthData healthData;

    private Vector2 _moveDir = Vector2.zero;
    private Directions _facingDirection = Directions.RIGHT;

    private readonly int _animMoveRight = Animator.StringToHash("Anim_Player_Move_Right");
    private readonly int _animIdleRight = Animator.StringToHash("Anim_Player_Idle_Right");

    private void Update()
    {
        GatherInput();
        CalculateFacingDirection();
        UpdateAnimation();
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
    }

    private void MovementUpdate()
    {
        _rb.linearVelocity = _moveDir.normalized * _moveSpeed * Time.fixedDeltaTime;
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
            healthData.currentHealth -= 1;
        }
    }
}