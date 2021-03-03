using OpenCvSharp;

namespace TurkMite
{
    abstract class StateBase
    {

        public StateTurkmite stateTurkmite;
        readonly protected Vec3b black = new Vec3b(0, 0, 0);
        readonly protected Vec3b white = new Vec3b(255, 255, 255);
        readonly protected Vec3b red = new Vec3b(0, 0, 255);
        public StateBase(StateTurkmite stateTurkmite)
        {
            this.stateTurkmite = stateTurkmite;
        }
        public abstract void Enter();
        public abstract (Vec3b newColor, int deltaDirection) HandleUpdate(Vec3b currentColor);
    }
}
