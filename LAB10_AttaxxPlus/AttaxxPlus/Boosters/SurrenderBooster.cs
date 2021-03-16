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

            for (int row = 0; row < GameViewModel.Model.Fields.GetLength(0); row++)
            {
                for (int col = 0; col < GameViewModel.Model.Fields.GetLength(1); col++)
                {
                    if (GameViewModel.Model.Fields[row, col].Owner == 0)
                    {
                        GameViewModel.Model.Fields[row, col].Owner = 3 - GameViewModel.CurrentPlayer;
                    }
                }
            }
            return true;
        }
    }
}
