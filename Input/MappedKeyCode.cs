using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{

    public class MappedKeyCode
    {
        public string label;
        public KeyCode code;

        public MappedKeyCode(KeyCode code)
        {
            this.code = code;
            string s = code.ToString();
            label = code.ToString();
        }

        public MappedKeyCode(KeyCode code, char c)
        {
            this.code = code;
            string s = code.ToString();
            if (s == ((int)code).ToString())
            {
                label = char.ToUpper(c) + "";
            }
            else
            {
                label = s;
            }
        }

        public bool Equals(MappedKeyCode other)
        {
            if (other == null) return false;
            return this.code == other.code;
        }

    }
}
