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
            var rows = base.GameViewModel.Model.Fields.GetLength(0);
            var columns = base.GameViewModel.Model.Fields.GetLength(0);
            int enemyColor = (base.GameViewModel.CurrentPlayer == 1) ? 2 : 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (base.GameViewModel.Fields[i][j].Owner == 0)
                    {
                        var test = base.GameViewModel.Model.Fields[i, j].Owner = enemyColor;
                    }
                }
            }
            return true;
        }
    }
}
