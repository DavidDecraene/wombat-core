using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class KeyConfig<T> where T : System.Enum
    {
        public MappedKeyCode code;
        public MappedKeyCode altCode;
        public readonly T type;
        private bool consumed = false;
        private readonly IKeyBindings parent;
        public string description;
        public string label;
        public bool locked = false;

        public string Label { get => code.ToString(); }

        public event System.Action OnCodeChange;

        public KeyConfig(IKeyBindings parent, T type, KeyCode code)
        {
            this.parent = parent;
            this.type = type;
            this.code = new MappedKeyCode(code);
            this.altCode = new MappedKeyCode(KeyCode.None);
        }

        public KeyConfig(IKeyBindings parent, T type, KeyCode code, KeyCode altCode)
        {
            this.parent = parent;
            this.type = type;
            this.code = new MappedKeyCode(code);
            this.altCode = new MappedKeyCode(altCode);
        }

        public KeyConfig<T> Lock()
        {
            this.locked = true;
            return this;
        }

        public KeyConfig<T> Describe(string label, string description)
        {
            this.label = label;
            this.description = description;
            return this;
        }

        public void Restore(KeySaveData<T> data)
        {
            if (data == null) return;
            if (data.code != KeyCode.None)
            {
                code = new MappedKeyCode(data.code);
                if (data.codeLabel != null)
                {
                    code.label = data.codeLabel;
                }
            }
            if (data.altCode != KeyCode.None)
            {
                altCode = new MappedKeyCode(data.altCode);
                if (data.altCodeLabel != null)
                {
                    altCode.label = data.altCodeLabel;
                }
            }

        }

        public KeySaveData<T> Save()
        {
            KeySaveData<T> data = new KeySaveData<T>();
            data.type = type;
            if (code != null)
            {
                data.code = code.code;
                data.codeLabel = code.label;
            }
            if (altCode != null)
            {
                data.altCode = altCode.code;
                data.altCodeLabel = altCode.label;
            }
            return data;
        }

        public void SetAltCode(MappedKeyCode code)
        {
            if (code == null || code.Equals(this.altCode)) return;
            this.altCode = code;
            this.OnCodeChange?.Invoke();
        }

        public void SetCode(MappedKeyCode code)
        {
            if (code == null || code.Equals(this.code)) return;
            this.code = code;
            this.OnCodeChange?.Invoke();
        }

        public void Consume()
        {
            if (parent.Paused()) return;
            consumed = true;
        }

        public void Consume(bool state)
        {
            if (parent.Paused()) return;
            consumed = state;
        }

        public bool IsConsumed()
        {
            if (parent.Paused()) return false;
            return consumed;
        }


        public bool IsPressed()
        {
            return IsPressed(false);
        }

        public bool IsHeldDown()
        {
            if (parent.Paused()) return false;
            if (consumed)
            {
                return false;
            }
            if (Input.GetKey(code.code))
            {
                return true;
            }
            if (altCode != null && altCode.code != KeyCode.None && Input.GetKey(altCode.code))
            {
                return true;
            }
            return false;
        }

        public bool IsPressed(bool consume)
        {
            if (parent.Paused()) return false;
            if (consumed)
            {
                return false;
            }
            if (Input.GetKeyDown(code.code))
            {
                if (consume) consumed = true;
                return true;
            }
            if (altCode != null && altCode.code != KeyCode.None && Input.GetKeyDown(altCode.code))
            {
                if (consume) consumed = true;
                return true;
            }
            return false;
        }

    }
}
