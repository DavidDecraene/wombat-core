using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class DataGraphEdge<Type> : IEquatable<DataGraphEdge<Type>>
    {
        public string Source { get; private set; }
        public string Link { get; private set; }
        public string Target { get; private set; }

        public DataGraphEdge(string source, string link, string target)
        {
            this.Source = source;
            this.Link = link;
            this.Target = target;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DataGraphEdge<Type>);
        }

        public bool Equals(DataGraphEdge<Type> other)
        {
            return other != null &&
                   Source == other.Source &&
                   Link == other.Link &&
                   Target == other.Target;
        }

        public override int GetHashCode()
        {
            var hashCode = -1200287535;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Source);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Link);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Target);
            return hashCode;
        }
    }
}
