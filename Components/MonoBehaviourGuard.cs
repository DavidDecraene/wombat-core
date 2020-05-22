using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public enum MonoBehaviourGuardLevel
    {
        LOCAL, PARENT, CHILD

    }

    public class MonoBehavioursGuard<T> where T : Component
    {
        private bool loaded = false;
        private T[] value;
        private MonoBehaviourGuardLevel level = MonoBehaviourGuardLevel.LOCAL;
        private MonoBehaviour parent;
        private System.Action<T[]> onLoad;

        public T[] Value { get => Load(); }

        public MonoBehavioursGuard(MonoBehaviour parent)
        {
            this.parent = parent;
        }

        public MonoBehavioursGuard<T> OnLoad(System.Action<T[]> onLoad)
        {
            this.onLoad = onLoad;
            return this;
        }

        public MonoBehavioursGuard<T> SearchInChild()
        {
            this.level = MonoBehaviourGuardLevel.CHILD;
            return this;
        }

        public MonoBehavioursGuard<T> SearchInParent()
        {
            this.level = MonoBehaviourGuardLevel.PARENT;
            return this;
        }

        public bool HasValue()
        {
            return !IsNull();
        }

        public bool IsNull()
        {
            return Load() is null;
        }

        public void Action(System.Action<T[]> fn)
        {
            Load();
            if (value is null) return;
            fn(value);
        }


        private T[] Load()
        {
            if (loaded) return value;
            switch (level)
            {
                case MonoBehaviourGuardLevel.LOCAL:
                    value = parent.GetComponents<T>();
                    break;
                case MonoBehaviourGuardLevel.PARENT:
                    value = parent.GetComponentsInParent<T>();
                    break;
                case MonoBehaviourGuardLevel.CHILD:
                    value = parent.GetComponentsInChildren<T>();
                    break;
            }
            if (value != null && value.Length == 0) value = null;
            if (value != null) onLoad?.Invoke(value);
            loaded = true;
            return value;
        }
    }

    public class MonoBehaviourGuard<T> where T : Component
    {
        private bool loaded = false;
        private T value;
        private MonoBehaviourGuardLevel level = MonoBehaviourGuardLevel.LOCAL;
        private MonoBehaviour parent;
        private System.Action<T> onLoad;

        public T Value { get => Load();  }

        public MonoBehaviourGuard(MonoBehaviour parent) {
            this.parent = parent;
        }

        public MonoBehaviourGuard<T> OnLoad(System.Action<T> onLoad)
        {
            this.onLoad = onLoad;
            return this;
        }

        public MonoBehaviourGuard<T> SearchInChild()
        {
            this.level = MonoBehaviourGuardLevel.CHILD;
            return this;
        }

        public MonoBehaviourGuard<T> SearchInParent()
        {
            this.level = MonoBehaviourGuardLevel.PARENT;
            return this;
        }

        public bool HasValue()
        {
            return !IsNull();
        }

        public bool IsNull()
        {
            return Load() is null;
        }

        public void Action(System.Action<T>  fn)
        {
            Load();
            if (value == null) return;
            fn(value);
        }


        private T Load()
        {
            if (loaded) return value;
            switch (level)
            {
                case MonoBehaviourGuardLevel.LOCAL:
                    parent.TryGetComponent<T>(out value);
                    break;
                case MonoBehaviourGuardLevel.PARENT:
                    value = parent.GetComponentInParent<T>();
                    break;
                case MonoBehaviourGuardLevel.CHILD:
                    value = parent.GetComponentInChildren<T>();
                    break;
            }
            if (value != null) onLoad?.Invoke(value);
            loaded = true;
            return value;
        }
    }
}
