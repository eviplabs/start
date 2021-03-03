using OpenCvSharp;
using System;

namespace TurkMite
{
    abstract class TurkmiteBase
    {

        public Mat Image { get; }
        private int x;
        private int y;
        private int direction;
        private Mat.Indexer<Vec3b> indexer;
        public abstract int IterationCount { get; }

        private readonly (int x, int y)[] delta = new (int x, int y)[] { (0, -1), (1, 0), (0, 1), (-1, 0) };
        public TurkmiteBase(Mat image)
        {
            Image = image;
            x = image.Cols / 2;
            y = image.Rows / 2;
            direction = 0;
            indexer = image.GetGenericIndexer<Vec3b>();

        }

        public void Move(int deltaDirection)
        {
            direction += deltaDirection;
            direction = (direction + 4) % 4;


            x += delta[direction].x;
            y += delta[direction].y;

            x = Math.Max(0, Math.Min(Image.Cols, x));
            y = Math.Max(0, Math.Min(Image.Rows, y));
        }

        public void Step()
        {
            int deltaDirection;
            (indexer[y, x], deltaDirection) = NextDirectionColor(indexer[y, x]);
            Move(deltaDirection);
        }

        protected abstract (Vec3b newColor, int deltaDirection) NextDirectionColor(Vec3b currentColor);

    }
}
