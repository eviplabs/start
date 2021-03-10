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
            for (int i = 0; i < GameViewModel.Fields.Count; i++)
            {
                for (int j = 0; j < GameViewModel.Fields.Count; j++)
                {
                    if (GameViewModel.Model.Fields[i, j].Owner == 0)
                    {
                        GameViewModel.Model.Fields[i, j].Owner = 3 - GameViewModel.CurrentPlayer;
                    }
                }
            }
            return true;
        }
    }
}
