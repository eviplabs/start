using OpenCvSharp;

namespace TurkMite
{
    class TurkMiteOriginal : TurkmiteBase
    {
        public override int IterationCount => 13000;

        readonly Vec3b black = new Vec3b(255, 255, 255);
        readonly Vec3b white = new Vec3b(0, 0, 0);
        private StateBase CurrentState { get; }
        public TurkMiteOriginal(Mat image) : base(image)
        {
        }
        protected override (Vec3b newColor, int deltaDirection) NextDirectionColor(Vec3b currentColor)
        {
            if (currentColor == white)
            {
                return (black, 1);
            }
            else
            {
                return (white, -1);
            }
        }
    }
}
