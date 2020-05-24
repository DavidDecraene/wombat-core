using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class GraphNode<Type>
    {
        public float weight = 1;
        public readonly Type data;
        public GraphNode(Type data, float weight)
        {
            this.data = data;
            this.weight = weight;
        }
    }
}
