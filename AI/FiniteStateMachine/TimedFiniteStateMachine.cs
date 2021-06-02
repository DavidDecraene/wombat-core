using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class TimedFiniteStateMachine<Data> where Data : System.Enum
    {
        private readonly Dictionary<Data, TimedFiniteState<Data>> dictionary = new Dictionary<Data, TimedFiniteState<Data>>();
        private TimedFiniteState<Data> currentState;
        private TimedFiniteState<Data> defaultState;
        public event OnFiniteStateEntry<Data> OnFiniteStateEntry;
        public event OnFiniteStateExit<Data> OnFiniteStateExit;
        private bool started = false;
        public bool enabled = true;

        public Data State { get => currentState != null ? currentState.state : default; }

        public TimedFiniteStateMachine()
        {
        }

        public TimedFiniteStateMachine<Data> AddDefaultState(TimedFiniteState<Data> state)
        {
            this.defaultState = state;
            this.currentState = state;
            dictionary.Add(state.state, state);
            return this;
        }

        public TimedFiniteStateMachine<Data> AddState(TimedFiniteState<Data> state)
        {
            dictionary.Add(state.state, state);
            return this;
        }

        public TimedFiniteState<Data> GetState()
        {
            return this.currentState;
        }

        private void ChangeCurrentState(TimedFiniteState<Data> state)
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

        public void Reset()
        {
            SetState(defaultState.state);
        }

        public void ResetState(Data state)
        {
            if (state == null) return;
            if (dictionary.TryGetValue(state, out TimedFiniteState<Data> result))
            {
                ChangeCurrentState(result);
            }
            else if (this.defaultState != null)
            {
                ChangeCurrentState(defaultState);
            }

        }

        public void SetState(Data state)
        {
            if (state == null) return;
            if (currentState != null && currentState.state.Equals(state)) return;
            if (dictionary.TryGetValue(state, out TimedFiniteState<Data> result))
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
            started = true;
            if (currentState != null) this.currentState.Enter();
        }

        public void Update(float delta)
        {
            if (!enabled) return;
            if (!started) Start();
            if (currentState != null) SetState(currentState.Update(delta));
        }
    }
}
