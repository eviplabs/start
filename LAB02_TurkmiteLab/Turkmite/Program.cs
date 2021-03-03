using OpenCvSharp;

namespace TurkMite
{

  class TurkmiteThreeColor: TurkmiteBase
  {
    readonly private Vec3b black = new Vec3b(0, 0, 0);
    readonly private Vec3b white = new Vec3b(255, 255, 255);
    readonly private Vec3b red = new Vec3b(255, 0, 0);
    public TurkmiteThreeColor(Mat image) : base(image)
    {

    }
    protected override (Vec3b newColor, int deltaDirection) NextDirectionColor(Vec3b currentColor)
    {
      if(currentColor == black)
      {
        return(white, 1);
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
  class TurkMiteOriginal : TurkmiteBase
  {


    readonly Vec3b black = new Vec3b(255, 255, 255);
    readonly Vec3b white = new Vec3b(0, 0,0);
    public TurkmitOrigin(Mat image) : base(image)
    {
    }
    private (Vec3b newColor, int deltaDirection)  NextDirectionColor(Vec3b currentColor)
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

  abstract class TurkmiteBase
  {

    public Mat Image {get;}
    private int x;
    private int y;
    private int direction;
    private Mat.Indexer<Vec3b> indexer;

    private readonly (int x, int y)[] delta = new (int x, int y)[] { (0, -1), (1, 0), (0, 1), (-1, 0) };
    public TurkMite(Mat image)
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
      (indexer[y, x], deltaDirection)  = NextDirectionColor(indexer[y,x]);
      Move(deltaDirection);
    }

    protected abstract (Vec3b newColor, int deltaDirection) NextDirectionColor(Vec3b currentColor);

  }
  class Program
  {

    static void Main()
    {
      Mat img = new Mat(200, 200, MatType.CV_8UC3, new Scalar(0, 0, 0));
      var turkmite = new TurkMiteOriginal(img);
      for(int i=0; i<13000; i++)
      {
        turkmite.Step();
      }
      Cv2.ImShow("TurkMite", turkmite.Image);
      Cv2.WaitKey();
    }
  }
}
