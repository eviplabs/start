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
            int newFieldOwner = 0;

            if (1 == this.GameViewModel.CurrentPlayer)
                newFieldOwner = 2;
            else
                newFieldOwner = 1;

            foreach (var field in this.GameViewModel.Model.Fields)
            {
                if (field.IsEmpty())
                    field.Owner = newFieldOwner;
            }
            return true;
        }
    }
}
