using System;
using AttaxxPlus.Model;
using AttaxxPlus.ViewModel;

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

            foreach (FieldViewModelList row in this.GameViewModel.Fields)
            {
                foreach (FieldViewModel field in row)
                {
                    if (field.Owner == 0)
                    {
                        field.Model.Owner = 3 - this.GameViewModel.CurrentPlayer;
                    }
                }
            }

            return true;
        }
    }
}
