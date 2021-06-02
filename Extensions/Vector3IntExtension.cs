using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public static class Vector3IntExtension
    {
        public static int ManhattanDistance(this Vector3Int source, Vector3Int target)
        {
            return Mathf.Abs(source.x - target.x) + Mathf.Abs(source.y - target.y) + Mathf.Abs(source.z - target.z);

        }

        public static int ChebyshevDistance(this Vector3Int source, Vector3Int target)
        {
            return Mathf.Max(Mathf.Abs(source.x - target.x), Mathf.Abs(source.y - target.y), Mathf.Abs(source.z - target.z));
        }

    }
}
