using OpenCvSharp;

namespace TurkMite
{
    class StateTurkmite : TurkmiteBase
    {
        public StateBase currentState;

        readonly public StateB stateB;
        readonly public StateC stateC;
        readonly public StateA stateA;
        public override int IterationCount => 15000;
       

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
}
