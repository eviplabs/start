using OpenCvSharp;

namespace TurkMite
{
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
                return (red, -1);
            }
            _turkmite.stateC.Enter();
            return (black, 0);
        }
    }
}
