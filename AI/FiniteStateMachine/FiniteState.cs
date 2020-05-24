using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public abstract class FiniteState<Data> where Data : System.Enum
    {
        public readonly Data state;

        public FiniteState(Data state)
        {
            this.state = state;
        }

        public virtual bool AcceptEnter()
        {
            return true;
        }

        public virtual bool AcceptExit()
        {
            return true;
        }


        public virtual void Enter()
        {

        }


        public virtual void Exit()
        {

        }

        public virtual Data Update()
        {
            return state;
        }

        public virtual Data FixedUpdate()
        {
            return state;

        }
    }
}
