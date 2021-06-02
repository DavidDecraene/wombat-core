using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace Wombat
{
    class JsonLevel
    {
        public int childCount;
        public bool array;

    }

    public class SimpleJSONWriter
    {
        private StringBuilder builder = new StringBuilder();
        private readonly char quote = '"';
        private readonly char openBracket = '{';
        private readonly char closeBracket = '}';
        private readonly char openArray = '[';
        private readonly char closeArray = ']';
        private readonly char comma = ',';
        private readonly char space = ' ';
        private readonly char colon = ':';
        private readonly char newLine = '\n';
        private Stack<JsonLevel> stack = new Stack<JsonLevel>();

        public SimpleJSONWriter NewLine()
        {
            builder.Append(newLine);
            return this;

        }


        public SimpleJSONWriter StartObject()
        {
            if (stack.Count > 0)
            {
                JsonLevel parent = stack.Peek();
                if (parent.childCount > 0)
                {
                    builder.Append(comma);
                    builder.Append(space);
                }
                parent.childCount++;
            }
            JsonLevel lvl = new JsonLevel();
            stack.Push(lvl);
            builder.Append(openBracket);
            builder.Append(space);
            return this;
        }

        private void WriteField(string name)
        {
            JsonLevel parent = stack.Peek();
            if (parent.childCount > 0)
            {
                builder.Append(comma);
                builder.Append(space);
            }
            builder.Append(quote);
            builder.Append(name);
            builder.Append(quote);
            builder.Append(space);
            builder.Append(colon);
            builder.Append(space);
            parent.childCount++;
        }

        private void WriteArrayEntry()
        {
            JsonLevel parent = stack.Peek();
            if (parent.childCount > 0)
            {
                builder.Append(comma);
                builder.Append(space);
            }
            parent.childCount++;
        }

        public SimpleJSONWriter Value(string name, string value)
        {
            WriteField(name);
            builder.Append(quote);
            builder.Append(value);
            builder.Append(quote);
            return this;
        }

        public SimpleJSONWriter Value(string name, float value)
        {
            WriteField(name);
            builder.Append(value);
            return this;
        }

        public SimpleJSONWriter Value(string name, int value)
        {
            WriteField(name);
            builder.Append(value);
            return this;
        }

        public SimpleJSONWriter Value(string name, bool value)
        {
            WriteField(name);
            builder.Append(value ? "true" : "false");
            return this;
        }

        public SimpleJSONWriter Value(string value)
        {
            WriteArrayEntry();
            builder.Append(quote);
            builder.Append(value);
            builder.Append(quote);
            return this;
        }



        public SimpleJSONWriter StartArray(string name)
        {
            WriteField(name);
            JsonLevel lvl = new JsonLevel();
            lvl.array = true;
            stack.Push(lvl);
            builder.Append(openArray);
            return this;
        }

        public SimpleJSONWriter EndArray()
        {
            builder.Append(closeArray);
            stack.Pop();
            return this;
        }

        public SimpleJSONWriter StartObject(string name)
        {
            WriteField(name);
            JsonLevel lvl = new JsonLevel();
            stack.Push(lvl);
            builder.Append(openBracket);
            builder.Append(space);
            return this;
        }

        public SimpleJSONWriter EndObject()
        {
            builder.Append(space);
            builder.Append(closeBracket);
            stack.Pop();
            return this;
        }

        public override string ToString()
        {
            return builder.ToString();
        }

    }
}
