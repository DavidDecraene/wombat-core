using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wombat
{
    public class DataGraph<Type>
    {
        private readonly Dictionary<string, Type> nodes = new Dictionary<string, Type>();
        private readonly HashMultiMap<string, DataGraphEdge<Type>> outgoing = new HashMultiMap<string, DataGraphEdge<Type>>();
        private readonly HashMultiMap<string, DataGraphEdge<Type>> incoming = new HashMultiMap<string, DataGraphEdge<Type>>();
        private readonly System.Func<Type, string> transform;
        public DataGraph(System.Func<Type, string> transform)
        {
            this.transform = transform;
        }

        public Dictionary<string, Type>.ValueCollection GetNodes()
        {
            return nodes.Values;
        }

        public int NodeCount()
        {
            return nodes.Count;
        }


        public void Clear()
        {
            nodes.Clear();
            outgoing.Clear();
            incoming.Clear();
        }

        public bool AddNode(Type t)
        {
            return AddNode(t, false);
        }

        public bool AddNode(Type t, bool replace)
        {
            if (t == null) return false;
            string key = transform(t);
            if (key == null) return false;
            if (!replace && nodes.ContainsKey(key)) return false;
            nodes[key] = t;
            return true;
        }

        public string GetKey(Type t)
        {
            if (t == null) return null;
            return transform(t);
        }

        public Type GetNode(string key)
        {
            nodes.TryGetValue(key, out Type value);
            return value;
        }

        public void AddEdge(string source, string link, string target)
        {
            if (source == null || target == null || link == null) return;
            DataGraphEdge<Type> edge = new DataGraphEdge<Type>(source, link, target);
            outgoing.Add(source, edge);
            incoming.Add(target, edge);
        }

        public void AddEdge(Type source, string link, Type target)
        {
            if (source == null || target == null || link == null) return;
            string sSource = transform(source);
            string sTarget = transform(target);
            AddEdge(sSource, link, sTarget);
        }

        public bool HasEdgesTo(Type t)
        {
            if (t == null) return false;
            string sSource = transform(t);
            return incoming.Count(sSource) > 0;
        }

        public bool HasEdgesFrom(Type t)
        {
            if (t == null) return false;
            string sSource = transform(t);
            return outgoing.Count(sSource) > 0;
        }



        public IEnumerable<DataGraphEdge<Type>> EdgesTo(Type t)
        {
            if (t == null) yield break;
            foreach (DataGraphEdge<Type> edge in incoming.Values(transform(t))) {
                yield return edge;
            }

        }



        public IEnumerable<DataGraphEdge<Type>> EdgesFrom(Type t)
        {
            if (t == null) yield break;
            foreach(DataGraphEdge<Type> edge in outgoing.Values(transform(t))) {
                yield return edge;
            }

        }

        public IEnumerable<Type> TargetNodes(Type source)
        {
            if (source == null) yield break;
            string sSource = transform(source);
            foreach (DataGraphEdge<Type> edge in outgoing.Values(sSource))
            {
                Type target = GetNode(edge.Target);
                if (target != null)
                    yield return target;
            }

        }

        public IEnumerable<Type> SourceNodes(Type target)
        {
            if (target == null) yield break;
            string sSource = transform(target);
            foreach (DataGraphEdge<Type> edge in incoming.Values(sSource))
            {
                Type source = GetNode(edge.Source);
                if (source != null)
                    yield return source;
            }

        }

    }
}
