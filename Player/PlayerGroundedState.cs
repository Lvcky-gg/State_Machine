using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) :
    base(_player, _stateMachine, _animBoolName)
    {

    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);
        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.counterState);
        if (Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword())
            stateMachine.ChangeState(player.aimSwordState);
    }
    private bool HasNoSword()
    {
        if (!player.sword)
            return true;
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
    public override void Exit()
    {
        base.Exit();
    }
}