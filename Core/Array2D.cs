using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wombat
{
    public class Array2D<Data>
    {
        public Data[,] values;
        private bool invertY = false;

        public static Array2D<Data> From1D(Data[] values, int width, bool invertY)
        {
            int height = width;
            Array2D<Data> result = new Array2D<Data>(width, height, invertY);
            for (int j = 0; j < height; j++)
            {
                int y = invertY ? height - (j + 1) : j;
                for (int i = 0; i < width; i++)
                {
                    result.values[i, y] = values[j * height + i];
                }
            }
            return result;
        }

        public Array2D(int width, int height)
        {
            this.values = new Data[width, height];
        }

        public Array2D(int width, int height, bool invertY)
        {
            this.values = new Data[width, height];
            this.invertY = invertY;
        }

        public void DebugPrint(System.Predicate<Data> filter)
        {

            int width = values.GetLength(0);
            int height = values.GetLength(1);
            Data[] result = new Data[width * height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if(filter.Invoke(values[i, j])) Debug.Log(i + " " + j + " " + values[i, j]);
                }
            }

        }

        public Data[] Get1D()
        {
            int width = values.GetLength(0);
            int height = values.GetLength(1);
            Data[] result = new Data[width * height];
             for (int j = 0; j < height; j++)
            {
                int y = this.invertY ? height - (j + 1) : j;
                for (int i = 0; i < width; i++)
                {
                    result[j * height + i] = values[i, y];
                }
            }
            return result;
        }

        public Array2D<Data> Rotate()
        {
            return Rotate(this);
        }

        public Array2D<Data> EmptyCopy()
        {
            return new Array2D<Data>(this.values.GetLength(0), this.values.GetLength(1), this.invertY);
        }

        public Array2D<Data> MirrorX(Array2D<Data> target)
        {

            int w = this.values.GetLength(0);
            int h = this.values.GetLength(1);
            int hw = w / 2;
            for(int x = 0; x < hw; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    int x2 = w - x - 1;
                    // Swap Values
                    Data d2 = this.values[x2, y];
                    Data d = this.values[x, y];
                    target.values[x, y] = d2;
                    target.values[x2, y] = d;

                }
            }

            return target;
        }


        public Array2D<Data> Rotate(Array2D<Data>  target)
        {
            //const x = Math.floor(n/ 2);
            //const y = n - 1;
            // Consider all squares one by one
            int h = this.values.GetLength(0);
            // int h = this.values.GetLength(1);

            for (int x = 0; x < h / 2; x++)
            {
                // Consider elements in group of 4 in
                // current square
                for (int y = x; y < h - x - 1; y++)
                {
                    // store current cell in temp variable
                    Data temp = this.values[x, y];

                    // move values from right to top
                    target.values[x, y] = this.values[y, h - 1 - x];

                    // move values from bottom to right
                    target.values[y, h - 1 - x] = this.values[h - 1 - x, h - 1 - y];

                    // move values from left to bottom
                    target.values[h - 1 - x, h - 1 - y] = this.values[h - 1 - y, x];

                    // assign temp to left
                    target.values[h - 1 - y, x] = temp;
                }
            }
            return target;
        }


        public Array2D<Data> Splice(int x, int y, int w, int h)
        {
            Array2D<Data> result = new Array2D<Data>(w, h);
            int dw = Mathf.Min(x + w, this.values.GetLength(0));
            int dh = Mathf.Min(y + h, this.values.GetLength(1));
            for (int i = x; i < dw; i++)
            {
                for (int j = y; j < dh; j++)
                {
                    result.values[i - x, j - y] = this.values[i, j];
                }
            }
            return result;
        }


    }
}
