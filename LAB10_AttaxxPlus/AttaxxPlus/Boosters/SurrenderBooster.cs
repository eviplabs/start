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

            int winner = (GameViewModel.CurrentPlayer == 1) ? 2 : 1;

            for (int row = 0; row < GameViewModel.Fields.Count; row++)
            {
                for (int col = 0; col < GameViewModel.Fields.Count; col++)
                {
                    if (GameViewModel.Model.Fields[row, col].IsEmpty())
                        GameViewModel.Model.Fields[row, col].Owner = winner;
                }
            }
            return true;
        }
    }
}
