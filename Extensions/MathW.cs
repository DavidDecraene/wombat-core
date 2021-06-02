using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class MathW
    {
        private const int X_PRIME = 1619;
        private const int Y_PRIME = 31337;
        private const int Z_PRIME = 6971;

        /**
         * When given 4 point values, calculate value for point somewhere inbetween
         * c00 value at x = 0, y = 0
         * c10 value at x = 1, y = 0
         * etc
         * tx => fragment of x between min x and max x
         * ty => fragment of y between min y and max y
         * 
         * // convert i,j to grid coordinates
            float gx = i / float(imageWidth) * gridSizeX; // be careful to interpolate boundaries 
            float gy = j / float(imageWidth) * gridSizeY; // be careful to interpolate boundaries 
            int gxi = int(gx); 
            int gyi = int(gy); 
         * */
        public static float BilinearLerp(float c00, float c10, float c01, float c11, float tx, float ty)
        {
            // See also https://stackoverflow.com/questions/21731324/bicubic-interpolation-artifacts-image-upscale
            // Alternatively try Mathf.SmoothStep for smoother results...
            return Mathf.Lerp(Mathf.Lerp(c00, c10, tx), Mathf.Lerp(c01, c11, tx), ty);
        }

        /**
         * 
        *     https://www.gamasutra.com/blogs/VivekTank/20180806/323709/How_to_work_with_Bezier_Curve_in_Games_with_Unity.php
         *    https://en.wikibooks.org/wiki/Cg_Programming/Unity/B%C3%A9zier_Curves
        *     Give three vectors A B C/ B(t) = (1-t)2A + 2(1-t)tB + t2C 
         *    Estimate length: Distance(C,  A) = L1
         *    Distance (C, B) + Distance(B, A) = L2
         *    Take average of L1 + L2 = +- length
         *    To move 1 unit, estimate length: (32 for example)
         *   1 / 32 = 1 unitgive or take..
         */
        public static Vector3 Bezier(Vector3 start, Vector3 control, Vector3 end, float ratio)
        {
            float t = Mathf.Clamp01(ratio);
            Vector3 result = (1 - t) * (1 - t) * start + 2 * (1 - t) * t * control + t * t * end;
            return result;
        }

        public static float Remap(float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static int SafeFloor(float v)
        {
            return Mathf.FloorToInt(v); // + float.Epsilon);
        }

        public static Vector3 GetWorldScale(Transform transform)
        {
            Vector3 worldScale = transform.localScale;
            Transform parent = transform.parent;

            while (parent != null)
            {
                worldScale = Vector3.Scale(worldScale, parent.localScale);
                parent = parent.parent;
            }

            return worldScale;
        }


        public static Vector3 Clone(Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z);
            // return new Vector3Int(SafeFloor(v.x), SafeFloor(v.y), SafeFloor(v.z));
        }
        /**
        public static Vector3 FloorEpsilon(Vector3 v)
        {
            float x = Mathf.FloorToInt(v.x + float.Epsilon);
            float y = Mathf.FloorToInt(v.y + float.Epsilon) ;
            float z = Mathf.FloorToInt(v.z + float.Epsilon) ;
            return new Vector3(x, y, z);
            // return new Vector3Int(SafeFloor(v.x), SafeFloor(v.y), SafeFloor(v.z));
        }*/

        public static Vector3Int SafeFloor(Vector3 v)
        {
            return Vector3Int.FloorToInt(v);
            // return new Vector3Int(SafeFloor(v.x), SafeFloor(v.y), SafeFloor(v.z));
        }

        public static int Hash2D(int seed, int x, int y)
        {
            int hash = seed;
            hash ^= X_PRIME * x;
            hash ^= Y_PRIME * y;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            return hash;
        }

        public static int Hash3D(int seed, int x, int y, int z)
        {
            int hash = seed;
            hash ^= X_PRIME * x;
            hash ^= Y_PRIME * y;
            hash ^= Z_PRIME * z;

            hash = hash * hash * hash * 60493;
            hash = (hash >> 13) ^ hash;

            return hash;
        }

        public static int Hash3D(int seed, Vector3Int vector)
        {
            return Hash3D(seed, vector.x, vector.y, vector.z);
        }

        public static int FastRound(float f) { return (f >= 0) ? (int)(f + (float)0.5) : (int)(f - (float)0.5); }
        public static float ValCoord2D(int seed, int x, int y)
        {
            int n = seed;
            n ^= X_PRIME * x;
            n ^= Y_PRIME * y;

            return (n * n * n * 60493) / (float)2147483648.0;
        }

        public static void Shuffle<T>(IList<T> list, System.Random rd)
        {
            if (list.Count <= 1) return;
            int currentIndex = list.Count;
            int randomIndex = 0;
            T temporaryValue;
            while (currentIndex > 0)
            {
                randomIndex = Mathf.FloorToInt(rd.NextFloat() * currentIndex);
                currentIndex--;
                temporaryValue = list[currentIndex];
                if (list.Count <= randomIndex) continue; // for weird situations on shutdown or so.
                list[currentIndex] = list[randomIndex];
                list[randomIndex] = temporaryValue;
            }
        }



        public static Vector3Int ChunkCoordinates(Vector3 position, int chunkSize)
        {

            return new Vector3Int(Mathf.FloorToInt(position.x / chunkSize), 
                Mathf.FloorToInt(position.y / chunkSize), 
                Mathf.FloorToInt(position.z / chunkSize));
        }

        public static Vector3Int VoxelCoordinates(Vector3 position, Vector3Int chunk, int chunkSize)
        {
            Vector3 voxelPos = position - (chunk * chunkSize);

            return new Vector3Int(Mathf.RoundToInt(voxelPos.x), 
                Mathf.RoundToInt(voxelPos.y), Mathf.RoundToInt(voxelPos.z));
        }

        public static Vector3Int VoxelCoordinatesFloor(Vector3 position, Vector3Int chunk, int chunkSize)
        {
            Vector3 voxelPos = position - (chunk * chunkSize);

            return new Vector3Int(Mathf.FloorToInt(voxelPos.x),
                Mathf.FloorToInt(voxelPos.y), Mathf.FloorToInt(voxelPos.z));
        }

        public static float CalcDegrees(float x, float y)
        {
            return Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        }
    }
}
