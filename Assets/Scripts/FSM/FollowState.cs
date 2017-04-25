using UnityEngine;
using System.Collections.Generic;
using FSM;

public class FollowState : State 
{
    private Queue<Node> path;

    private Vector3? targetPosition;

    private float moveSpeed;
    private float rotationSpeed;

    private Vector3 startPosition;
    private float startTime;
    private float timeLeft;

    private Quaternion targetRotation;

    private Node currentNode;

    public FollowState(StateMachine stateMachine, Queue<Node> myPath, float speed, float myRotationSpeed) : base(stateMachine)
    {
        path = myPath;
        moveSpeed = speed;
        rotationSpeed = myRotationSpeed;
    }

    public override void Enter()
    {
        character.PlayMovementAnimation(MovementAnimation.Run);
    }

    public override void Exit()
    {
        if (currentNode != null)
        {
            GridManager.Instance.GetVisual(currentNode).Reset();
        }
    }

    public override void Execute() 
    {
        if (targetPosition.HasValue)
        {
            FollowTarget();
            RotateTowardsTarget();
        }
        else
        {
            TryToSetNewTarget();
        }
    }

    private void FollowTarget()
    {
        float interpolation = (Time.time - startTime) / timeLeft;

        if (interpolation < 1f)
        {
            character.Transform.position = Vector3.Lerp(startPosition, targetPosition.Value, interpolation);
        }
        else
        {
            TryToSetNewTarget();
        }
    }

    private void RotateTowardsTarget()
    {
        character.Transform.rotation = Quaternion.Lerp(character.Transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void TryToSetNewTarget()
    {
        if (path != null && path.Count > 0)
        {
            startPosition = character.Transform.position;

            startTime = Time.time;

            SwitchNode();

            targetPosition = GridManager.Instance.GetWorldPosition(currentNode) + Vector3.up / 2;

            timeLeft = (targetPosition.Value - startPosition).magnitude / moveSpeed;

            Vector3 forward = targetPosition.Value - character.Transform.position;

            targetRotation = Quaternion.LookRotation(forward);
        }
        else
        {
            Exit();

            fsm.ChangeState(new IdleState(fsm));
        }
    }

    private void SwitchNode()
    {
        if (currentNode != null)
        {
            GridManager.Instance.GetVisual(currentNode).Reset();
        }

        currentNode = path.Dequeue();

        GridManager.Instance.GetVisual(currentNode).SetTint(Color.blue);
    }
}
