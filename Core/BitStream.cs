using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace Wombat
{
    public class BitStreamWriter
    {
        private readonly List<byte> buffer = new List<byte>();
        public static readonly char BoundsMarker = '\n';

        public int Count { get => buffer.Count;  }

        public void MarkBound()
        {
            Append(BoundsMarker);
        }

        public void MarkRowStart()
        {

            Append((char)0);
        }

        public void Append(byte b)
        {
            buffer.Add(b);
        }
        public void Append(byte[] b)
        {
            for(int i = 0; i < b.Length; i++)  buffer.Add(b[i]);
        }

        public void Append(char value)
        {
            byte[] b = BitConverter.GetBytes(value); // 4
            Append(b);
        }

        public void Append(bool value)
        {
            Append(Convert.ToByte(value));
        }

        public void Append(float value)
        {
            byte[] b = BitConverter.GetBytes(value); // 4
            Append(b);
        }

        public void Append(int value)
        {
            byte[] b = BitConverter.GetBytes(value); // 4
            Append(b);
        }

        public void AppendNullGuid()
        {
            Append((byte)0);
        }

        public void Append(Guid value)
        {
            Append((byte)1);
            Append(value.ToByteArray()); // 16 bytes
        }

        public void Append(string value)
        {
            if (value == null)
            {
                Append(0);
                return;
            }
            byte[] b = Encoding.UTF8.GetBytes(value);
            Append(b.Length);
            Append(b);
        }

        public void Append(Vector3 vector)
        {
            Append(vector.x);
            Append(vector.y);
            Append(vector.z);
        }

        public byte[] ToArray()
        {
            return buffer.ToArray();
        }



    }

    public class BitStreamReader
    {
        private readonly byte[] data;
        private int index;

        public static readonly char BoundsMarker = '\n';

        public bool Closed { get => index >= data.Length; }
        public int Count { get => index; }

        public BitStreamReader(byte[] data)
        {
            this.data = data;
        }

        public void Printrest()
        {
            int ln = data.Length - index;
            //Debug.Log("Remains: " + ln +", index  " + index + "  " + (int)BoundsMarker + (char)0);
        }

        public bool IsBound()
        {
            if (Closed) return false;
            return PeekChar(BoundsMarker);
        }

        public bool ConsumeBound(bool warn = true)
        {
            bool isBound = ReadChar() == BoundsMarker;
            if (!isBound && warn) Debug.LogError("Not a bound");
            return isBound;
        }

        public bool ConsumeRowStart()
        {
            bool isBound =(int) ReadChar() == 0;
            if (!isBound) Debug.LogError("Not a row start");
            return isBound;
        }

        public bool PeekChar(char c)
        {
            return BitConverter.ToChar(data, index) == c;
        }

        public byte Peek()
        {
            return data[index];
        }

        public char PeekChar()
        {
            return BitConverter.ToChar(data, index);
        }

        public bool ReadBool()
        {
            bool state = BitConverter.ToBoolean(data, index);
            index = index + 1;
            return state;
        }

        public char ReadChar()
        {
            char r =  BitConverter.ToChar(data, index);
            index = index + 2;
            return r;
        }

        public bool TryReadGuid(out Guid result)
        {
            if (Closed) return false;
            byte b = ReadByte();
            if (b == 0) return false;
            byte[] guid = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                guid[i] = data[index++];
            }
            result = new Guid(guid);
            return true;
        }

        public Vector3 ReadVector()
        {
            float x = ReadFloat();
            float y = ReadFloat();
            float z = ReadFloat();
           return new Vector3(x, y, z);
        }

        public Quaternion ReadEulerAngles()
        {
            float x = ReadFloat();
            float y = ReadFloat();
            float z = ReadFloat();
            return Quaternion.Euler(x, y, z);
        }

        public float ReadFloat()
        {
            float r = BitConverter.ToSingle(data, index);
            index = index + 4;
            return r;
        }

        public byte ReadByte()
        {
            return data[index++];
        }

        public int ReadInt()
        {
            int r = BitConverter.ToInt32(data, index);
            index = index + 4;
            return r;
        }


        public string ReadString()
        {
            if (Closed) return null;
            int ln = ReadInt();
            if (ln <= 0) return null;
            string r = Encoding.UTF8.GetString(data, index, ln);
            index = index + ln;
            return r;
        }

    }
}
