using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2;
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;
        player.anim.SetInteger("ComboCounter", comboCounter);
        #region ChooseAttackDirection
        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = player.facingDir;
        #endregion
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        player.anim.speed = 1.2f;

        stateTimer = .1f;
    }
    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.ZeroVelocity();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);
        player.anim.speed = 1f;
        comboCounter++;
        lastTimeAttacked = Time.time;
    }
}
