using OpenCvSharp;

namespace TurkMite
{
    class Program
    {
        static void Main()
        {
            Mat img = new Mat(200, 200, MatType.CV_8UC3, new Scalar(0, 0, 0));
            var turkmite = new StateTurkmite(img);
            for (int i = 0; i < turkmite.IterationCount; i++)
            {
                turkmite.Step();
            }
            Cv2.ImShow("TurkMite", turkmite.Image);
            Cv2.WaitKey();
        }
    }
}
