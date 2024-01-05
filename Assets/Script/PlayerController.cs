using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Attack")]
    public bool isAttacking;

    [Header("Dash")]
    public bool isDashing;
    [SerializeField] private int dashStamina = 20;
    [SerializeField] private float dashingTime;
    [SerializeField] private float dashForce;

    [Header("Move")]
    [SerializeField] private float moveSpeed;

    [SerializeField] private Vector2 moveDirection;
    [SerializeField] private Vector2 lastDirection;
    [SerializeField] private Vector2 attackDirection;

    private Animator animator;
    private Rigidbody2D rb2d;
    private PlayerStamina playerStamina;
    private PlayerAttack playerAttack;
    private CameraCollider cameraCollider => CameraCollider.instance;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerStamina = GetComponent<PlayerStamina>();
        playerAttack = GetComponent<PlayerAttack>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        UpdateInput();
        UpdateAnimation();

        CheckState();
    }
    void UpdateInput()
    {
        if (isAttacking)
        {
            rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y);
            return;
        }
        Move();
        Dash();
        Attack();
    }
    void CheckState()
    {
        playerStamina.UpdateStaminaState();
        playerStamina.UpdateStaminaBar();
        playerStamina.UpdateStaminaBarState();
    }
    void UpdateAnimation()
    {
        animator.SetFloat("MoveX", moveDirection.x);
        animator.SetFloat("MoveY", moveDirection.y);

        animator.SetFloat("LastMoveX", lastDirection.x);
        animator.SetFloat("LastMoveY", lastDirection.y);

        animator.SetFloat("AttackX", attackDirection.x);
        animator.SetFloat("AttackY", attackDirection.y);

        animator.SetFloat("MoveSpeed", moveDirection.magnitude);
        animator.SetBool("Move", moveDirection != Vector2.zero);
    }
    void Move()
    {
        if (CanMove())
        {
            float pressMoveX = Input.GetAxisRaw("Horizontal");
            float pressMoveY = Input.GetAxisRaw("Vertical");
            if ((pressMoveX == 0 && pressMoveY == 0) && (moveDirection.x != 0 || moveDirection.y != 0))
            {
                SetLastDirection(moveDirection);
            }

            rb2d.velocity = new Vector2(pressMoveX * moveSpeed, pressMoveY * moveSpeed);
            SetMoveDirection(new Vector2(pressMoveX, pressMoveY).normalized);
        }
    }
    void Dash()
    {
        bool pressDash = Input.GetMouseButtonDown(1);
        if (pressDash && CanDash())
        {
            isDashing = true;
            if (moveDirection == Vector2.zero)
            {
                if(lastDirection.x == 0 && lastDirection.y == 0) lastDirection.y = -1;

                SetMoveDirection(new Vector2(lastDirection.x, lastDirection.y));
            }
            rb2d.velocity = moveDirection.normalized * dashForce;

            playerStamina.UpdateStamina(dashStamina);
            StartCoroutine(StopDash());
        }

    }
    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }

    public void Attack()
    {
        bool pressAttack = Input.GetMouseButtonDown(0);
        if (pressAttack && CanAttack())
        {
            if (cameraCollider.CheckTargetDetection() != null)
            {
                Vector3 direction;
                direction = (cameraCollider.GetTargetTransform().position - transform.position).normalized;
                SetAttackDirection(direction);
            }
            else
            {
                if (moveDirection.x == 0 && moveDirection.y == 0)
                {
                    SetAttackDirection(lastDirection);
                }
                else if(moveDirection.x !=0 || moveDirection.y != 0)
                {
                    SetAttackDirection(moveDirection);
                    SetLastDirection(moveDirection);
                }
            }
            ChangeAttackState(true);
            animator.SetTrigger("Attack");
        }
    }
    public Vector3 SetDirectionAttackWithOutTarget()
    {
        Vector3 direction;
        if (lastDirection.x == 0 && lastDirection.y == 0) direction = new Vector2(0, -1);

        else if (moveDirection.x == 0 && moveDirection.y == 0)
            direction = new Vector2(lastDirection.x, lastDirection.y);

        else direction = moveDirection;

        return direction;
    }

    void SetMoveDirection(Vector2 direction) => moveDirection = direction;
    void SetLastDirection(Vector2 direction) => lastDirection = direction;
    void SetAttackDirection(Vector2 direction) => attackDirection = direction;
    bool CanMove() => !isDashing;
    bool CanAttack() => !isAttacking && !isDashing;
    bool CanDash() => playerStamina.currentStamina >= dashStamina && !isDashing && !isAttacking;

    public void ChangeAttackState(bool state)
    {
        isAttacking = state;
    }
}
