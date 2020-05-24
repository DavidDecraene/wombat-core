using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public delegate void OnFiniteStateEntry<Data>(Data status) where Data : System.Enum;
    public delegate void OnFiniteStateExit<Data>(Data status) where Data : System.Enum;

    public class FiniteStateMachine<Data> where Data : System.Enum
    {
        private readonly Dictionary<Data, FiniteState<Data>> dictionary = new Dictionary<Data, FiniteState<Data>>();
        private FiniteState<Data> currentState;
        private FiniteState<Data> defaultState;
        public event OnFiniteStateEntry<Data> OnFiniteStateEntry;
        public event OnFiniteStateExit<Data> OnFiniteStateExit;

        public FiniteStateMachine()
        {
        }

        public FiniteStateMachine<Data> AddDefaultState(FiniteState<Data> state)
        {
            this.defaultState = state;
            this.currentState = state;
            dictionary.Add(state.state, state);
            return this;
        }

        public FiniteStateMachine<Data> AddState(FiniteState<Data> state)
        {
            dictionary.Add(state.state, state);
            return this;
        }

        public FiniteState<Data> GetState()
        {
            return this.currentState;
        }

        private void ChangeCurrentState(FiniteState<Data> state)
        {
            // Debug.Log("Changing states to " + state);
            if (!state.AcceptEnter())
            {
                return;
            }
            if (currentState != null)
            {
                if (!currentState.AcceptExit()) return;
                currentState.Exit();
                OnFiniteStateExit?.Invoke(currentState.state);
            }
            this.currentState = state;
            state.Enter();
            OnFiniteStateEntry?.Invoke(state.state);
        }

        public void SetState(Data state)
        {
            if (state == null) return;
            if (currentState != null && currentState.state.Equals(state)) return;
            if (dictionary.TryGetValue(state, out FiniteState<Data> result))
            {
                ChangeCurrentState(result);
            }
            else if (this.defaultState != null)
            {
                ChangeCurrentState(defaultState);
            }
        }

        public void Start()
        {
            if(currentState != null) this.currentState.Enter();
        }

        public void Update()
        {
            if (currentState != null) SetState(currentState.Update());
        }

        public void FixedUpdate()
        {
            if (currentState != null) SetState(currentState.FixedUpdate());

        }
    }
}
