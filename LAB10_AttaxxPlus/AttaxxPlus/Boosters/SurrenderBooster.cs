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
            foreach (var i in GameViewModel.Model.Fields)
            {
                if (i.Owner == 0)
                {
                    if (GameViewModel.CurrentPlayer == 1)
                    {
                        i.Owner = 2;
                    }
                    else if (GameViewModel.CurrentPlayer == 2)
                    {
                        i.Owner = 1;
                    }
                }
            }
            return true;
        }
    }
}

