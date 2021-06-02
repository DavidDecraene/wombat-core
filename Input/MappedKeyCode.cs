using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{

    public class MappedKeyCode
    {
        public static string UpArrow = "↑";
        public static string DownArrow = "↓";
        public static string LeftArrow = "←";
        public static string RightArrow = "→";
        public static string Escape = "Esc";
        public string label;
        public KeyCode code;

        public MappedKeyCode(KeyCode code)
        {
            Update(code);
        }

        public MappedKeyCode(KeyCode code, char c)
        {
            Update(code, c);
        }

        public MappedKeyCode Update(KeyCode code, char c)
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
            return this;
        }

        public MappedKeyCode Update(KeyCode code)
        {

            this.code = code;
            if (code == KeyCode.UpArrow)
            {
                label = UpArrow;
            } else if (code == KeyCode.DownArrow)
            {
                label = DownArrow;
            }
            else if (code == KeyCode.LeftArrow)
            {
                label = LeftArrow;
            }
            else if (code == KeyCode.RightArrow)
            {
                label = RightArrow;
            }
            else if (code == KeyCode.Escape)
            {
                label = Escape;
            }
            else
            {
                label = code.ToString();
            }
            return this;
        }

        public bool Equals(MappedKeyCode other)
        {
            if (other == null) return false;
            return this.code == other.code;
        }

    }
}
