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
            int Next;

            if (this.GameViewModel.CurrentPlayer == 1)
            {
                Next = 2;
            }
            else
            {
                Next = 1;
            }

            foreach (var f in this.GameViewModel.Model.Fields)
                if (f.IsEmpty())
                    f.Owner = Next;

            return true;
        }
    }
}
