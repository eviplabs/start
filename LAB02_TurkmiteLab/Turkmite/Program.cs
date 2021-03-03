using OpenCvSharp;

namespace TurkMite
{
  /* milyen esemenyek vannak 
A:
   harmadik fekete mezot szinezi
    onThreeBlack -> change direction 
    enter B 
  B: 
  fekete mezot feherre szinez, balra fordul es megy egyet elore
  feher mezot pirosra szinez jobbra fordul es megy egyet elore
  piroz mezot feketere szinez elore megy es valt c allapotra

  C: 
    allit c-re enternel
    elore megy tovabb
    fekete mezot feherre szinez, es balra fordul, es megy egyenesen
    piros mezot feketere szines es balra fordul
    ha fekete: enter A
    ha nem: enter B
   */ 
  
  class StateA: StateBase{
    private int colourCounter;
    public StateA(StateTurkmite st) : base(st)
    {
      colourCounter = 0;
    }

    public Enter()
    {
      _turkmite.currentState = _turkmite.stateA;
      colourCounter = 0;
    }
    public override HandleUpdate(Vec3 currentColor)
    {
      /* egyenesen megy tovabb */
      if(currentColor == black)
      {
        colourCounter++;
      } 
      if(colourCounter == 3)
      {
        /* enter next state: B*/
        _turkmite.stateB.Enter();
        return (white,2);
      }
      return (currentColor, 0);
    }
}
  class StateB: StateBase{
    public StateB(StateTurkmite st) : base(st)
    {
    }
    public Enter()
    {
      _turkmite.currentState = _turkmite.stateB;
    }
    public override HandleUpdate(Vec3 currentColor)
    {
      if(currentColor == black)
      {
        return (white,1);
      } 
      if(currentColor == white)
      {
        return(red,3);
      }
      _turkmite.stateC.Enter();
      return (black, 0);
    }

  }
  class StateC: StateBase{
    private bool first_state;
    private int counter;
    public StateC(StateTurkmite st) : base(st)
    {
      counter = 0;
      first_state = true;
    }
      
    public Enter()
    {
      first_state = true;
      counter = 0;
      _turkmite.currentState = _turkmite.stateC;
    }
    public override HandleUpdate(Vec3 currentColor)
    {
      counter++;
      StateTransition();
      if(first_state)
      {
        first_state = false;
        return(red, 0);
      }
      if(currentColor == black)
      {
        return(white, 1);
      }
      if(currentColor == white)
      {
        return(red, 0);
      }
      if(currentColor == red)
      {
        return(black, 1);
      }
    }
    private void StateTransition(Vec3 currentColor)
    {
      if(counter == 5)
      {
        if(currentColor == red)
        {
          _turkmite.stateB.Enter();
        }
        else
        {
          _turkmite.stateA.Enter();
        }
      } 
    }
  }
  class StateBase
  {
    private StateTurkmit _turkmite;
    public StateBase(StateTurkmite stateTurkmite){
      _turkmite = stateTurkmite;
    }
    public abstract Enter();
    public abstract HandleUpdate(Vec3b currentColor);
  }
  class StateTurkmite: Turkmite
  {
    private State currentState;

    readonly private StateB stateB = new StateB(this);
    readonly private StateC stateC = new StateC(this);
    readonly private StateA stateA = new StateA(this);

    readonly private Vec3b black = new Vec3b(0, 0, 0);
    readonly private Vec3b white = new Vec3b(255, 255, 255);
    readonly private Vec3b red = new Vec3b(255, 0, 0);

    public TurkmiteThreeColor(Mat image) : base(image)
    {
      currentState = stateA;
      IterationCount = 1000;
    }
    protected override (Vec3b newColor, int deltaDirection) NextDirectionColor(Vec3b currentColor)
  {
    return currentState.HandleUpdate(currentColor);
  }

}
  class TurkmiteThreeColor: TurkmiteBase
  {
    readonly private Vec3b black = new Vec3b(0, 0, 0);
    readonly private Vec3b white = new Vec3b(255, 255, 255);
    readonly private Vec3b red = new Vec3b(255, 0, 0);
    public TurkmiteThreeColor(Mat image) : base(image)
    {
      IterationCount = 100;
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
    private StateBase CurrentState{get;}
    public TurkmitOrigin(Mat image) : base(image)
    {
      IterationCount = 200;
    }
    private override (Vec3b newColor, int deltaDirection)  NextDirectionColor(Vec3b currentColor)
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
    public abstract int IterationCount{get;}

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
      for(int i=0; i<turkmite.IterationNumber; i++)
      {
        turkmite.Step();
      }
      Cv2.ImShow("TurkMite", turkmite.Image);
      Cv2.WaitKey();
    }
  }
}
