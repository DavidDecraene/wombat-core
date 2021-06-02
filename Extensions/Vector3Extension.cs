using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public static class Vector3Extension
    {
        public static float ManhattanDistance(this Vector3 source, Vector3 target)
        {
            return Mathf.Abs(source.x - target.x) + Mathf.Abs(source.y - target.y) + Mathf.Abs(source.z - target.z);

        }

        public static float ChebyshevDistance(this Vector3 source, Vector3 target)
        {
            return Mathf.Max(Mathf.Abs(source.x - target.x), Mathf.Abs(source.y - target.y), Mathf.Abs(source.z - target.z));
        }

        public static Vector3Int AsVoxel(this Vector3 input)
        {
            return Vector3Int.FloorToInt(input);
        }

        public static Vector3Int RoundVoxel(this Vector3 input)
        {
            return Vector3Int.RoundToInt(input);
        }
    }
}
