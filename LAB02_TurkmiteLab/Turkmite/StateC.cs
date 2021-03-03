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
            stateTurkmite.currentState = stateTurkmite.stateC;
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
                    stateTurkmite.stateB.Enter();
                }
                else
                {
                    stateTurkmite.stateA.Enter();
                }
            }
        }
    }
}
