using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class LayerFlag
    {
        private readonly BitFlag flag = new BitFlag();
        public int Value { get => flag.Value; }

        public void Of(LayerMask mask)
        {
            this.flag.Of(mask.value);
        }

        public void Of(GameObject layer)
        {
            if (layer == null)
            {
                this.flag.Clear();
                return;
            }
            this.Of(layer.layer);
        }

        public void Of(int layer)
        {
            this.flag.Of(1 << layer);
        }

        public bool Has(int layer)
        {
            return flag.Has(1 << layer);
        }

        public bool Contains(GameObject layer)
        {
            if (layer == null) return false;
            return Has(layer.layer);
        }
    }
}
