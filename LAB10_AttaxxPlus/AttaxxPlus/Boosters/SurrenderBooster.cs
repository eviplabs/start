using System;
using AttaxxPlus.Model;

namespace AttaxxPlus.Boosters
{
    /// <summary>
    /// Booster filling all empty fields with the opponents' color (assuming 2 players).
    /// </summary>
    public class SurrenderBooster : BoosterBase
    {
        // EVIP: compact override of getter for Title returning constant.
        public override string Title => "Surrender";

        public SurrenderBooster() : base()
        {
            LoadImage(new Uri(@"ms-appx:///Boosters/SurrenderBooster.png"));
        }

        public override bool TryExecute(Field selectedField, Field currentField)
        {
            int otherPlayer = 1;
            if (GameViewModel.CurrentPlayer == otherPlayer)
            {
                otherPlayer = 2;
            }
            foreach (var f in GameViewModel.Model.Fields)
            {
                if (f.Owner == 0)//empty
                {
                    f.Owner = otherPlayer;
                }
            }
            GameViewModel.EndOfTurn();

            return false;
        }
    }
}
