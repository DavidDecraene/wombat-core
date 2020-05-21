using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wombat
{
    public class KeyMapper : MonoBehaviour
    {
        private bool capturing = false;
        public Text text;
        private MappedKeyCode code;
        [HideInInspector]
        public bool changed;
        private static readonly string c1 = ".";
        private static readonly string c2 = "..";
        private static readonly string c3 = "...";
        private int tick = 0;
        private float tickUpdate = 0.5f;
        private float tickTime = 0;


        public void Toggle()
        {

            capturing = true;

            this.text.text = c1;
            tick++;
        }

        private void SetKeyCode(MappedKeyCode code)
        {
            if (this.code == null || !this.code.Equals(code))
            {
                changed = true;
                this.text.text = code.label;
            }
            capturing = false;
            this.code = code;
        }

        private KeyCode GetKeyCode(char character)
        {
            // Get from cache if it was taken before to prevent unnecessary enum parse
            KeyCode code;
            // Cast to it's integer value
            int alphaValue = character;
            code = (KeyCode)System.Enum.Parse(typeof(KeyCode), alphaValue.ToString());
            return code;
        }

        private void UpdatePointer()
        {
            tickTime += Time.deltaTime;
            if (tickTime >= tickUpdate)
            {
                tickTime = 0;
                tick++;
                if (tick >= 3) tick = 0;
                switch (tick)
                {
                    case 0:
                        this.text.text = c1; break;
                    case 1:
                        this.text.text = c2; break;
                    case 2:
                        this.text.text = c3; break;
                }
            }

        }

        public void Update()
        {
            if (capturing)
            {
                UpdatePointer();
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    this.SetKeyCode(new MappedKeyCode(KeyCode.LeftArrow));
                    return;
                }
                else
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    this.SetKeyCode(new MappedKeyCode(KeyCode.RightArrow));
                    return;
                }
                else
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    this.SetKeyCode(new MappedKeyCode(KeyCode.UpArrow));
                    return;
                }
                else
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    this.SetKeyCode(new MappedKeyCode(KeyCode.DownArrow));
                    return;
                }
                string s = Input.inputString;
                if (s != null)
                {
                    foreach (char c in s)
                    {
                        if (c == '\b')
                        {// DELETE
                            continue;
                        }
                        if ((c == '\n') || (c == '\r'))
                        {
                            this.SetKeyCode(new MappedKeyCode(KeyCode.Return));
                            break;
                        }
                        KeyCode code = GetKeyCode(c);
                        // Debug.Log(char.ToUpper(c) + " " + code.ToString() + (int)code);
                        SetKeyCode(new MappedKeyCode(code, c));
                        break;

                    }
                }
            }
        }
    }
}
