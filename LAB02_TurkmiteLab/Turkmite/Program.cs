using OpenCvSharp;

namespace TurkMite
{
  class TurkMite
  {
    public Mat Image {get;}
    private int x;
    private int y;
    private int direction;
    private Mat.Indexer<Vec3b> indexer;
    

    readonly Vec3b black = new Vec3b(255, 255, 255);
    readonly Vec3b white = new Vec3b(0, 0,0);
    public TurkMite(Mat image)
    {
      Image = image;
      x = image.Cols / 2;
      y = image.Rows / 2;
      direction = 0;
      indexer = image.GetGenericIndexer<Vec3b>();

    }

    public void Step()
    {
      Vec3b currentColor = indexer[y, x];
      if (currentColor == white)
      {
        indexer[y, x] = black;
        direction++;
        if (direction > 3)
          direction = 0;
      }
      else
      {
        indexer[y, x] = white;
        direction--;
        if (direction < 0)
          direction = 3;
      }
      var delta = new(int x, int y)[] { (0, -1), (1, 0), (0, 1), (-1, 0) };

      x += delta[direction].x;
      y += delta[direction].y;

      x = Math.Max(0, Math.Min(Image.Cols, x));
      y = Math.Max(0, Math.Min(Image.Rows, y));

    }
  }
  class Program
  {

    static void Main()
    {
      Mat img = new Mat(200, 200, MatType.CV_8UC3, new Scalar(0, 0, 0));
      var turkmite = new TurkMite(img);
      for(int i=0; i<13000; i++)
      {
        turkmite.Step();
      }
      Cv2.ImShow("TurkMite", turkmite.Image);
      Cv2.WaitKey();
    }
  }
}
