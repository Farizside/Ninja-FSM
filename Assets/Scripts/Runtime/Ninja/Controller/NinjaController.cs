using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NinjaInputReader))]
public class NinjaController : MonoBehaviour
{
    #region Inspector

    [Header("States")]
    [SerializeField] private List<NinjaState> states = new();
    [SerializeField] private StateKey initialState = StateKey.Idle;

    [Header("Stats")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public int maxHealth = 100;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Debug")]
    public int debugDamageAmount = 10;

    #endregion

    #region Runtime Data

    [HideInInspector] public float MoveInput;
    [HideInInspector] public bool JumpRequested;
    [HideInInspector] public bool AttackRequested;
    [HideInInspector] public bool IsGrounded;
    [HideInInspector] public bool IsAlive = true;
    [HideInInspector] public float StateTimer;

    public int CurrentHealth { get; private set; }

    #endregion

    #region Components

    public Rigidbody2D Rb { get; private set; }
    public Animator Animator { get; private set; }

    private NinjaStateMachine _fsm;
    private NinjaInputReader _input;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        _input = GetComponent<NinjaInputReader>();

        CurrentHealth = maxHealth;

        _fsm = new NinjaStateMachine(this);
        _fsm.RegisterAll(states);

        if (!_fsm.HasState(initialState))
        {
            Debug.LogError($"[NinjaController] Initial state '{initialState}' not found in list.");
            return;
        }

        _fsm.ChangeState(initialState);

        _input.OnMoveInput += v => MoveInput = v;
        _input.OnJumpPressed += () => JumpRequested = true;
        _input.OnAttackPressed += () => AttackRequested = true;
        _input.OnTakeDamagePressed += () => TakeDamage(debugDamageAmount);
    }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        _fsm.Update();

        JumpRequested = false;
        AttackRequested = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    #endregion

    #region Public API

    public void ChangeState(StateKey key) => _fsm.ChangeState(key);

    public void TakeDamage(int amount)
    {
        if (!IsAlive) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        if (CurrentHealth == 0)
        {
            IsAlive = false;
            ChangeState(StateKey.Die);
        }
        else
        {
            ChangeState(StateKey.Hurt);
        }
    }
    
    public void FlipTowardsMovement()
    {
        if (Mathf.Approximately(MoveInput, 0f)) return;
        Vector3 scale = transform.localScale;
        scale.x = MoveInput > 0 ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
    
    #endregion
}