using OpenCvSharp;
using System;

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

    class StateA : StateBase
    {
        private int colourCounter;
        public StateA(StateTurkmite st) : base(st)
        {
            colourCounter = 0;
        }

        public override void Enter()
        {
            _turkmite.currentState = _turkmite.stateA;
            colourCounter = 0;
        }
        public override (Vec3b newColor, int deltaDirection) HandleUpdate(Vec3b currentColor)
        {
            /* egyenesen megy tovabb */
            if (currentColor == black)
            {
                colourCounter++;
            }
            if (colourCounter == 3)
            {
                /* enter next state: B*/
                _turkmite.stateB.Enter();
                return (white, 2);
            }
            return (currentColor, 0);
        }
    }
    class StateB : StateBase
    {
        public StateB(StateTurkmite st) : base(st)
        {
        }
        public override void Enter()
        {
            _turkmite.currentState = _turkmite.stateB;
        }
        public override (Vec3b newColor, int deltaDirection) HandleUpdate(Vec3b currentColor)
        {
            if (currentColor == black)
            {
                return (white, 1);
            }
            if (currentColor == white)
            {
                return (red, 3);
            }
            _turkmite.stateC.Enter();
            return (black, 0);
        }

    }
    class StateC : StateBase
    {
        private bool first_state;
        private int counter;
        public StateC(StateTurkmite st) : base(st)
        {
            counter = 0;
            first_state = true;
        }

        public override void Enter()
        {
            first_state = true;
            counter = 0;
            _turkmite.currentState = _turkmite.stateC;
        }
        public override (Vec3b newColor, int deltaDirection) HandleUpdate(Vec3b currentColor)
        {
            counter++;
            StateTransition(currentColor);
            if (first_state)
            {
                first_state = false;
                return (red, 0);
            }
            if (currentColor == black)
            {
                return (white, 1);
            }
            if (currentColor == white)
            {
                return (red, 0);
            }
            else
            {
                return (black, 1);
            }
        }
        private void StateTransition(Vec3b currentColor)
        {
            if (counter == 5)
            {
                if (currentColor == red)
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

    abstract class StateBase
    {

        public StateTurkmite _turkmite;
        readonly protected Vec3b black = new Vec3b(0, 0, 0);
        readonly protected Vec3b white = new Vec3b(255, 255, 255);
        readonly protected Vec3b red = new Vec3b(255, 0, 0);
        public StateBase(StateTurkmite stateTurkmite)
        {
            _turkmite = stateTurkmite;
        }
        public abstract void Enter();
        public abstract (Vec3b newColor, int deltaDirection) HandleUpdate(Vec3b currentColor);
    }
    class StateTurkmite : TurkmiteBase
    {
        public StateBase currentState;

        readonly public StateB stateB;
        readonly public StateC stateC;
        readonly public StateA stateA;
        public override int IterationCount => 15000;
        readonly private Vec3b black = new Vec3b(0, 0, 0);
        readonly private Vec3b white = new Vec3b(255, 255, 255);
        readonly private Vec3b red = new Vec3b(255, 0, 0);

        public StateTurkmite(Mat image) : base(image)
        {
            stateA = new StateA(this);
            stateB = new StateB(this);
            stateC = new StateC(this);
            currentState = stateA;

        }
        protected override (Vec3b newColor, int deltaDirection) NextDirectionColor(Vec3b currentColor)
        {
            return currentState.HandleUpdate(currentColor);
        }

    }
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
