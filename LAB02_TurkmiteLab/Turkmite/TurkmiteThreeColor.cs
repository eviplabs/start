using OpenCvSharp;

namespace TurkMite
{
    class TurkmiteThreeColor : TurkmiteBase
    {
        public override int IterationCount => 20000;
        readonly private Vec3b black = new Vec3b(0, 0, 0);
        readonly private Vec3b white = new Vec3b(255, 255, 255);
        readonly private Vec3b red = new Vec3b(255, 0, 0);
        public TurkmiteThreeColor(Mat image) : base(image)
        {

        }
        protected override (Vec3b newColor, int deltaDirection) NextDirectionColor(Vec3b currentColor)
        {
            if (currentColor == black)
            {
                return (white, 1);
            }
            else if (currentColor == white)
            {
                return (red, -1);
            }
            else
            {
                return (black, -1);
            }
        }
    }
}
