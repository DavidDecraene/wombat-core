using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class Graph<Type>
    {
        private Dictionary<Vector2Int, GraphNode<Type>> nodes = new Dictionary<Vector2Int, GraphNode<Type>>();
        private List<GraphEdge<Type>> edges = new List<GraphEdge<Type>>();
        private Dictionary<Vector2Int, List<Vector2Int>> outEdges = new Dictionary<Vector2Int, List<Vector2Int>>();
        private Dictionary<Vector2Int, List<Vector2Int>> inEdges = new Dictionary<Vector2Int, List<Vector2Int>>();

        public GraphNode<Type> AddNode(Vector2Int position, Type data, float weight)
        {
            if (nodes.TryGetValue(position, out GraphNode<Type> result))
            {
                if (weight < result.weight)
                {
                    result.weight = weight;
                }
                return null;
            }

            GraphNode<Type> node = new GraphNode<Type>(data, weight);
            nodes.Add(position, node);
            return node;
        }

        public bool TryNode(Vector2Int position, out GraphNode<Type> result)
        {
            return nodes.TryGetValue(position, out result);
        }

        public IEnumerable<GraphNode<Type>> OutgoingNodes(Vector2Int source)
        {
            if (outEdges.TryGetValue(source, out List<Vector2Int> targets))
            {
                foreach (Vector2Int target in targets)
                {
                    GraphNode<Type> targetNode = GetNode(target);
                    if (targetNode != null) yield return targetNode;
                }
            }
        }

        public GraphNode<Type> GetNode(Vector2Int position)
        {
            GraphNode<Type> result = null;
            nodes.TryGetValue(position, out result);
            return result;
        }

        public GraphEdge<Type> AddEdge(Vector2Int source, Vector2Int target, float weight)
        {
            if (source == target) return null;
            GraphEdge<Type> edge = new GraphEdge<Type>();
            edge.source = source;
            edge.target = target;
            edge.weight = weight;
            bool added = false;
            if (!outEdges.TryGetValue(source, out List<Vector2Int>  outboundNodes))
            {
                outboundNodes = new List<Vector2Int>();
                outEdges.Add(source, outboundNodes);
            }
            if (!outboundNodes.Contains(target))
            {
                outboundNodes.Add(target);
                edges.Add(edge);
                added = true;
            }
            if (!inEdges.TryGetValue(target, out List<Vector2Int>  incomingNodes))
            {
                incomingNodes = new List<Vector2Int>();
                inEdges.Add(target, incomingNodes);
            }
            if (!incomingNodes.Contains(source))
            {
                incomingNodes.Add(source);
            }
            return added ? edge : null;
        }
    }
}
