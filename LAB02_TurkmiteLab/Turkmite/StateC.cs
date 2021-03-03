using OpenCvSharp;

namespace TurkMite
{
    class StateC : StateBase
    {
        private int counter;
        public StateC(StateTurkmite st) : base(st)
        {
            counter = 0;
        }

        public override void Enter()
        {
            counter = 0;
            _turkmite.currentState = _turkmite.stateC;
        }
        public override (Vec3b newColor, int deltaDirection) HandleUpdate(Vec3b currentColor)
        {
            counter++;
            CheckStateTransition(currentColor);
            if (counter == 1)
            {
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
                return (black, 3); // turn left
            }
        }
        private void CheckStateTransition(Vec3b currentColor)
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
}
