using System.Collections;


using UnityEngine;

public class Player : Entity
{
    public GameObject sword { get; private set; }
    [Header("Attack Details")]
    public Vector2[] attackMovement;

    public bool isBusy { get; private set; }
    [Header("Move Info")]
    public float moveSpeed = 8f;
    public float jumpForce;
    public float swordReturnImpact;


    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }
    #region States
    public PlayerStateMachine stateMachine
    { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerCounterAttackState counterState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }

    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJump wallJump { get; private set; }
    public PlayerJumpState jumpState
    { get; private set; }
    public PlayerDashState dashState { get; private set; }

    public PlayerPrimaryAttack primaryAttack { get; private set; }
    #endregion
    public float CounterAttackDuration = .2f;

    public SkillManager skill { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJump(this, stateMachine, "Jump");
        counterState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        primaryAttack = new PlayerPrimaryAttack(this, stateMachine, "Attack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);
    }


    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();

    }
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {

            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }

    public void AssignNewSword(GameObject _sword) => sword = _sword;
    public void ClearSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }


}
