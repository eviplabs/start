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
            Field[,] fields = this.GameViewModel.Model.Fields;
            int otherPlayerIndex = this.GameViewModel.CurrentPlayer == 1 ? 2 : 1;
            for (int row = 0; row < fields.GetLength(0); row++)
            {
                for (int col = 0; col < fields.GetLength(1); col++)
                {
                    if(fields[row, col].IsEmpty())
                        fields[row, col].Owner = otherPlayerIndex;
                }
            }

            return true;
        }
    }
}
