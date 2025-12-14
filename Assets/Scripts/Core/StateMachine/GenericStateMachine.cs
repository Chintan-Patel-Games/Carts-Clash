using System;
using System.Collections.Generic;

namespace CartClash.Core.StateMachine
{
    public class GenericStateMachine<TOwner>
    {
        protected TOwner Owner;
        protected IState<TOwner> currentState;
        protected Dictionary<Enum, IState<TOwner>> States = new();

        public GenericStateMachine(TOwner Owner) => this.Owner = Owner;

        protected void SetOwner()
        {
            foreach (IState<TOwner> state in States.Values)
                state.Owner = Owner;
        }

        public void Update() => currentState?.UpdateState();

        private void ChangeState(IState<TOwner> newState)
        {
            currentState?.OnExitState();
            currentState = newState;
            currentState?.OnEnterState();
        }

        public void ChangeState(Enum newState) => ChangeState(States[newState]);
    }
}