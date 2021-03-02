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
      if (currentColor == new Vec3b(0, 0, 0))
      {
        indexer[y, x] = new Vec3b(255, 255, 255);
        direction++;
        if (direction > 3)
          direction = 0;
      }
      else
      {
        indexer[y, x] = new Vec3b(0, 0, 0);
        direction--;
        if (direction < 0)
          direction = 3;
      }
      switch (direction)
      {
        case 0:
          y--;
          break;
        case 1:
          x++;
          break;
        case 2:
          y++;
          break;
        case 3:
          x--;
          break;
      }
      if (x < 0)
        x = 199;
      if (x > 199)
        x = 0;
      if (y < 0)
        y = 199;
      if (y > 199)
        y = 0;
    }
  }
  class Program
  {

    static void Main()
    {
      Mat img = new Mat(200, 200, MatType.CV_8UC3, new Scalar(0, 0, 0));
      var indexer = img.GetGenericIndexer<Vec3b>();
      int x = 100;
      int y = 100;
      int direction = 0;  // 0 up, 1 right, 2 down, 3 left
      for(int i=0; i<13000; i++)
      {
        Vec3b currentColor = indexer[y, x];
        if (currentColor == new Vec3b(0,0,0))
        {
          indexer[y, x] = new Vec3b(255, 255, 255);
          direction++;
          if (direction > 3)
            direction = 0;
        }
        else
        {
          indexer[y, x] = new Vec3b(0, 0, 0);
          direction--;
          if (direction < 0)
            direction = 3;
        }
        switch(direction)
        {
          case 0:
            y--;
            break;
          case 1:
            x++;
            break;
          case 2:
            y++;
            break;
          case 3:
            x--;
            break;
        }
        if (x < 0)
          x = 199;
        if (x > 199)
          x = 0;
        if (y < 0)
          y = 199;
        if (y > 199)
          y = 0;
      }
      Cv2.ImShow("TurkMite", img);
      Cv2.WaitKey();
    }
  }
}
