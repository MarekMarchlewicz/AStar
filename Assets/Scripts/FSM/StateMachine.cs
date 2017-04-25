using UnityEngine;
using System.Collections.Generic;

namespace FSM
{
    /// <summary>
    /// State base class.
    /// </summary>
    public abstract class State
    {
        public State(StateMachine stateMachine)
        {
            fsm = stateMachine;
        }

        protected Character character;
        protected StateMachine fsm;

        public virtual void Enter() {}
        public virtual void Execute() {}
        public virtual void FixedExecute() {}
        public virtual void Exit() {}

        public void SetCharacter(Character myCharacter)
        {
            character = myCharacter;
        }
    }

    /// <summary>
    /// Basic implementation of FSM
    /// </summary>
    public class StateMachine : MonoBehaviour
    {
        private Character character;

        private State currentState = null;

        public State GetCurrentState()
        {
            return currentState;
        }

        public void Initialize(Character myCharacter)
        {
            character = myCharacter;
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Execute();
            }
        }

        private void FixedUpdate()
        {
            if (currentState != null)
            {
                currentState.FixedExecute();
            }
        }

        public void ChangeState(State newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;

            if (currentState != null)
            {
                currentState.SetCharacter(character);

                currentState.Enter();
            }
        }
    }
}
