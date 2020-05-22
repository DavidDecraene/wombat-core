using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class GraphEdge<Type>
    {
        public float weight = 1;
        public Vector2Int source;
        public Vector2Int target;
    }
}
