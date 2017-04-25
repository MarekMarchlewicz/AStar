using UnityEngine;
using FSM;

public class IdleState : State 
{
    public IdleState(StateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        character.PlayMovementAnimation(MovementAnimation.Idle);
    }
}
