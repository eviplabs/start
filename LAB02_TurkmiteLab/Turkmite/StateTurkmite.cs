using OpenCvSharp;

namespace TurkMite
{
    class StateTurkmite : TurkmiteBase
    {
        public StateBase CurrentState { get; set; }

        readonly public StateB stateB;
        readonly public StateC stateC;
        readonly public StateA stateA;

        public override int IterationCount => 800000;
       

        public StateTurkmite(Mat image) : base(image)
        {
            stateA = new StateA(this);
            stateB = new StateB(this);
            stateC = new StateC(this);
            CurrentState = stateA;

        }
        protected override (Vec3b newColor, int deltaDirection) NextDirectionColor(Vec3b currentColor)
        {
            return CurrentState.HandleUpdate(currentColor);
        }

    }
}
