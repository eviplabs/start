﻿using OpenCvSharp;

namespace TurkMite
{
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
}
