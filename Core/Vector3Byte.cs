using System;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Wombat
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector3Byte : IEquatable<Vector3Byte>, IFormattable
    {
        public byte x { get { return m_X; } set { m_X = value; } }
        public byte y { get { return m_Y; } set { m_Y = value; } }
        public byte z { get { return m_Z; } set { m_Z = value; } }

        private byte m_X;
        private byte m_Y;
        private byte m_Z;

        public Vector3Byte(byte x, byte y, byte z)
        {
            m_X = x;
            m_Y = y;
            m_Z = z;
        }

        // Set x, y and z components of an existing Vector.
        public void Set(byte x, byte y, byte z)
        {
            m_X = x;
            m_Y = y;
            m_Z = z;
        }

        // Access the /x/, /y/ or /z/ component using [0], [1] or [2] respectively.
        public byte this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default:
                        throw new IndexOutOfRangeException(string.Format("Invalid Vector3Byte index addressed: {0}!", index));
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default:
                        throw new IndexOutOfRangeException(string.Format("Invalid Vector3Byte index addressed: {0}!", index));
                }
            }
        }
        


        // Converts a Vector3Int to a [[Vector3]].
        public static implicit operator Vector3(Vector3Byte v)
        {
            return new Vector3(v.x, v.y, v.z);
        }
        public static implicit operator Vector3Int(Vector3Byte v)
        {
            return new Vector3Int(v.x, v.y, v.z);
        }


        public static bool operator ==(Vector3Byte lhs, Vector3Byte rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
        }

        public static bool operator !=(Vector3Byte lhs, Vector3Byte rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector3Byte)) return false;

            return Equals((Vector3Byte)other);
        }

        public bool Equals(Vector3Byte other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            var yHash = y.GetHashCode();
            var zHash = z.GetHashCode();
            return x.GetHashCode() ^ (yHash << 4) ^ (yHash >> 28) ^ (zHash >> 4) ^ (zHash << 28);
        }

        public override string ToString()
        {
            return ToString(null, CultureInfo.InvariantCulture.NumberFormat);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.InvariantCulture.NumberFormat);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("({0}, {1}, {2})", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider));
        }
    }
}
