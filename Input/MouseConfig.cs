using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class MouseConfig
    {
        private bool consumed = false;
        public int button;

        public MouseConfig(int button)
        {
            this.button = button;
        }


        public void Consume()
        {
            consumed = true;
        }

        public void Consume(bool state)
        {
            consumed = state;
        }

        public bool IsConsumed()
        {
            return consumed;
        }


        public bool IsPressed()
        {
            return IsPressed(false);
        }

        public bool IsPressed(bool consume)
        {
            if (consumed)
            {
                return false;
            }
            if (Input.GetMouseButtonDown(button))
            {
                if (consume) consumed = true;
                return true;
            }
            return false;
        }
    }
}
