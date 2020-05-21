using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    [System.Serializable]
    public class KeySaveData<T> where T: System.Enum
    {
        public T type;
        public KeyCode code;
        public KeyCode altCode;
        public string codeLabel;
        public string altCodeLabel;
    }
}
