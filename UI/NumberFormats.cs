using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    struct NFData
    {
        public readonly float value;
        public readonly string prefix;

        public NFData(float value, string prefix)
        {
            this.value = value;
            this.prefix = prefix;
        }

    }

    public class NumberFormats
    {
        public static readonly NumberFormats instance = new NumberFormats();

        private readonly List<NFData> formats = new List<NFData>();
        private readonly NFData defaultFormat = new NFData(1, "");

        private NumberFormats()
        {
            formats.Add(new NFData(1E3f, "K"));
            formats.Add(new NFData(1E6f, "M"));
            formats.Add(new NFData(1E9f, "B"));
            formats.Add(new NFData(1E12f, "T"));
            formats.Add(new NFData(1E15f, "Qa"));
        }

        private float GetDivide(float v)
        {
            if (v >= 10) return 10;
            return 100;
        }

        public string Format(float v)
        {
            NFData selection = defaultFormat;
            if (v >= 1000)
            {
                foreach (NFData format in formats)
                {
                    if (format.value > v) break;
                    selection = format;
                }
            }
            float r = selection.value;
            float vv = v / r;
            float dvd = GetDivide(vv);
            float vvm = Mathf.FloorToInt(vv * dvd) / dvd;
            return string.Format("{0}{1}", vvm, selection.prefix);
            
        }
    }
}
