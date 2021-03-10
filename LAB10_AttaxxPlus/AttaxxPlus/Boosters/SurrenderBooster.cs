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
            var currPlayer = this.GameViewModel.Model.CurrentPlayer;
            foreach (var currField in this.GameViewModel.Model.Fields)
            {
                if (currField.Owner != currPlayer)
                {
                    currField.Owner = (currPlayer == 1) ? 2 : 1;
                }
            }
            Notify(nameof(Title));
            return true;
        }
    }
}
